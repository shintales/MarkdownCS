namespace Markdown;

public class Link : IMarkdown
{
    string text;
    string url;
    public Link(string text, string url)
    {
        this.text = text; this.url = url;
    }
    public string BuildMarkdown()
    {
        return $"[{text}]({url})";
    }
}