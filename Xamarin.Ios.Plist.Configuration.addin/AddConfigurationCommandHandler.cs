using System.IO;
using System.Linq;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Projects;

namespace Xamarin.Ios.Plist.Configuration
{
    public enum AddConfigurationCommands
    {
        AddConfiguration,
        Configuration,
    }

    public class AddConfigurationCommandHandler : CommandHandler
    {

        protected virtual string GetProjectItemConfigurationPath(ProjectFile item, string configuration)
        {
            var filename = Path.ChangeExtension(item.FilePath.FileName, $@"{configuration}{item.FilePath.Extension}");
            return Path.Combine(item.FilePath.CanonicalPath.ParentDirectory, filename);
        }


        protected override void Run(object dataItem)
        {
            if (dataItem is string configuration && IdeApp.ProjectOperations.CurrentSelectedItem is ProjectFile item)
            {
                var fullpath = GetProjectItemConfigurationPath(item, configuration);
                if (!File.Exists(fullpath))
                {
                    File.Create(fullpath, 1024, FileOptions.None);
                }

                var file = new ProjectFile(fullpath, BuildAction.None, Subtype.Code)
                {
                    DependsOn = item.FilePath,
                };
                item.Project.AddFile(file);
                file.Project.NotifyModified("Items");
                _ = IdeApp.ProjectOperations.SaveAsync(item.Project);
            }
        }


        protected override void Update(CommandArrayInfo info)
        {
            if (IdeApp.ProjectOperations.CurrentSelectedItem is ProjectFile item)
            {
                var configurations = IdeApp.Workspace.GetConfigurations()
                    .Select(c => c.Split('|').First().Trim())
                    .Distinct()
                    .OrderBy(c => c)
                    .ToList();

                // Is this already a configuration specific?
                if (configurations.Select(c => $@"{c}{item.FilePath.Extension}").Any(c => item.FilePath.FileName.EndsWith(c, System.StringComparison.InvariantCultureIgnoreCase)))
                {
                    return;
                }

                foreach (var cfg in configurations)
                {
                    var cfgInfo = info.Add(cfg, cfg);
                    cfgInfo.Enabled = !File.Exists(GetProjectItemConfigurationPath(item, cfg));
                }
            }
        }
    }

}