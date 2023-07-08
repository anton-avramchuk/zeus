using System.Reflection;

namespace Zeus.Core.Application.Configuration.Abstractions
{
    public interface IZeusConfigurationBuilderOptions
    {
        string? BasePath { get; set; }
        string[]? CommandLineArgs { get; set; }
        string? EnvironmentName { get; set; }
        string? EnvironmentVariablesPrefix { get; set; }
        string FileName { get; set; }
        bool Optional { get; set; }
        bool ReloadOnChange { get; set; }
        Assembly? UserSecretsAssembly { get; set; }
        string? UserSecretsId { get; set; }
    }
}