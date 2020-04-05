using System;
using System.Activities;
using System.ComponentModel;

namespace Elogroup.String
{
    [DisplayName("Remove Special Characters")]
    [Description("Remove all special characters of a string")]
    public class RemoveSpecialCharacters : Activity
    {
        #region Arguments
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter with a text or variable")]        
        public InArgument<string> InputText { get; set; }

        [Category("Options")]
        [DefaultValue(false)]
        [Description("If this option is check, all blank space in string will preserved")]        
        public bool IgnoreBlankSpace { get; set; }

        [Category("Output")]
        [Description("OutputText")]        
        public OutArgument<string> OutputText { get; set; }
        #endregion

        public RemoveSpecialCharacters()
        {

        }

        protected override void Execute(CodeActivityContext context)
        {
            string result;

            try
            {
                var RemoveSpecialCharacters = new Code.RemoveSpecialCharacters();
                RemoveSpecialCharacters.SetOptions(IgnoreBlankSpace);

                result = RemoveSpecialCharacters.Execute(
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
