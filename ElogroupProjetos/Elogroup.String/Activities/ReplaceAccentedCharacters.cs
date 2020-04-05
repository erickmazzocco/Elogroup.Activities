using System;
using System.Activities;
using System.ComponentModel;

namespace Elogroup.String
{
    [DisplayName("Replace Accented Characters")]
    [Description("Replace a accented character for your letter without accent")]
    public class ReplaceAccentedCharacters : Activity
    {
        #region Arguments
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter with a text or variable")]        
        public InArgument<string> InputText { get; set; }

        [Category("Output")]        
        public OutArgument<string> OutputText { get; set; }
        #endregion

        public ReplaceAccentedCharacters()
        {

        }

        protected override void Execute(CodeActivityContext context)
        {
            string result;
            try
            {
                var ReplaceAccentedCharacters = new Code.ReplaceAccentedCharacters();

                result = ReplaceAccentedCharacters.Execute(
                    InputText.Get(context)
                );                
            }
            catch (Exception ex)
            {
                if (ThrowError.Equals(ThrowOptions.Yes))
                    throw ex;

                result = string.Empty;
            }

            OutputText.Set(context, result);
        }        
    }
}
