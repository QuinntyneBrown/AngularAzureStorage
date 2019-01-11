using CommandLine;

namespace AngularAzureStorage.CLI
{
    public class Options
    {
        [Option("directory", Required = false, HelpText = "Directory")]
        public string Directory { get; set; }
        public string CurrentDirectory { get; set; } = System.Environment.CurrentDirectory;
    }
}
