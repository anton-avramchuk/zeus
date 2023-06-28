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

        [SetUp]
        public void SetUp() {
            _applicationInitializationContextMock = new Mock<IApplicationInitializationContext>();
            _applicationServiceConfigurationMock = new Mock<IApplicationServiceConfiguration>();
            
        }

        private ApplicationModule Create()
        {
            return new ApplicationModuleForTests();
        }

        [Test]
        public void CheckConfigureServices()
        {
            var module=Create();
            module.ConfigureServices(_applicationServiceConfigurationMock.Object);
        }


        [Test]
        public async Task CheckConfigureServicesAsync()
        {
            var module = Create();
            await module.ConfigureServicesAsync(_applicationServiceConfigurationMock.Object);
        }






    }
}
