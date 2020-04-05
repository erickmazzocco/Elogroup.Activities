using System.Text.RegularExpressions;

namespace Elogroup.String.Code
{
    public class RemoveSpecialCharacters
    {        
        public bool IgnoreBlankSpace;

        public RemoveSpecialCharacters()
        {

        }

        public void SetOptions(bool ignoreBlankSpace)
        {
            IgnoreBlankSpace = ignoreBlankSpace;
        }

        public string Execute(string text)
        {
            var result = Regex.Replace(text, GetRegexRule(), string.Empty);

            return result;
        }

        private string GetRegexRule()
        {
            return IgnoreBlankSpace ? @"[^0-9a-zA-Z\s]+" : @"[^0-9a-zA-Z]+";
        }

    }
}
