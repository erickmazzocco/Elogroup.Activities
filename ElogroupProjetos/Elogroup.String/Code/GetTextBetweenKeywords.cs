using System;

namespace Elogroup.String.Code
{
    public class GetTextBetweenKeywords
    {
        public bool IgnoreCase {get; private set;}
        public bool TrimOutput {get; private set;}
        public bool KeepKeywords {get; private set;}

        public GetTextBetweenKeywords()
        {
        }
        
        public void SetOptions(bool ignoreCase, bool trimOutput, bool keepKeywords)
        {
            IgnoreCase = ignoreCase;
            TrimOutput = trimOutput;
            KeepKeywords = keepKeywords;
        }

        public string Execute(string text, string keyWord1, string keyWord2)
        {
            if (InputsAreNotValid(text))
                return text;            

            if (KeywordNotExistsInText(keyWord1, text) 
                || KeywordNotExistsInText(keyWord2, text))
                throw new ArgumentException ("The keyword wasn't find in the text");                      

            var firstText = GetFirstPartOfText(text, keyWord1);

            var result = GetSecondPartOfText(firstText, keyWord2);           

            return TrimOutputIfTrue(KeepKeywordsIfTrue(result, keyWord1, keyWord2));
        }

        private bool InputsAreNotValid(string text)
        {
            return string.IsNullOrEmpty(text);
        }

        private StringComparison GetStringComparison ()
        {
            return IgnoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture;
        }

        private bool KeywordNotExistsInText(string keyword, string text)
        {
            text = IgnoreCase ? text.ToUpper() : text;
            keyword = IgnoreCase ? keyword.ToUpper() : keyword;

            return !text.Contains(keyword);
        }        

        private string GetFirstPartOfText(string text, string keyWord1)
        {
            if(string.IsNullOrEmpty(keyWord1))
                return text;

            var index = text.IndexOf(keyWord1, GetStringComparison());
            var lenght = keyWord1.Length;

            return text.Substring(index + lenght, text.Length - index - lenght);
        }

        private string GetSecondPartOfText(string text, string keyWord2)
        {            
            if(KeywordNotExistsInText(keyWord2, text))            
                return text;

            if(string.IsNullOrEmpty(keyWord2))
                return text;

            return text.Substring(0, text.IndexOf(keyWord2, GetStringComparison()));
        }

        private string TrimOutputIfTrue(string text)
        {
            return TrimOutput ? text.Trim() : text;
        }

        private string KeepKeywordsIfTrue(string text, string keyWord1, string keyWord2)
        {
            return KeepKeywords ? keyWord1 + text + keyWord2 : text;
        }

    }
}
