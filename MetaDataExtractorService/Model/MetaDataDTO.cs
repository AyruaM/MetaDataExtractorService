namespace MetaDataExtractorService.Model
{
    public class MetaDataDTO
    {
            public string PatientName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string DocumentType { get; set; }
            public string ProviderName { get; set; }
            public DateTime DocumentDate { get; set; }
            public IFormFile File { get; set; }

    }
    public class DocumentUploadDto
    {
        public IFormFile File { get; set; }
    }

}
