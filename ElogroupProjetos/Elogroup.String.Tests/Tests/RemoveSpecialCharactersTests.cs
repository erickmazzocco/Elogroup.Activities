using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elogroup.String.UnitTests
{
    [TestFixture]
    class RemoveSpecialCharactersTests
    {        
        private const string DefaultText = "Sp&ci@l Ch@r@ct£rs";
        private const string DefaultTextAccented = "Spéêci@l Châráct£rs";
        private Code.RemoveSpecialCharacters _removeSpecialCharacters;

        [SetUp]
        public void SetUp()
        {
            _removeSpecialCharacters = new Code.RemoveSpecialCharacters();
        }

        [Test]
        [TestCase(DefaultText, false, "SpcilChrctrs")]
        [TestCase(DefaultText, true, "Spcil Chrctrs")]
        public void Execute_PreserveBlankSpaceIsFalseOrTrue_TextWithoutSpecialCharacters(string text, bool preserveBlankSpace, string expectedResult)
        {
            _removeSpecialCharacters.SetOptions(preserveBlankSpace);
            var result = _removeSpecialCharacters.Execute(text);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("", true, "")]        
        public void Execute_TextIsEmpty_TextWithoutSpecialCharacters(string text, bool preserveBlankSpace, string expectedResult)
        {
            _removeSpecialCharacters.SetOptions(preserveBlankSpace);
            var result = _removeSpecialCharacters.Execute(text);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(DefaultTextAccented, true, "Spcil Chrctrs")]        
        public void Execute_TextWithAccents_TextWithoutSpecialCharacters(string text, bool preserveBlankSpace, string expectedResult)
        {
            _removeSpecialCharacters.SetOptions(preserveBlankSpace);
            var result = _removeSpecialCharacters.Execute(text);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("¹²³H½¼¾><±−×÷∗⁄‰∫∑∏√∞≈≅∝≡≠≤≥∴⋅·∂ℑℜ′″°∠⊥∇⊕⊗ℵøØ∈∉∩∪⊂⊃⊆⊇∃∀∅¬∧∨↵← e →↑ e ↓↔⇐ e ⇒⇑e ⇓⇔⌈ e ⌉⌊ e i⌋◊&><ˆ˜¨´¸\"“”‘’‹›«»ºª–—­  ¯…¦•¶§♠♣♥♦", false, "Heeeeeei")]        
        [TestCase("¹²³H½¼¾><±−×÷∗⁄‰∫∑∏√∞≈≅∝≡≠≤≥∴⋅·∂ℑℜ′″°∠⊥∇⊕⊗ℵøØ∈∉∩∪⊂⊃⊆⊇∃∀∅¬∧∨↵←e→↑e↓↔⇐e⇒⇑e⇓⇔⌈e⌉⌊ei⌋◊&><ˆ˜¨´¸\"“”‘’‹›«»ºª–—­¯…¦•¶ § ♠ ♣♥♦", true, "Heeeeeei   ")]
        public void Execute_ALotOfSpecialCharacters_TextWithoutSpecialCharacters(string text, bool preserveBlankSpace, string expectedResult)
        {
            _removeSpecialCharacters.SetOptions(preserveBlankSpace);
            var result = _removeSpecialCharacters.Execute(text);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
