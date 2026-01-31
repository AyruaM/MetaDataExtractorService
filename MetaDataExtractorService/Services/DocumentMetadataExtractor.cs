using System.Text.RegularExpressions;

public class DocumentMetadataExtractor
{
    public PatientDocument Extract(string text)
    {
        return new PatientDocument
        {
            PatientName = Match(text, @"Patient Name:\s*(.*)"),
            ProviderName = Match(text, @"Provider:\s*(.*)"),
            DocumentType = Match(text, @"Type:\s*(.*)"),
            DateOfBirth = Match(text, @"DOB:\s*(\d{2}/\d{2}/\d{4})"),
            DocumentDate = Match(text, @"Document Date:\s*(\d{2}/\d{2}/\d{4})")
        };
    }

    private string Match(string text, string pattern)
    {
        var match = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
        return match.Success ? match.Groups[1].Value.Trim() : null;
    }

    private DateTime? ParseDate(string text, string pattern)
    {
        var match = Regex.Match(text, pattern);
        return match.Success ? DateTime.Parse(match.Groups[1].Value) : null;
    }
}
