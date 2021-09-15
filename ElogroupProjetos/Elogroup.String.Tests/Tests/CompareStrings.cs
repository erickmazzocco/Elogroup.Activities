using NUnit.Framework;

namespace Elogroup.String.UnitTests
{
    [TestFixture]
    class CompareStrings
    {
        private Code.CompareStrings _compareStrings;

        public CompareStrings()
        {

        }

        [SetUp]
        public void SetUp()
        {
            _compareStrings = new Code.CompareStrings();
        }

        [Test]
        [TestCase("Paulo", "Paula", true, true, true, 0.8)]
        [TestCase("Pauló", "Paulã", true, true ,true, 0.8)]
        [TestCase("paulo *       ", "Paulo", true, false, true, 1)]
        [TestCase(" José da Silva", "José da Silva ****", true, false, true, 1)]
        public void Execute_WhenCalled_ReturnOnlyNumbers(string source,
            string target,
            bool removeSpecialCharacters,
            bool ignoreBlankSpaces,
            bool replaceAccents, 
            double expectedResult)
        {
            var result = _compareStrings.Execute(
                source, 
                target, 
                removeSpecialCharacters, 
                ignoreBlankSpaces, 
                replaceAccents);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
