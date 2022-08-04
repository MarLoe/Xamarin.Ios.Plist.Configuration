using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin(
    "Xamarin.Ios.Plist.Configuration",
    Namespace = "VisualStudioMac",
    Version = "17.0",
    Category = "IDE extensions"
)]

[assembly: AddinName("Xamarin Ios Plist Configuration")]
[assembly: AddinDescription("Create configuration specific plist settings.\n\nby Martin Løbger")]
[assembly: AddinAuthor("Martin Løbger")]
[assembly: AddinUrl("https://github.com/MarLoe/Xamarin.Ios.Plist.Configuration")]

[assembly: AddinDependency("::MonoDevelop.Core", MonoDevelop.BuildInfo.Version)]
[assembly: AddinDependency("::MonoDevelop.Ide", MonoDevelop.BuildInfo.Version)] 