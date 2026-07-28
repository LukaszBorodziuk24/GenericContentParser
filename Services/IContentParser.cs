using GenericContentParser.Models;

namespace GenericContentParser.Services;

public interface IContentParser
{
    ContentType SupportedType { get; }
    object Parse(string content);
}