public class PatientDocument
{
    public int Id { get; set; }

    public string PatientName { get; set; }

    public string DateOfBirth { get; set; } = string.Empty;

    public string DocumentType { get; set; }
    public string ProviderName { get; set; } = string.Empty;

    public string DocumentDate { get; set; }

    public string FileName { get; set; }
    public string FilePath { get; set; }

    public string CreatedAt { get; set; }
}
