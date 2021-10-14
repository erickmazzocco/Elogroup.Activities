using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elogroup.Utils.Code
{
    public class TextFileToDataTable
    {
        private string _file;
        private int _howManyLinesNeedToSkip;
        private bool _hasHeader;
        private string _breakColumns;
        private string _breakLines;
        private bool _ThrowExceptionIfThereIsProblemWithLine;        

        public DataTable Execute(
            string file,
            out List<string> LinesWithProblem,
            int howManyLinesNeedToSkip = 0,
            bool hasHeader = true,
            string breakColumns = ";",
            string breakLines = "\n",
            bool ThrowExceptionIfThereIsProblemWithLine = false            
            )
        {
            _file = file;
            _howManyLinesNeedToSkip = howManyLinesNeedToSkip;
            _hasHeader = hasHeader;
            _breakColumns = breakColumns;
            _breakLines = breakLines;
            _ThrowExceptionIfThereIsProblemWithLine = ThrowExceptionIfThereIsProblemWithLine;

            return GetDataTableByText(out LinesWithProblem);
        }

        public DataTable GetDataTableByText(out List<string> LinesWithProblem)
        {
            LinesWithProblem = new List<string>();

            var text = File.ReadAllText(_file);
            if (string.IsNullOrEmpty(text.Trim()))
                return new DataTable();

            var lines = text.Split(_breakLines.ToCharArray()).ToList();

            if (_howManyLinesNeedToSkip != 0)
                lines.RemoveAt(_howManyLinesNeedToSkip - 1);

            var dt = new DataTable();

            var firstLineColumns = lines.ElementAt(0).Split(_breakColumns.ToCharArray()).ToArray();
            if (_hasHeader)
            {
                foreach (var column in firstLineColumns)
                {
                    int count = 1;
                    var nameColumn = column;
                    while (dt.Columns.Contains(nameColumn))
                    {
                        count += 1;
                        nameColumn = column + " " + count;
                    }

                    dt.Columns.Add(nameColumn);
                }
            }
            else
            {
                for (int i = 0; i < firstLineColumns.Length; i++)
                {
                    dt.Columns.Add("Column " + (i + 1).ToString());
                }
            }

            var linesLength = lines.Skip(_hasHeader ? 1 : 0).Count();
            for (int i = 0; i < linesLength; i++)
            {
                var line = lines.ElementAt(i);

                var columns = line.Split(_breakColumns.ToCharArray());

                if (columns.Length != dt.Columns.Count)
                {
                    if (_ThrowExceptionIfThereIsProblemWithLine)
                        throw new Exception("Line don't have the same quantity of columns");

                    LinesWithProblem.Add("Line number: " + i + " Line Content: " + line);
                    continue;
                }

                dt.Rows.Add(columns);
            }

            return dt;
        }
    }
}
