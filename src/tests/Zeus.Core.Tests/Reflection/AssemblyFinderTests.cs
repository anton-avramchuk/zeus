using Zeus.Core.Application.Modules.Abstractions;
using Zeus.Core.Reflection;
using Zeus.Core.Tests.Application.Modules;

namespace Zeus.Core.Tests.Reflection
{
    [TestFixture]
    public class AssemblyFinderTests
    {

        [Theory]
        [TestCase(new object[] { new Type[] { } })]
        [TestCase(new object[] { new[] { typeof(ApplicationEmptyModule) } })]
        public void Should_Get_Assemblies_Of_Given_Modules(Type[] moduleTypes)
        {
            //Arrange

            var fakeModuleContainer = CreateFakeModuleContainer(moduleTypes);

            //Act

            var assemblyFinder = new AssemblyFinder(fakeModuleContainer);

            //Assert
            Assert.That(assemblyFinder.Assemblies.Count, Is.EqualTo(moduleTypes.Length));
            

            foreach (var moduleType in moduleTypes)
            {
                Assert.IsTrue(assemblyFinder.Assemblies.Contains(moduleType.Assembly));
            }
        }

        private static IApplicationModuleContainer CreateFakeModuleContainer(IEnumerable<Type> moduleTypes)
        {
            var moduleDescriptors = moduleTypes.Select(CreateModuleDescriptor).ToList();
            return CreateFakeModuleContainer(moduleDescriptors);
        }

        private static IApplicationModuleContainer CreateFakeModuleContainer(List<IApplicationModuleDescriptor> moduleDescriptors)
        {
            var moduleContainerMock = new Mock<IApplicationModuleContainer>();
            moduleContainerMock.Setup(x=>x.Modules).Returns(moduleDescriptors);
            return moduleContainerMock.Object;
        }

        private static IApplicationModuleDescriptor CreateModuleDescriptor(Type moduleType)
        {
            var moduleDescriptorMock = new Mock<IApplicationModuleDescriptor>();
            moduleDescriptorMock.Setup(x => x.Type).Returns(moduleType);
            
            return moduleDescriptorMock.Object;
        }
    }
}
