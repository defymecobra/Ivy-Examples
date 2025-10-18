using Ivy;
using Ivy.Apps;
using Ivy.Shared;
using Ivy.Core;
using static Ivy.Views.Layout;
using static Ivy.Views.Text;

namespace MarkdownBuilder.Apps;

[App(order: 1, title: "Markdown Builder", icon: Icons.FileText)]
public class MarkdownBuilderApp() : ViewBase
{
    private string _markdownContent = """
        # Welcome to Markdown Builder!

        This is an example of markdown content. You can edit this text in the left panel.

        ## Features

        - **Bold text**
        - *Italic text*
        - `Code`
        - [Links](https://example.com)

        ### Code

        ```csharp
        public class Example
        {
            public void DoSomething()
            {
                Console.WriteLine("Hello, World!");
            }
        }
        ```

        > This is a quote

        ---

        **Start editing in the left panel!**
        """;

    public override object? Build()
    {
        var markdownState = this.UseState(_markdownContent);
        
        var leftCard = new Card(
            Layout.Vertical().Gap(6).Padding(3)
            | Text.H3("Markdown Builder")
            | Text.Block("Enter your markdown code:")
            | markdownState.ToCodeInput()
                .Language(Languages.Markdown)
                .Width(Size.Full())
                .Height(Size.Units(120))
                .Placeholder("Body (Markdown)")
            | Text.Markdown("Builds HTML from Markdown.")
          | Text.Markdown("Built with [Ivy Framework](https://github.com/Ivy-Interactive/Ivy-Framework)")
        ).Width(Size.Fraction(0.4f));

        var rightPanel = Layout.Vertical().Width(Size.Fraction(0.6f))
            | Text.Markdown(markdownState.Value);

        return Layout.Horizontal().Gap(6)
            | leftCard
            | rightPanel;
    }
}
