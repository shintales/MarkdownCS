namespace Markdown;

public abstract class AddableBase
{
    public List<IMarkdown> Items { get; set; } = new();

    public AddableBase() {
        Items = new();
    }

    public AddableBase(IMarkdown[] items) {
        Items = new(items);
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

    public void Clear() => Items.Clear();
}