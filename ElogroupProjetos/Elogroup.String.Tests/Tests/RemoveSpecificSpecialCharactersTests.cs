using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elogroup.String.UnitTests
{
    [TestFixture]
    class RemoveSpecificSpecialCharactersTests
    {           
        private Code.RemoveSpecificSpecialCharacters _removeSpecificSpecialCharacters;

        [SetUp]
        public void SetUp()
        {
            _removeSpecificSpecialCharacters = new Code.RemoveSpecificSpecialCharacters();
        }

        [Test]
        [TestCase("Sp&ci@l Ch@rªct£rs", "&@ª£ ", "SpcilChrctrs")]
        [TestCase("Sp&ci@l Ch@rªct£rs", "ª£", "Sp&ci@l Ch@rctrs")]
        [TestCase("Sp&ci@l Ch@rªct£rs", "  ", "Sp&ci@lCh@rªct£rs")]
        public void Execute_WhenCalled_TextWithoutSpecificSpecialCharacters(string text, string characters, string expectedResult)
        {            
            var result = _removeSpecificSpecialCharacters.Execute(text, characters);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
