using Ivy;
using Ivy.Chrome;
using Ivy.Core;
using Ivy.Views;
using Ivy.Shared;
using static Ivy.Views.Layout;
using static Ivy.Views.Text;

namespace MarkdownBuilder;

public static class MarkdownBuilderServer
{
    public static async Task RunAsync(ServerArgs? args = null)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
        var server = new Server(args);
        server.AddAppsFromAssembly(typeof(MarkdownBuilderServer).Assembly);
        server.UseHotReload();

        var version = typeof(Server).Assembly.GetName().Version!.ToString().EatRight(".0");
        server.SetMetaTitle($"Markdown Builder {version}");

        var customHeader = Layout.Vertical().Gap(2).Align(Align.Center)
        | new IvyLogo()
                | Text.Muted($"Version {version}")
        | new Html(@"
            <div>
          <a href=""https://github.com/codespaces/new?hide_repo_select=true&ref=main&repo=Ivy-Interactive%2FIvy-Examples&machine=standardLinux32gb&devcontainer_path=.devcontainer%2Fmarkdown-builder%2Fdevcontainer.json&location=EuropeWest"">
            <img src=""https://github.com/codespaces/badge.svg"" alt=""Open Markdown Builder in Codespaces"" />
          </a>
        </div>
      ")
    | new Button("Source Code").Url("https://github.com/Ivy-Interactive/Ivy-Examples/tree/main/markdown-builder").Icon(Icons.ExternalLink).Width(Ivy.Shared.Size.Full());

        var chromeSettings = new ChromeSettings()
            .Header(customHeader)
            .DefaultApp<Apps.MarkdownBuilderApp>()
            .UsePages()
            .UseTabs(preventDuplicates: true);
        server.UseChrome(() => new DefaultSidebarChrome(chromeSettings));

        await server.RunAsync();
    }
}
