using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elogroup.String
{
    [DisplayName("Get Only Numbers")]
    [Description("Get all numbers in the text")]
    public class GetOnlyNumbers : Activity
    {
        #region Arguments
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter with a text or variable")]        
        public InArgument<string> InputText { get; set; }

        [Category("Output")]        
        public OutArgument<string> OutputText { get; set; }
        #endregion

        public GetOnlyNumbers()
        {
        }

        protected override void Execute(CodeActivityContext context)
        {
            string result;
            try
            {
                var getOnlyNumbers = new Code.GetOnlyNumbers();

                result = getOnlyNumbers.Execute(
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
