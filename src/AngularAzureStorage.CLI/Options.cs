using CommandLine;

namespace AngularAzureStorage.CLI
{
    public class Options
    {
        [Option("directory", Required = false, HelpText = "Directory")]
        public string Directory { get; set; }
        [Option("connectionString", Required = false, HelpText = "Connection String")]
        public string CloudStorageConnectionString { get; set; }
    }
}
