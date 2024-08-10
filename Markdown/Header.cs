namespace Markdown;

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