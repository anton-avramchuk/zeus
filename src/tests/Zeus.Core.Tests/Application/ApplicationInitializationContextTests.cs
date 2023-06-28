using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeus.Core.Application;

namespace Zeus.Core.Tests.Application
{
    [TestFixture]
    public class ApplicationInitializationContextTests
    {
        private Mock<IServiceProvider> _serviceProviderMock;
        [SetUp]
        public void SetUp()
        {
            _serviceProviderMock = new Mock<IServiceProvider>();
        }


        private ApplicationInitializationContext Create()
        {
            return new ApplicationInitializationContext(_serviceProviderMock.Object);
        }

        [Test]
        public void CheckServiceProviderNotNull()
        {
            var context=Create();
            Assert.IsNotNull(context.ServiceProvider);
        }
    }
}
