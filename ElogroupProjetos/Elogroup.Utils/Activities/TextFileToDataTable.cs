using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elogroup.Utils
{
    [DisplayName("TextFile To DataTable")]
    [Description("Convert a .txt/.csv file in a DataTable")]
    public class TextFileToDataTable : CodeActivity
    {
        #region Arguments
        [Category("Input")]
        [RequiredArgument]
        [Description("The full path of the .csv/.txt file")]
        public InArgument<string> File { get; set; }

        [Category("Options")]
        [Description(@"The quantity of lines that will be skiped")]
        [DefaultValue(0)]
        public InArgument<int> HowManyLinesNeedToSkip { get; set; } = 0;

        [Category("Options")]
        [DefaultValue(false)]
        [Description(@"There is header inside the text file")]
        public bool HasHeaders { get; set; } = false;

        [Category("Options")]
        [Description("Default character: ;")]
        public InArgument<string> BreakColumns { get; set; } = ";";

        [Category("Options")]
        [Description("Default character: \\n")]
        public InArgument<string> BreakLines { get; set; } = "\n";

        [Category("Options")]
        [DefaultValue(false)]
        [Description(@"Throw an exception if the quantity of line's columns is diferent from the quantity of datatable's columns")]
        public bool ThrowExceptionIfThereIsProblemWithLine { get; set; } = false;

        [Category("Output")]
        public OutArgument<DataTable> OutputDataTable { get; set; }

        [Category("Output")]
        public OutArgument<List<string>> OutputLinesWithProblem { get; set; }
        #endregion

        protected override void Execute(CodeActivityContext context)
        {
            DataTable result;
            var LinesWithProblem = new List<string>();
            try
            {
                var textFileToDataTable = new Code.TextFileToDataTable();

                result = textFileToDataTable.Execute(
                    File.Get(context),
                    out LinesWithProblem,
                    HowManyLinesNeedToSkip.Get(context),
                    HasHeaders,
                    BreakColumns.Get(context),
                    BreakLines.Get(context),
                    ThrowExceptionIfThereIsProblemWithLine                    
                );
            }
            catch (Exception ex)
            {
                OutputDataTable.Set(context, null);                
                throw ex;
            }
            finally
            {
                OutputLinesWithProblem.Set(context, LinesWithProblem);
            }

            OutputDataTable.Set(context, result);
        }
    }    
}
