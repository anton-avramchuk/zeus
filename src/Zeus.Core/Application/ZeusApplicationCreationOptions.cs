using Microsoft.Extensions.DependencyInjection;
using Zeus.Core.Application.Configuration;
using Zeus.Core.Application.Configuration.Abstractions;

namespace Zeus.Core.Application
{
    public class ZeusApplicationCreationOptions
    {
        public IServiceCollection Services { get; }

        //[NotNull]
        //public PlugInSourceList PlugInSources { get; }

        /// <summary>
        /// The options in this property only take effect when IConfiguration not registered.
        /// </summary>
        public IZeusConfigurationBuilderOptions Configuration { get; }

        public bool SkipConfigureServices { get; set; }

        public string? ApplicationName { get; set; }

        public string? Environment { get; set; }

        public ZeusApplicationCreationOptions(IServiceCollection services)
        {
            Services = services;
            Configuration = new ZeusConfigurationBuilderOptions();
        }
    }
}
