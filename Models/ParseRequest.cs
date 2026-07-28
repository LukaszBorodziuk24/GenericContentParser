namespace GenericContentParser.Models;

public class ParseRequest
{
    public ContentType? Type { get; set; }

    public string? Content { get; set; } = string.Empty;
}