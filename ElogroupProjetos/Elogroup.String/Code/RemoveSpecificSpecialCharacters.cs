using System.Text.RegularExpressions;

namespace Elogroup.String.Code
{
    public class RemoveSpecificSpecialCharacters
    {
        public RemoveSpecificSpecialCharacters()
        {

        }

        public string Execute(string text, string characters)
        {
            if(string.IsNullOrEmpty(characters)) return text;

            var result = Regex.Replace(text, GetRegexRule(characters), string.Empty);
            
            return result;
        }

        private string GetRegexRule(string characters)
        {
            return $@"[{characters.Replace(" ", @"\s")}]";
        }
    }
}
