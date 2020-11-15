using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elogroup.Utils
{
    [DisplayName("Convert File to 7z")]
    [Description("Convert a file into a 7z file")]
    public class ConvertFileTo7z : CodeActivity
    {
        #region Arguments
        [Category("Input")]        
        [RequiredArgument]
        [Description("Full path of the file that will be convert")]        
        public InArgument<string> File { get; set; }

        [Category("Input")]        
        [Description(@"Location of 7z.exe. If the location is not set, the default location that will use is C:\Program Files\7-Zip\7z.exe")]
        [DefaultValue("")]
        public InArgument<string> Exe7z { get; set; }

        [Category("Input")]        
        [Description(@"Set the timeout in milliseconds. If the timeout is not set, the default value is the necessary time for convert the file")]
        [DefaultValue(0)]
        public InArgument<int> Timeout { get; set; }

        [Category("Options")]
        [DefaultValue(false)]
        [Description("Delete original file after be converted")]        
        public bool DeleteOriginalFile { get; set; }

        [Category("Options")]
        [DefaultValue(false)]
        [Description("Overwrite the output file if it exists")]        
        public bool OverwriteExistingFile { get; set; }

        [Category("Output")]        
        public OutArgument<string> OutputFileName { get; set; }
        #endregion
               
        protected override void Execute(CodeActivityContext context)
        {
            string result;
            try
            {
                var convertFile = new Code.ConvertFileTo7z();

                result = convertFile.Execute(
                    File.Get(context),
                    Exe7z.Get(context),
                    Timeout.Get(context),
                    DeleteOriginalFile,
                    OverwriteExistingFile                    
                );                
            }
            catch (Exception ex)
            {
                OutputFileName.Set(context, string.Empty);                

                throw ex;
            }

            OutputFileName.Set(context, result);
        }  
    }


   
}
