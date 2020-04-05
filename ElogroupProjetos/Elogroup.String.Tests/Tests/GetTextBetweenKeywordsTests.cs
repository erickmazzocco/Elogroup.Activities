using Elogroup.String.Code;
using NUnit.Framework;

namespace Elogroup.String.UnitTests
{
    [TestFixture]
    public class GetTextBetweenKeywordsTests
    {
        private const string DefaultText = "Class fields should never be accessed directly";
        private Code.GetTextBetweenKeywords _getTextBetweenKeywords;

        [SetUp]
        public void SetUp()
        {
            _getTextBetweenKeywords = new Code.GetTextBetweenKeywords();
        }

        [Test]
        [TestCase(DefaultText, "fields", "accessed", false, false, false, " should never be ")]        
        public void Execute_OptionsAreFalse_ReturnTextBetweenKeywords(string text, string key1, string key2, bool ignoreCase, bool trimOutput, bool keepKeywords, string expectedResult)
        {            
            _getTextBetweenKeywords.SetOptions(ignoreCase, trimOutput, keepKeywords);
            var result = _getTextBetweenKeywords.Execute(text, key1, key2);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(DefaultText, "Fields", "be", false, false, false)]
        [TestCase(DefaultText, "fields", "Be", false, false, false)]
        public void Execute_KeywordNotFound_ReturnException(string text, string key1, string key2, bool ignoreCase, bool trimOutput, bool keepKeywords)
        {            
            _getTextBetweenKeywords.SetOptions(ignoreCase, trimOutput, keepKeywords);            
            
            Assert.That(
                () => _getTextBetweenKeywords.Execute(text, key1, key2), 
                Throws.ArgumentException);
        }

        [Test]
        [TestCase(DefaultText, "Fields", "accessed", true, false, false, " should never be ")]
        [TestCase(DefaultText, "fields", "Accessed", true, false, false, " should never be ")]
        public void Execute_IgnoreCaseIsTrue_ReturnTextBetweenKeywords(string text, string key1, string key2, bool ignoreCase, bool trimOutput, bool keepKeywords, string expectedResult)
        {            
            _getTextBetweenKeywords.SetOptions(ignoreCase, trimOutput, keepKeywords);
            var result = _getTextBetweenKeywords.Execute(text, key1, key2);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(DefaultText, "fields", "accessed", false, true, false, "should never be")]        
        public void Execute_TrimOutputIsTrue_ReturnTextBetweenKeywords(string text, string key1, string key2, bool ignoreCase, bool trimOutput, bool keepKeywords, string expectedResult)
        {            
            _getTextBetweenKeywords.SetOptions(ignoreCase, trimOutput, keepKeywords);
            var result = _getTextBetweenKeywords.Execute(text, key1, key2);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(DefaultText, " fields", "accessed ", false, false, true, " fields should never be accessed ")]
        [TestCase(DefaultText, " fields", "accessed ", false, true, true, "fields should never be accessed")]
        public void Execute_KeepKeywordsIsTrue_ReturnTextBetweenKeywords(string text, string key1, string key2, bool ignoreCase, bool trimOutput, bool keepKeywords, string expectedResult)
        {            
            _getTextBetweenKeywords.SetOptions(ignoreCase, trimOutput, keepKeywords);
            var result = _getTextBetweenKeywords.Execute(text, key1, key2);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(DefaultText, "", "accessed", false, false, false, "Class fields should never be ")]        
        public void Execute_FirstArgumentIsEmpty_ReturnTextBetweenKeywords(string text, string key1, string key2, bool ignoreCase, bool trimOutput, bool keepKeywords, string expectedResult)
        {            
            _getTextBetweenKeywords.SetOptions(ignoreCase, trimOutput, keepKeywords);
            var result = _getTextBetweenKeywords.Execute(text, key1, key2);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(DefaultText, "fields", "", false, false, false, " should never be accessed directly")]        
        public void Execute_SecondArgumentIsEmpty_ReturnTextBetweenKeywords(string text, string key1, string key2, bool ignoreCase, bool trimOutput, bool keepKeywords, string expectedResult)
        {            
            _getTextBetweenKeywords.SetOptions(ignoreCase, trimOutput, keepKeywords);
            var result = _getTextBetweenKeywords.Execute(text, key1, key2);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(DefaultText, "", "", false, false, false, DefaultText)]        
        public void Execute_BothArgumentAreEmpty_ReturnTextBetweenKeywords(string text, string key1, string key2, bool ignoreCase, bool trimOutput, bool keepKeywords, string expectedResult)
        {            
            _getTextBetweenKeywords.SetOptions(ignoreCase, trimOutput, keepKeywords);
            var result = _getTextBetweenKeywords.Execute(text, key1, key2);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }


    }
}
