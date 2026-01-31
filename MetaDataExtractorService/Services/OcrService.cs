using Azure;
using Azure.AI.Vision.ImageAnalysis;
using Tesseract;

namespace MetaDataExtractorService.Services
{
    public class OcrService
    {


        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public OcrService(IWebHostEnvironment env,IConfiguration config)
        {
            _env = env;
            _config = config;

        }

        public string ExtractTextFromImage(string filePath)
        {
            var tessDataPath = Path.Combine(_env.ContentRootPath, "tessdata");

            using var engine = new TesseractEngine(
                tessDataPath,
                "eng",
                EngineMode.Default
            );

            using var img = Pix.LoadFromFile(filePath);
            using var page = engine.Process(img);

            return page.GetText();
        }


        public async Task<string> ExtractTextAzureAsync(string filePath)
        {
            var client = new ImageAnalysisClient(
                new Uri(_config["AzureVision:Endpoint"]),
                new AzureKeyCredential(_config["AzureVision:Key"])
            );

            using var stream = File.OpenRead(filePath);

            var result = await client.AnalyzeAsync(
                BinaryData.FromStream(stream),
                VisualFeatures.Read
            );

            // Use the correct property: Read instead of ReadResult
            return string.Join("\n",
                result.Value.Read.Blocks
                    .SelectMany(b => b.Lines)
                    .Select(l => l.Text));
        }
    }
}
