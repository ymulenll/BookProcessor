using NUnit.Framework;

namespace BookProcessor.Implementation.UnitTests
{
    [TestFixture]
    public class SimpleBookMapperTests
    {
        [Test]
        public void BasicMap_CorrectInput_WorksFine()
        {
            var booksMapper = new SimpleBookMapper();

            string[] fields = { "hello", "25.36" };
            var result = booksMapper.Map(fields);

            Assert.AreEqual("hello", result.Title);
            Assert.AreEqual(25.36, result.Price);
        }

        [Test]
        [SetCulture("Es-es")]
        public void Map_SpanishCulture_WorksFine()
        {
            var booksMapper = new SimpleBookMapper();

            string[] fields = { "hello", "25.36" };
            var result = booksMapper.Map(fields);

            Assert.AreEqual("hello", result.Title);
            Assert.AreEqual(25.36, result.Price);
        }
    }
}
