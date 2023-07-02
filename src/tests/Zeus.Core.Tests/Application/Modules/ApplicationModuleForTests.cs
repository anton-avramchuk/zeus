using System.Diagnostics.CodeAnalysis;
using Zeus.Core.Application.Abstraction;
using Zeus.Core.Application.Modules;
using Zeus.Core.Application.Modules.Abstractions;

namespace Zeus.Core.Tests.Application.Modules
{
    public class ApplicationModuleForTests : ApplicationModule
    {
        public bool PreConfigureServicesAsyncIsCalled { get; set; }
        public bool ConfigureServicesAsyncIsCalled { get; set; }
        public bool PostConfigureServicesAsyncIsCalled { get; set; }
        public bool OnPreApplicationInitializationIsCalled { get; set; }
        public bool OnPostApplicationInitializationIsCalled { get; set; }
        public bool OnApplicationInitializeAsyncIsCalled { get; set; }
        public bool OnApplicationShutdownAsyncIsCalled { get; set; }

        public bool PreConfigureServicesIsCalled { get; set; }
        public bool ConfigureServicesIsCalled { get; set; }
        public bool PostConfigureServicesIsCalled { get; set; }
        public bool OnPreApplicationInitializationAsyncIsCalled { get; set; }
        public bool OnPostApplicationInitializationAsyncIsCalled { get; set; }
        public bool OnApplicationInitializeIsCalled { get; set; }
        public bool OnApplicationShutdownIsCalled { get; set; }

        public override Task OnPreApplicationInitializationAsync([NotNull] IApplicationInitializationContext context)
        {
            OnPreApplicationInitializationAsyncIsCalled = true;
            return base.OnPreApplicationInitializationAsync(context);
        }

        public override void OnPreApplicationInitialization([NotNull] IApplicationInitializationContext context)
        {
            OnPreApplicationInitializationIsCalled = true;
            base.OnPreApplicationInitialization(context);
        }

        public override void OnApplicationInitialization([NotNull] IApplicationInitializationContext context)
        {
            OnApplicationInitializeIsCalled = true;
            base.OnApplicationInitialization(context);
        }

        public override Task OnApplicationInitializationAsync([NotNull] IApplicationInitializationContext context)
        {
            OnApplicationInitializeAsyncIsCalled = true;
            return base.OnApplicationInitializationAsync(context);
        }

        public override void PreConfigureServices(IApplicationServiceConfiguration context)
        {
            PreConfigureServicesIsCalled = true;
            base.PreConfigureServices(context);
        }

        public override Task PreConfigureServicesAsync(IApplicationServiceConfiguration context)
        {
            PreConfigureServicesAsyncIsCalled = true;
            return base.PreConfigureServicesAsync(context);
        }

        public override void OnPostApplicationInitialization([NotNull] IApplicationInitializationContext context)
        {
            OnPostApplicationInitializationIsCalled = true;
            base.OnPostApplicationInitialization(context);
        }

        public override Task OnPostApplicationInitializationAsync([NotNull] IApplicationInitializationContext context)
        {
            OnPostApplicationInitializationAsyncIsCalled = true;
            return base.OnPostApplicationInitializationAsync(context);
        }

        public override void OnApplicationShutdown([NotNull] IApplicationShutdownContext context)
        {
            OnApplicationShutdownIsCalled = true;
            base.OnApplicationShutdown(context);
        }

        public override Task OnApplicationShutdownAsync([NotNull] IApplicationShutdownContext context)
        {
            OnApplicationShutdownAsyncIsCalled = true;
            return base.OnApplicationShutdownAsync(context);
        }

        public override void PostConfigureServices(IApplicationServiceConfiguration context)
        {
            PostConfigureServicesIsCalled=true;
            base.PostConfigureServices(context);
        }

        public override Task PostConfigureServicesAsync(IApplicationServiceConfiguration context)
        {
            PostConfigureServicesAsyncIsCalled=true;
            return base.PostConfigureServicesAsync(context);
        }
    }
}
