using Zeus.Core.Application.Abstraction;
using Zeus.Core.Application.Modules;

namespace Zeus.Core.Tests.Application.Modules
{
    [TestFixture]
    public class ApplicationModuleTests
    {
        private Mock<IApplicationInitializationContext> _applicationInitializationContextMock;

        [SetUp]
        public void SetUp() {
            _applicationInitializationContextMock = new Mock<IApplicationInitializationContext>();
        }

        private ApplicationModule Create()
        {
            return new ApplicationModuleForTests();
        }

        [Test]
        public void CheckServiceProviderIsNotNull()
        {
            var module= Create();
            Assert.IsNotNull(module.ServiceProvider);
        }

    }
}
