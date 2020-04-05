using System;
using System.Activities;
using System.ComponentModel;

namespace Elogroup.String
{
    [DisplayName("Remove Specific Special Characters")]
    [Description("Remove some special characters of a string")]
    public class RemoveSpecificSpecialCharacters : Activity
    {
        #region Arguments
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter with a text or variable")]        
        public InArgument<string> InputText { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Put all Characters that will removed")]        
        public InArgument<string> Characters { get; set; }

        [Category("Output")]        
        public OutArgument<string> OutputText { get; set; }
        #endregion

        public RemoveSpecificSpecialCharacters()
        {

        }

        protected override void Execute(CodeActivityContext context)
        {
            string result;
            try
            {
                var RemoveSpecificSpecialCharacters = new Code.RemoveSpecificSpecialCharacters();

                result = RemoveSpecificSpecialCharacters.Execute(
                    InputText.Get(context), 
                    Characters.Get(context)
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
