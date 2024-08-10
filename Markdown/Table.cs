using System.Text;

namespace Markdown;

public class Table : IMarkdown
{
    internal string[] headers;
    internal string[][] rows;

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

    public static Table From<T>(IEnumerable<T> collection, string title = "") 
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

public class TableWithCaption : IMarkdown
{
    string caption;
    Table table;
    
    string[] headers => table.headers;
    string[][] rows => table.rows;

    public TableWithCaption(string[] headers, string[][] rows, string caption = "") {
        this.table = new Table(headers, rows);
        this.caption = caption;
    }

    public TableWithCaption(Table table, string caption) {
        this.table = table;
        this.caption = caption;
    }

    public string BuildMarkdown()
    {
        StringBuilder sb = new();
        sb.AppendLine("<table>");
        if (!string.IsNullOrEmpty(caption))
            sb.AppendLine($"<caption style=\"text-align: center; font-weight: bold;\">{caption}</caption>");
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

        return sb.ToString();
    }

    public static TableWithCaption From<T>(IEnumerable<T> collection, string caption = "") 
    {
        return new TableWithCaption(Table.From<T>(collection), caption);
    }
}