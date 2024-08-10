using System.Text;

namespace Markdown;

public class Table : IMarkdown
{
    string[] headers;
    string[][] rows;

    public Table(string[] headers, string[][] rows) {
        this.headers = headers;
        this.rows = rows;
    }

    public string BuildMarkdown()
    {
        StringBuilder sb = new();

        foreach (var header in headers)
        {
            sb.Append($"|{header}");
        }
        sb.AppendLine("|");

        sb.Append("|:-");
        for (int i = 1; i < headers.Length; i++)
        {
            sb.Append("|-");
        }
        sb.AppendLine(":|");

        foreach (var row in rows)
        {
            foreach (var column in row) 
            {
                sb.Append($"|{column}");
            }
            sb.AppendLine("|");
        }

        return sb.ToString();
    }

    public static Table From<T>(IEnumerable<T> collection) 
    {
        var typeInfo = typeof(T).GetProperties();
        var headers = typeInfo.Select(p => p.Name).ToArray();
        List<string[]> rows = new();
        foreach (var item in collection)
        {
            rows.Add(typeInfo.Select(h => h.GetValue(item).ToString()).ToArray());
        }
        return new Table(headers, rows.ToArray());
    }
}