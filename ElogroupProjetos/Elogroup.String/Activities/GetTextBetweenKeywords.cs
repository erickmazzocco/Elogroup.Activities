using System;
using System.Activities;
using System.ComponentModel;

namespace Elogroup.String
{
    [DisplayName("Get Text Between Keywords")]
    [Description("Get all text between the keywords")]
    public class GetTextBetweenKeywords : Activity
    {
        #region Arguments
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter with the first keyword")]        
        public InArgument<string> Keyword1 { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter with the second keyword")]        
        public InArgument<string> Keyword2 { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter with a text or variable")]          
        public InArgument<string> InputText { get; set; }

        [Category("Options")]
        [DefaultValue(false)]
        [Description("If this option is check, the activity will use 'ignore case'")]        
        public bool IgnoreCase
        {
            get;
            set;
        }

        [Category("Options")]
        [DefaultValue(true)]        
        public bool TrimOutput
        {
            get;
            set;
        }

        [Category("Options")]
        [DefaultValue(false)]                
        public bool KeepKeywords
        {
            get;
            set;
        }

        [Category("Output")]
        [Description("Output Text")]
        public OutArgument<string> OutputText { get; set; }
        #endregion

        public GetTextBetweenKeywords()
        {
            TrimOutput = true;
        }

        protected override void Execute(CodeActivityContext context)
        {
            string result;
            try
            {
                var GetTextBetweenKeywords = new Code.GetTextBetweenKeywords();
                GetTextBetweenKeywords.SetOptions(IgnoreCase, TrimOutput, KeepKeywords);
                
                result = GetTextBetweenKeywords.Execute(
                    InputText.Get(context), 
                    Keyword1.Get(context), 
                    Keyword2.Get(context)
                );                
            }
            catch (Exception ex)
            {
                if (ThrowError.Equals(ThrowOptions.Yes))
                    throw ex;

                result  = string.Empty;
            }

            OutputText.Set(context, result);
        }        
    }
}
