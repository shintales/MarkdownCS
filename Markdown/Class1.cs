using System.Text;

namespace Markdown;

public interface IMarkdown {
    string BuildMarkdown();
}

public enum HeaderLevel {
    H1 = 1,
    H2,
    H3,
    H4,
    H5,
    H6
}

public record Header(HeaderLevel Level = HeaderLevel.H1, string Text =  "") : IMarkdown
{
    public string BuildMarkdown()
    {
        var headerLevel = string.Concat(Enumerable.Repeat("#", (int)Level));
        return $"{headerLevel} {Text}\n";
    }
}

public abstract class AddableBase
{
    public List<IMarkdown> Items { get; set; } = new();

    public AddableBase() {
        Items = new();
    }

    public AddableBase(List<IMarkdown> items) {
        Items = items;
    }

    public AddableBase Add(IMarkdown markdown)
    {
        Items.Add(markdown);
        return this;
    }

    public AddableBase Add(string markdown) => Add(new Text(markdown));
}

public enum ListStyle {
    Ordered,
    Unordered
}

public class List : AddableBase, IMarkdown
{
    public ListStyle Style {get;set;}

    public List(ListStyle style) : base() {
        Style = style;
    }

    public List(ListStyle style, List<IMarkdown> items) : base(items) {
        Style = style;
    }

    public string BuildMarkdown()
    {
        StringBuilder stringBuilder = new StringBuilder();
        string entrySeperator = Style switch {
            ListStyle.Ordered => "1.",
            ListStyle.Unordered => "-"
        };
        foreach (var item in Items) {
            stringBuilder.Append($"{entrySeperator} {item.BuildMarkdown()}\n");
        }
        return stringBuilder.ToString();
    }
}

public class Text : IMarkdown {
    private string text;

    public Text(string text)
    {
        this.text = text;
    }

    public string BuildMarkdown()
    {
        return text;
    }
}

public class Table : IMarkdown
{
    List<string> headers;
    List<string[]> rows;

    public Table(List<string> headers, List<string[]> rows) {
        this.headers = headers;
        this.rows = rows;
    }

    public string BuildMarkdown()
    {
        throw new NotImplementedException();
    }

    public static Table From<T>(IEnumerable<T> collection) 
    {
        var fields = typeof(T).GetFields().Where(f => f.IsPublic);
        return new Table(new List<string> (){}, new List<string[]> (){});
    }
}

public class Document : AddableBase {
    public Document() {
        this.Items = new List<IMarkdown>();
    }

    public Document(List<IMarkdown> markdowns)
    {
        this.Items = markdowns;
    }

    public string BuildMarkdown()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var markdown in Items) {
            sb.Append(markdown.BuildMarkdown());
        }
        return sb.ToString();
    }
}

public class Class1
{

}
