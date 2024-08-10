namespace Markdown;

public class Text : IMarkdown {
    private string text;

    public Text(string text)
    {
        this.text = text;
    }

    public static implicit operator Text(string name)
    {
        return new Text(name);
    }

    public string BuildMarkdown()
    {
        return text;
    }
}