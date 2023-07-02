using Microsoft.Extensions.DependencyInjection;
using Zeus.Core.Application.Modules;

namespace Zeus.Core.Tests.Application.Modules
{
    public class ApplicationServiceConfigurationTests
    {
        private Mock<IServiceCollection> _serviceCollectionMock;

        [SetUp]
        public void SetUp()
        {
            _serviceCollectionMock = new Mock<IServiceCollection>();
        }

        private ApplicationServiceConfiguration Create()
        {
            return new ApplicationServiceConfiguration(_serviceCollectionMock.Object);
        }

        [TestCase]
        public void CheckItemsIsNotNull()
        {
            var config = Create();

            Assert.IsNotNull(config.Items);
        }

        [TestCase]
        public void CheckServicesIsNotNull()
        {
            var config = Create();

            Assert.IsNotNull(config.Services);
        }

        [TestCase]
        public void CheckByKeyIsNull()
        {
            var key = "key";
            var config = Create();

            Assert.IsNull(config[key]);
        }

        [TestCase]
        public void CheckByKeyIsSameObject()
        {
            var key = "key";
            var obj = new object();
            var config = Create();
            config[key] = obj;
            Assert.That(config[key], Is.SameAs(obj));
        }

        [TestCase]
        public void CheckByKeyIsNotSameObject()
        {
            var key = "key";
            var obj = new object();
            var obj2 = new object();
            var config = Create();
            config[key] = obj;
            config[key] = obj2;
            Assert.That(config[key], Is.Not.SameAs(obj));
        }

    }
}
