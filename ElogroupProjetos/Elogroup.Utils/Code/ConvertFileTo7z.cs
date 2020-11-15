using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elogroup.Utils.Code
{
    class ConvertFileTo7z
    {
        private const string Default7zExePath = @"C:\Program Files\7-Zip\7z.exe";        
        private bool _deleteOriginalFile;
        private bool _overwriteExistingFile;
        private string _exe7z;
        private string _newFileName;

        public string Execute(string originalFileName, string exe7z, int timeout, bool deleteOriginalFile, bool overwriteExistingFile)
        {
            _deleteOriginalFile = deleteOriginalFile;
            _overwriteExistingFile = overwriteExistingFile;
            _exe7z = exe7z;            

            ValidateFileExists(originalFileName);
            ValidateExe7zExists();

            var parameters = GenerateParameters(originalFileName);

            var result = ConvertFile(parameters, timeout);
            
            return result;
        }

        private string ConvertFile(string parameters, int timeout)
        {
            try
            {
                ProcessStartInfo pro = new ProcessStartInfo();
                pro.WindowStyle = ProcessWindowStyle.Hidden;
                pro.FileName = _exe7z;
                pro.Arguments = parameters;
                Process x = Process.Start(pro);
                
                if(timeout > 0)
                    x.WaitForExit(timeout);
                else
                    x.WaitForExit();                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _newFileName;
        }        

        private string GenerateParameters(string originalFilename)
        {
            var originalFile = new FileInfo(originalFilename);

            _newFileName = GenerateOutputFileNameAndValidate(originalFile);            

            string parameters = "a ";
            parameters += _deleteOriginalFile ? "-sdel " : " ";
            parameters += _newFileName + " ";
            parameters += originalFile.FullName;

            return parameters;
        }

        private string GenerateOutputFileNameAndValidate(FileInfo file)
        {
            var newFile = file.FullName.Replace(file.Extension, ".7z");

            if (File.Exists(newFile))
            {
                if (_overwriteExistingFile)
                {
                    File.Delete(newFile);
                } else
                {
                    throw new Exception("The output file already exists");
                }
            }            
            
            return newFile;                
        } 

        private void ValidateFileExists(string file)
        {
            if(File.Exists(file))
                return;
            else
                throw new Exception("File not found");
        }

        private void ValidateExe7zExists()
        {
            if(string.IsNullOrEmpty(_exe7z))
                _exe7z = Default7zExePath;

            if(File.Exists(_exe7z))
                return;
            else
                throw new Exception("7z executable not found");
        }
    }
}
