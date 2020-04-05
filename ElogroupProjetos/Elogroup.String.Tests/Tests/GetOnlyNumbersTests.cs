using System;
using NUnit.Framework;

namespace Elogroup.String.UnitTests
{
    [TestFixture]
    public class GetOnlyNumbersTests
    {
        private Code.GetOnlyNumbers _getOnlyNumbers;        

        public GetOnlyNumbersTests()
        {

        }

        [SetUp]
        public void SetUp()
        {
            _getOnlyNumbers = new Code.GetOnlyNumbers();
        }        


        [Test]
        [TestCase("Testing123 ", "123")]
        [TestCase("!@#1st2ingé´r´t3 ", "123")]
        public void Execute_WhenCalled_ReturnOnlyNumbers(string text, string expectedResult)
        {
            var result = _getOnlyNumbers.Execute(text);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
