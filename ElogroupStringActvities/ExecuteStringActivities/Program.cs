using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExecuteStringActivities
{
    class Program
    {
        static void Main(string[] args)
        {
            var oi = new Elogroup.StringActivities.RemoveSpecialCharacters();
            oi.IgnoreBlankSpace = true;
            var s = oi.ExecuteMethod("eri#$% ck");
            var ss = "";
        }
    }
}
