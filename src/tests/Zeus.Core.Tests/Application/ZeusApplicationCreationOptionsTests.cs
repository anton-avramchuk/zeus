using Microsoft.Extensions.DependencyInjection;
using Zeus.Core.Application;

namespace Zeus.Core.Tests.Application
{
    [TestFixture]
    public class ZeusApplicationCreationOptionsTests
    {
        private Mock<IServiceCollection> _serviceCollectionMock;

        [SetUp]
        public void SetUp()
        {
            _serviceCollectionMock = new Mock<IServiceCollection>();
        }

        private ZeusApplicationCreationOptions Create()
        {
            return new ZeusApplicationCreationOptions(_serviceCollectionMock.Object);
        }


        [Test]
        public void CheckServicesIsNotNull()
        {
            var options= Create();
            Assert.IsNotNull(options.Services);
        }

        [Test]
        public void CheckConfigurationIsNotNull()
        {
            var options = Create();
            Assert.IsNotNull(options.Configuration);
        }
    }
}
