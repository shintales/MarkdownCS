using System.Text;

namespace Markdown;

public class Table : IMarkdown
{
    string title;
    string[] headers;
    string[][] rows;

    public Table(string[] headers, string[][] rows, string title = "") {
        this.headers = headers;
        this.rows = rows;
        this.title = title;
    }

    public string BuildMarkdown()
    {
        StringBuilder sb = new();
        sb.AppendLine("<table>");
        if (!string.IsNullOrEmpty(title))
            sb.AppendLine($"<caption style=\"text-align: center; font-weight: bold;\">{title}</caption>");
        sb.AppendLine("<tr>");
        foreach (var header in headers)
        {
            sb.AppendLine($"<th>{header}</th>");
        }
        sb.AppendLine("</tr>");

        foreach (var row in rows)
        {
            sb.AppendLine("<tr>");
            foreach (var column in row) 
            {
                sb.AppendLine($"<td>{column}</td>");
            }
            sb.AppendLine("</tr>");
        }
        sb.AppendLine("</table>");

        /*
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
        */

        return sb.ToString();
    }

    public static Table From<T>(IEnumerable<T> collection, string title = "") 
    {
        var typeInfo = typeof(T).GetProperties();
        var headers = typeInfo.Select(p => p.Name).ToArray();
        List<string[]> rows = new();
        foreach (var item in collection)
        {
            rows.Add(typeInfo.Select(h => h.GetValue(item).ToString()).ToArray());
        }
        return new Table(headers, rows.ToArray(), title);
    }
}