﻿using Microsoft.Extensions.Configuration;
using Zeus.Core.Application.Configuration;
using Zeus.Core.Application.Configuration.Abstractions;
using Zeus.Core.Extensions.Common;

namespace Zeus.Core.Extensions.Configuration
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot BuildConfiguration(
            IZeusConfigurationBuilderOptions? options = null,
            Action<IConfigurationBuilder>? builderAction = null)
        {
            options ??= new ZeusConfigurationBuilderOptions();

            if (options.BasePath.IsNullOrEmpty())
            {
                options.BasePath = Directory.GetCurrentDirectory();
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(options.BasePath!)
                .AddJsonFile(options.FileName + ".json", optional: options.Optional, reloadOnChange: options.ReloadOnChange);

            if (!options.EnvironmentName.IsNullOrEmpty())
            {
                builder = builder.AddJsonFile($"{options.FileName}.{options.EnvironmentName}.json", optional: options.Optional, reloadOnChange: options.ReloadOnChange);
            }

            if (options.EnvironmentName == "Development")
            {
                if (options.UserSecretsId != null)
                {
                    builder.AddUserSecrets(options.UserSecretsId);
                }
                else if (options.UserSecretsAssembly != null)
                {
                    builder.AddUserSecrets(options.UserSecretsAssembly, true);
                }
            }

            builder = builder.AddEnvironmentVariables(options.EnvironmentVariablesPrefix);

            if (options.CommandLineArgs != null)
            {
                builder = builder.AddCommandLine(options.CommandLineArgs);
            }

            builderAction?.Invoke(builder);

            return builder.Build();
        }
    }
}
