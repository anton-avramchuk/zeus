using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Zeus.Core.Application.Modules.Abstractions;
using Zeus.Core.Extensions.Collections;

namespace Zeus.Core.Application.Modules
{
    public class ApplicationServiceConfiguration : IApplicationServiceConfiguration
    {
        public IServiceCollection Services { get; }

        public IDictionary<string, object?> Items { get; }

        /// <summary>
        /// Gets/sets arbitrary named objects those can be stored during
        /// the service registration phase and shared between modules.
        ///
        /// This is a shortcut usage of the <see cref="Items"/> dictionary.
        /// Returns null if given key is not found in the <see cref="Items"/> dictionary.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object? this[string key]
        {
            get => Items.GetOrDefault(key);
            set => Items[key] = value;
        }

        public ApplicationServiceConfiguration(IServiceCollection services)
        {
            Services = services;
            Items = new Dictionary<string, object?>();
        }
    }

}
