using System.Text.Json;
using GenericContentParser.Models;

namespace GenericContentParser.Services.Parsers;

public class InternalJsonParser : IContentParser
{
    public ContentType SupportedType => ContentType.INTERNAL_JSON;

    public object Parse(string content)
    {
        try
        {
            using var document = JsonDocument.Parse(content);

            return document.RootElement.Clone();
        }
        catch (JsonException)
        {
            throw new ArgumentException(
                "Content is not valid JSON"
            );
        }
    }
}