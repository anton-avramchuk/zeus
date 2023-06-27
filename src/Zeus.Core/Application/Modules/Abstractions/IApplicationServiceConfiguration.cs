using Microsoft.Extensions.DependencyInjection;

namespace Zeus.Core.Application.Modules.Abstractions
{
    public interface IApplicationServiceConfiguration
    {
        IServiceCollection Services { get; }

        IDictionary<string, object?> Items { get; }

        object? this[string key] { get; set; }
    }
}
