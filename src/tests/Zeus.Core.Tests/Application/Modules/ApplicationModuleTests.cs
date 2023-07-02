using Zeus.Core.Application.Abstraction;
using Zeus.Core.Application.Modules;
using Zeus.Core.Application.Modules.Abstractions;

namespace Zeus.Core.Tests.Application.Modules
{
    [TestFixture]
    public class ApplicationModuleTests
    {
        private Mock<IApplicationInitializationContext> _applicationInitializationContextMock;
        private Mock<IApplicationServiceConfiguration> _applicationServiceConfigurationMock;
        private Mock<IApplicationShutdownContext> _applicationShutdonwContextMock;

        [SetUp]
        public void SetUp() {
            _applicationInitializationContextMock = new Mock<IApplicationInitializationContext>();
            _applicationServiceConfigurationMock = new Mock<IApplicationServiceConfiguration>();
            _applicationShutdonwContextMock = new Mock<IApplicationShutdownContext>();            
        }

        private ApplicationModuleForTests Create()
        {
            return new ApplicationModuleForTests();
        }

        [Test]
        public void CheckConfigureServices()
        {
            var module=Create();
            module.ConfigureServices(_applicationServiceConfigurationMock.Object);
            Assert.IsTrue(module.ConfigureServicesIsCalled);
        }


        [Test]
        public async Task CheckConfigureServicesAsync()
        {
            var module = Create();
            await module.ConfigureServicesAsync(_applicationServiceConfigurationMock.Object);
            Assert.IsTrue(module.ConfigureServicesAsyncIsCalled);
        }


        [Test]
        public void CheckIsApplicationModule()
        {
            var module = Create();
            Assert.IsTrue(ApplicationModule.IsApplicationModule(module.GetType()));
        }

        [Test]
        public void CheckIsNotApplicationModule()
        {
            var module = new object();
            Assert.IsFalse(ApplicationModule.IsApplicationModule(module.GetType()));
        }

        [Test]
        public void CheckOnPostApplicationInitialization()
        {
            var module = Create();
            module.OnPostApplicationInitialization(_applicationInitializationContextMock.Object);
            Assert.IsTrue(module.OnPostApplicationInitializationIsCalled);
        }


        [Test]
        public async Task CheckOnPostApplicationInitializationAsync()
        {
            var module = Create();
            await module.OnPostApplicationInitializationAsync(_applicationInitializationContextMock.Object);
            Assert.IsTrue(module.OnPostApplicationInitializationAsyncIsCalled);
        }


        [Test]
        public void CheckOnPreApplicationInitialization()
        {
            var module = Create();
            module.OnPreApplicationInitialization(_applicationInitializationContextMock.Object);
            Assert.IsTrue(module.OnPreApplicationInitializationIsCalled);
        }

        [Test]
        public async Task CheckOnPreApplicationInitializationAsync()
        {
            var module = Create();
            await module.OnPreApplicationInitializationAsync(_applicationInitializationContextMock.Object);
            Assert.IsTrue(module.OnPreApplicationInitializationAsyncIsCalled);
        }

        [Test]
        public void CheckOnApplicationInitialization()
        {
            var module = Create();
            module.OnApplicationInitialization(_applicationInitializationContextMock.Object);
            Assert.IsTrue(module.OnApplicationInitializeIsCalled);
        }

        [Test]
        public async Task CheckOnApplicationInitializationAsync()
        {
            var module = Create();
            await module.OnApplicationInitializationAsync(_applicationInitializationContextMock.Object);
            Assert.IsTrue(module.OnApplicationInitializeAsyncIsCalled);
        }

        [Test]
        public void CheckPreConfigureServices()
        {
            var module = Create();
            module.PreConfigureServices(_applicationServiceConfigurationMock.Object);
            Assert.IsTrue(module.PreConfigureServicesIsCalled);
        }

        [Test]
        public async Task CheckPreConfigureServicesAsync()
        {
            var module = Create();
            await module.PreConfigureServicesAsync(_applicationServiceConfigurationMock.Object);
            Assert.IsTrue(module.PreConfigureServicesAsyncIsCalled);
        }

        [Test]
        public void CheckOnApplicationShutdown()
        {
            var module = Create();
            module.OnApplicationShutdown(_applicationShutdonwContextMock.Object);
            Assert.IsTrue(module.OnApplicationShutdownIsCalled);
        }

        [Test]
        public async Task CheckOnApplicationShutdownAsync()
        {
            var module = Create();
            await module.OnApplicationShutdownAsync(_applicationShutdonwContextMock.Object);
            Assert.IsTrue(module.OnApplicationShutdownAsyncIsCalled);
        }

        [Test]
        public void CheckPostConfigureServices()
        {
            var module = Create();
            module.PostConfigureServices(_applicationServiceConfigurationMock.Object);
            Assert.IsTrue(module.PostConfigureServicesIsCalled);
        }

        [Test]
        public async Task CheckPostConfigureServicesAsync()
        {
            var module = Create();
            await module.PostConfigureServicesAsync(_applicationServiceConfigurationMock.Object);
            Assert.IsTrue(module.PostConfigureServicesAsyncIsCalled);
        }
    }
}
