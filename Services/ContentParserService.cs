using GenericContentParser.Models;
using System.Text;

namespace GenericContentParser.Services;

public class ContentParserService(IEnumerable<IContentParser> parsers)
{
    
    public ParseResponse Parse(ParseRequest request)
    {
        if (request.Type is null)
        {
            throw new ArgumentException("Content type is required");
        }

        if (string.IsNullOrWhiteSpace(request.Content))
        {
            throw new ArgumentException("Content is required");
        }
        
        var parser = parsers
            .SingleOrDefault(p => p.SupportedType == request.Type);
        

        if (parser is null)
        {
            throw new NotSupportedException(
                $"Content type {request.Type} is not supported"
            );
        }

        var decodedContent = DecodeBase64(request.Content);

        var data = parser.Parse(decodedContent);

        return new ParseResponse{
            Status = "success",
            Count = GetCount(data),
            Data = data
        };
    }
    
    private string DecodeBase64(string content)
    {
        try
        {
            var bytes = Convert.FromBase64String(content);

            return Encoding.UTF8.GetString(bytes);
        }
        catch (FormatException)
        {
            throw new ArgumentException(
                "Content is not valid Base64"
            );
        }
    }
    
    private int GetCount(object data)
    {
        if (data is IEnumerable<object> collection)
        {
            return collection.Count();
        }

        return 1;
    }
    
}