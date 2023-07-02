using System.Xml.Serialization;
using Zeus.Core.Application.Modules.Abstractions;
using Zeus.Core.Reflection;

namespace Zeus.Core.Tests.Reflection
{
    [TestFixture]
    public class AssemblyFinderTests
    {
        private Mock<IApplicationModuleContainer> _applicationModuleContainerMock;

        [SetUp]
        public void SetUp()
        {
            _applicationModuleContainerMock = new Mock<IApplicationModuleContainer>();
        }

        private AssemblyFinder Create()
        {
            return new AssemblyFinder(_applicationModuleContainerMock.Object);
        }

        [Test]
        public void Test()
        {
            throw new NotImplementedException();
        }
    }
}
