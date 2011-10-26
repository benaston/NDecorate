// ReSharper disable InconsistentNaming
namespace NDecorate.Test.Fast
{
    using NUnit.Framework;

    [TestFixture]
    public class Decorate_HappyPath_DecoratesType
    {
        [Test]
        public void TestMethod()
        {
            var query = new MyQuery();
            Assert.That(query.Execute() == "hello");
            var decoratedQuery = query.Decorate<IQueryTypeA>(new[] {new NameAdderDecorator()});
            Assert.That(decoratedQuery.Execute() == "hello world");
        }
    }

    public interface IQueryTypeA : IDecorator<IQueryTypeA>
    {
        string Execute();
    }

    public class MyQuery : IQueryTypeA
    {
        public string Execute()
        {
            return "hello";
        }

        public IQueryTypeA Target { get; set; }
    }

    public class NameAdderDecorator : IQueryTypeA
    {
        public IQueryTypeA Target { get; set; }

        public string Execute()
        {
            return Target.Execute() + " world";
        }
    }
}
// ReSharper restore InconsistentNaming