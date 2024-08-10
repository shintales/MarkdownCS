using System.Text;

namespace Markdown;

public class Document : AddableBase {
    public Document() {
        this.Items = new List<IMarkdown>();
    }

    public Document(IMarkdown[] markdowns) : base(markdowns) {}
    public Document(List<IMarkdown> markdowns) : base(markdowns) {}

    public string BuildMarkdown()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var markdown in Items)
        {
            sb.Append(markdown.BuildMarkdown());
        }
        return sb.ToString();
    }
}