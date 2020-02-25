using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elogroup.StringActivities
{
    [DisplayName("Replace Accented Characters")]
    [Description("Replace a accented character for your letter without accent")]
    public class ReplaceAccentedCharacters : StringActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter with a text or variable")]
        [DisplayName("Input Text")]
        public InArgument<string> InputText { get; set; }

        [Category("Output")]
        [Description("Output Text")]
        public OutArgument<string> OutputText { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                OutputText.Set(context, ReplaceAccented(
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

        protected string ReplaceAccented(string InputText)
        {
            var normalizedString = InputText.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
