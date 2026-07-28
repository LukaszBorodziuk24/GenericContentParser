using GenericContentParser.Models;

namespace GenericContentParser.Services.Parsers;

public class CsvParser: IContentParser
{
    public ContentType SupportedType => ContentType.CSV;

    public object Parse(string content)
    {
        var lines = content.Split(
            '\n',
            StringSplitOptions.RemoveEmptyEntries
        );

        if (lines.Length < 2)
        {
            throw new ArgumentException(
                "CSV must contain header and at least one row"
            );
        }

        var headers = lines[0]
            .Split(',')
            .Select(x => x.Trim())
            .ToArray();

        var rows = new List<Dictionary<string, string>>();

        foreach (var line in lines.Skip(1))
        {
            var values = line
                .Split(',')
                .Select(x => x.Trim())
                .ToArray();

            if (values.Length != headers.Length)
            {
                throw new ArgumentException(
                    "CSV row has different number of columns than header"
                );
            }

            var row = new Dictionary<string, string>();

            for (int i = 0; i < headers.Length; i++)
            {
                row[headers[i]] = values[i];
            }

            rows.Add(row);
        }

        return rows;
    }
}