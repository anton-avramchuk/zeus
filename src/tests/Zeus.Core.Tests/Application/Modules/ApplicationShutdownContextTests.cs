using Zeus.Core.Application.Modules;

namespace Zeus.Core.Tests.Application.Modules
{
    [TestFixture]
    public class ApplicationShutdownContextTests
    {
        private Mock<IServiceProvider> _serviceProviderMock;

        [SetUp]
        public void SetUp()
        {
            _serviceProviderMock = new Mock<IServiceProvider>();
        }

        private ApplicationShutdownContext Create()
        {
            return new ApplicationShutdownContext(_serviceProviderMock.Object);
        }

        public void CheckServiceProviderIsNotull()
        {
            var context=Create();
            Assert.IsNotNull(context.ServiceProvider);
        }
    }
}
