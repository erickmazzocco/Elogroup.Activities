using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elogroup.String
{
    [DisplayName("Compare Strings")]
    [Description("Compare two strings and return the similarity")]
    public class CompareStrings : Activity
    {
        #region Arguments
        [Category("Input")]
        [RequiredArgument]
        [Description("Source string")]
        public InArgument<string> Source { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Target string")]
        public InArgument<string> Target { get; set; }

        [Category("Options")]
        [DefaultValue(true)]
        [Description("Remove all special characters inside Source and Target strings")]
        public bool RemoveSpecialCharacters { get; set; } = true;

        [Category("Options")]
        [DefaultValue(false)]
        [Description("Ignore the blank spaces inside strings")]
        public bool IgnoreBlankSpace { get; set; } = false;

        [Category("Options")]
        [DefaultValue(true)]
        [Description("Replace letters with accents")]
        public bool ReplaceAccents { get; set; } = true;

        [Category("Output")]
        [Description("Similarity")]
        public OutArgument<double> Similarity { get; set; }
        #endregion

        public CompareStrings()
        {

        }

        protected override void Execute(CodeActivityContext context)
        {
            double result;
            try
            {
                var compareStrings = new Code.CompareStrings();

                result = compareStrings.Execute(
                    Source.Get(context),
                    Target.Get(context),
                    RemoveSpecialCharacters,
                    IgnoreBlankSpace,
                    ReplaceAccents
                );
            }
            catch (Exception ex)
            {
                if (ThrowError.Equals(ThrowOptions.Yes))
                    throw ex;

                result = 0;
            }

            Similarity.Set(context, result);
        }
    }
}
