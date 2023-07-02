using System.Reflection;
using Zeus.Core.Reflection.Abstractions;
using Zeus.Core.Reflection;

namespace Zeus.Core.Tests.Reflection
{
    [TestFixture]
    public class TypeFinderTests
    {
        private Mock<IAssemblyFinder> _assemblyFinderMock;

        [SetUp]
        public void SetUp()
        {
            _assemblyFinderMock = new Mock<IAssemblyFinder>();
            _assemblyFinderMock.Setup(x => x.Assemblies).Returns(new List<Assembly>
            {
                typeof(TypeFinderTests).Assembly
            });
        }

        private TypeFinder Create()
        {
            return new TypeFinder(_assemblyFinderMock.Object);
        }

        [Test]
        public void Should_Find_Types_In_Given_Assemblies()
        {
            var finder=Create();

            //Assert

            Assert.IsTrue(finder.Types.Contains(typeof(TypeFinderTests)));

        }
    }
    
}
