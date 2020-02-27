using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Elogroup.StringActivities
{
    [DisplayName("Get Text Between Keywords")]
    [Description("Get all text between the keywords")]
    public class GetTextBetweenKeywords : StringActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter with the first keyword")]
        [DisplayName("First Keyword")]
        public InArgument<string> FirstKeyword { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter with the second keyword")]
        [DisplayName("Second Keyword")]
        public InArgument<string> SecondKeyword { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter with a text or variable")]
        [DisplayName("Input Text")]            
        public InArgument<string> InputText { get; set; }

        [Category("Options")]
        [DefaultValue(false)]
        [Description("If this option is check, the activity will use 'ignore case'")]
        [DisplayName("Ignore Case")]
        public bool IgnoreCase
        {
            get;
            set;
        }

        [Category("Output")]
        [Description("Output Text")]
        public OutArgument<string> OutputText { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                OutputText.Set(context, 
                    ExecuteMethod(
                        FirstKeyword.Get(context), 
                        SecondKeyword.Get(context), 
                        InputText.Get(context), 
                        IgnoreCase));

                ValidActivity.Set(context, true);
            }
            catch (Exception ex)
            {
                if (ThrowError.Equals(ThrowOptions.Yes))
                    throw ex;

                OutputText.Set(context, string.Empty);
                ValidActivity.Set(context, false);
            }                
        }

        public string ExecuteMethod(string firstKey, string secondKey, string Text, bool ignoreCase)
        {
            var stringComparison = IsIgnoreCase(ignoreCase);

            if (KeywordNotExistsInText(firstKey, Text, ignoreCase) 
                || KeywordNotExistsInText(secondKey, Text, ignoreCase))
                throw new Exception("The keywords wasn't find in the text");

            var firstPartOfText = GetFirstPartOfText(firstKey, Text, stringComparison);            

            return GetSecondPartOfText(secondKey, firstPartOfText, stringComparison);
        }

        protected StringComparison IsIgnoreCase (bool ignoreCase)
        {
            if(ignoreCase)
                return StringComparison.InvariantCultureIgnoreCase;
            else
                return StringComparison.InvariantCulture;
        }

        protected bool KeywordNotExistsInText(string Keyword, string Text, bool ignoreCase)
        {
            if(ignoreCase)
                return !Text.ToUpper().Contains(Keyword.ToUpper());
            else
                return !Text.Contains(Keyword);
        }

        protected bool SecondKeywordExists(string secondKeyword)
        {
            return String.IsNullOrEmpty(secondKeyword) ? false : true;
        }

        protected string GetFirstPartOfText(string firstKeyword, string Text, StringComparison stringComparison)
        {
            var indexFirst = Text.IndexOf(firstKeyword, stringComparison);
            var lenght = firstKeyword.Length;

            return Text.Substring(indexFirst + lenght, Text.Length - indexFirst - lenght);
        }

        protected string GetSecondPartOfText(string secondKeyword, string Text, StringComparison stringComparison)
        {            
            if(SecondKeywordExists(secondKeyword))
                return Text.Substring(0, Text.IndexOf(secondKeyword, stringComparison));
            else
                return Text;
        }
    }
}
