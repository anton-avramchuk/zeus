using Zeus.Core.DependencyInjection;

namespace Zeus.Core.Tests.DependencyInjection
{
    [TestFixture]
    public class ObjectAccessorTests
    {
        private ObjectAccessor<object> Create(object? value)
        {
            return new ObjectAccessor<object>(value);
        }

        [Test]
        public void CheckIsNullValue()
        {
            var accessor= Create(null);
            Assert.IsNull(accessor.Value);
        }


        [Test]
        public void CheckValue()
        {
            var expected=new object();
            var accessor = Create(expected);
            Assert.That(accessor.Value, Is.EqualTo(expected));
        }

    }
}
