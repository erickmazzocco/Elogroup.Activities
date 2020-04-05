using System;
using NUnit.Framework;

namespace Elogroup.String.UnitTests
{
    [TestFixture]
    public class ReplaceAccentedCharactersTests
    {
        private Code.ReplaceAccentedCharacters _replaceAccentedCharacters;

        [SetUp]
        public void SetUp()
        {
            _replaceAccentedCharacters = new Code.ReplaceAccentedCharacters();
        }

        [Test]
        [TestCase("RéplâcÈ âccénted Charâctérs", "ReplacE accented Characters")]
        public void Execute_WhenCalled_ReturnTextWithoutAccents(string text, string expectedResult)
        {
            var result = _replaceAccentedCharacters.Execute(text);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
