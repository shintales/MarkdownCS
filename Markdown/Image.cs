namespace Markdown;

public class Image : IMarkdown
{
    Link link;
    public Image(string text, string url)
    {
        link = new Link(text, url);
    }
    public string BuildMarkdown()
    {
        return $"!{link.BuildMarkdown()}";
    }
}
