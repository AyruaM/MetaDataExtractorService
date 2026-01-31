using MetaDataExtractorService.Data;
using MetaDataExtractorService.Model;
using MetaDataExtractorService.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MetaDataExtractorService.Data;

namespace MetaDataExtractorService.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly OcrService _ocrService;
        private readonly DocumentMetadataExtractor _metadataExtractor;

        public DocumentsController(
            IWebHostEnvironment env,
            AppDbContext context,
            OcrService ocrService,
            DocumentMetadataExtractor metadataExtractor)
        {
            _env = env;
            _context = context;
            _ocrService = ocrService;
            _metadataExtractor = metadataExtractor;
            _context = context;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] DocumentUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("File is required");

            // 1️⃣ Save file to DataFiles
            var uploadsFolder = Path.Combine(_env.ContentRootPath, "DataFiles");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{dto.File.FileName}";
            var fullFilePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            // 2️⃣ OCR — USE YOUR EXISTING FUNCTIONS HERE
            string extractedText;

            var extension = Path.GetExtension(dto.File.FileName).ToLower();

            if (extension == ".pdf")
            {
                // 👉 Call YOUR Azure Vision OCR for PDF
                extractedText = await _ocrService.ExtractTextAzureAsync(fullFilePath);
            }
            else
            {
                // 👉 Call YOUR Tesseract OCR for images
                extractedText = _ocrService.ExtractTextFromImage(fullFilePath);
            }

            // 3️⃣ Extract metadata from OCR text (YOUR logic)
            var document = _metadataExtractor.Extract(extractedText);

            // 4️⃣ Add file info
            document.FileName = dto.File.FileName;
            document.FilePath = "DataFiles/"+uniqueFileName;
            document.CreatedAt = DateTime.UtcNow.ToString();

            // 5️⃣ Save to DB
            _context.PatientDocuments.Add(document);
            await _context.SaveChangesAsync();

            return Ok(document);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var documents = await _context.PatientDocuments
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(documents);
        }
    }
}
