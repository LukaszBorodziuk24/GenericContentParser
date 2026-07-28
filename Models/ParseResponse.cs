namespace GenericContentParser.Models;

public class ParseResponse
{
    public string Status { get; set; } = string.Empty;

    public int Count { get; set; }

    public object Data { get; set; } = new();
}