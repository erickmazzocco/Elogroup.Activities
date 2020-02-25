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
    [DisplayName("Remove Special Characters")]
    [Description("Remove all special characters of a string")]
    public class RemoveSpecialCharacters : StringActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter with a text or variable")]
        [DisplayName("Input Text")]
        public InArgument<string> InputText { get; set; }

        [Category("Options")]
        [DefaultValue(false)]
        [Description("If this option is check, all blank space in string will preserved")]
        [DisplayName("Ignore Blank Space")]
        public bool IgnoreBlankSpace { get; set; }

        [Category("Output")]
        [Description("Output Text")]
        [DisplayName("Output Text")]
        public OutArgument<string> OutputText { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                OutputText.Set(context, 
                    ApplyRegexRule(
                        InputText.Get(context)
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

        protected string ApplyRegexRule(string InputString)
        {
            return Regex.Replace(
                InputString,
                GetRegexRule(),
                string.Empty);
        }

        protected string GetRegexRule()
        {
            return this.IgnoreBlankSpace ? @"[^0-9a-zA-Z\s]+" : @"[^0-9a-zA-Z]+";
        }
    }
}
