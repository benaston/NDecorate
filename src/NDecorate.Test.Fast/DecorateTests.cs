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
            var decoratedQuery = query.Decorate<IQueryTypeA>(new IQueryTypeA[] { new WorldAdderDecorator(), new ExclamationAdderDecorator() });
            Assert.That(decoratedQuery.Execute() == "hello world!");
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

        public IQueryTypeA DecoratorTarget { get; set; }
    }

    public class WorldAdderDecorator : IQueryTypeA
    {
        public IQueryTypeA DecoratorTarget { get; set; }

        public string Execute()
        {
            return DecoratorTarget.Execute() + " world";
        }
    }

    public class ExclamationAdderDecorator : IQueryTypeA
    {
        public IQueryTypeA DecoratorTarget { get; set; }

        public string Execute()
        {
            return DecoratorTarget.Execute() + "!";
        }
    }
}
// ReSharper restore InconsistentNaming