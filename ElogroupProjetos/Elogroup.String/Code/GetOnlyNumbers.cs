using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Elogroup.String.Code
{
    public class GetOnlyNumbers
    {
        public GetOnlyNumbers()
        {

        }

        public string Execute(string text)
        {
            var result = Regex.Replace(text, @"[^\d]", string.Empty);           
            
            return result;
        }
    }
}
