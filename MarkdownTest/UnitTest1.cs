using Markdown;

namespace MarkdownTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestMarkdownTable()
    {
        var table = new Table(
            ["Test1", "Test2", "Test3"],
            [
                ["Value1", "Value2", "Value3"]
            ]
        );
        Console.WriteLine(table.BuildMarkdown());
//        Assert.That(header.BuildMarkdown(), Is.EqualTo("# Test\n"));
    }

    [Test]
    public void Test1()
    {
        var header = new Header(HeaderLevel.H1, "Test");
        Assert.That(header.BuildMarkdown(), Is.EqualTo("# Test\n"));
    }

    [Test]
    public void Test2()
    {
        var list = new Markdown.List(ListStyle.Ordered, new List<IMarkdown>() {
            new Markdown.Text("Hello World")
        });
        Assert.That(list.BuildMarkdown(), Is.EqualTo("1. Hello World\n"));
    }

    public void Test3()
    {
        var document = new Markdown.Document(new List<IMarkdown>() {
            new Markdown.Header(HeaderLevel.H2, "Test"),
            new Markdown.Text("Example\n"),
            new Markdown.List(ListStyle.Ordered, new List<IMarkdown>() {
                new Markdown.Text("Hello World")
            })
        });
        Assert.That(document.BuildMarkdown(), Is.EqualTo("## Test\nExample\n1. Hello World\n"));
    }

    public void Test4()
    {
        var document = new Markdown.Document();
        document.Add(new Markdown.Header(HeaderLevel.H2, "Test"))
            .Add(new Markdown.Text("Example\n"))
            .Add(new Markdown.List(ListStyle.Ordered, new List<IMarkdown>() {
                new Markdown.Text("Hello World")
            }));
        Assert.That(document.BuildMarkdown(), Is.EqualTo("## Test\nExample\n1. Hello World\n"));
    }

    public record Ex(string Text, string Title);

    [Test]
    public void Test5()
    {
        var x = new List<Ex>(){
            new Ex("Test", "Tep"),
            new Ex("Test2", "Tep2")
        };
        var y = Markdown.TableWithCaption.From(x, caption: "Hello World Plz");
        Console.WriteLine(y.BuildMarkdown());
    }
}