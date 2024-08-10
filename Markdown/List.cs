namespace Markdown;

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

    public List(ListStyle style, IMarkdown[] items) : base(items) {
        Style = style;
    }

    public List(ListStyle style, List<IMarkdown> items) : base(items) {
        Style = style;
    }

    public string BuildMarkdown()
    {
        System.Text.StringBuilder stringBuilder = new();
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