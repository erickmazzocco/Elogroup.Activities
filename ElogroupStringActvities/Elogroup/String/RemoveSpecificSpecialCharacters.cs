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
    [DisplayName("Remove Specific Special Characters")]
    [Description("Remove some special characters of a string")]
    public class RemoveSpecificSpecialCharacters : StringActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter with a text or variable")]
        [DisplayName("Input Text")]
        public InArgument<string> InputText { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Put all Characters that will removed")]
        [DisplayName("Specific Characters")]
        public InArgument<string> Characters { get; set; }

        [Category("Output")]
        [Description("Output Text")]
        public OutArgument<string> OutputText { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                OutputText.Set(context, 
                    ApplyRegexRule(
                        InputText.Get(context),
                        Characters.Get(context)
                    ));
                                    
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

        protected string ApplyRegexRule(string InputText, string Characters)
        {
            return Regex.Replace(
                InputText, 
                GetRegexRule(Characters), 
                string.Empty);
        }

        protected string GetRegexRule(string Characters)
        {
            return $@"[{Characters.Replace(" ", "")}]";
        }
    }
}
