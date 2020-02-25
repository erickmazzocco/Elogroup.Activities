using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elogroup.StringActivities
{
    public abstract class StringActivity : CodeActivity
    {
        public enum ThrowOptions
        {
            No,
            Yes                
        }

        [Category("Output")]
        [Description("Boolean result for activity")]
        [DisplayName("Valid Activity")]
        public OutArgument<bool> ValidActivity { get; set; }

        [Category("Throw Error")]
        [DisplayName("If a exception happens")]            
        [RequiredArgument]
        public ThrowOptions ThrowError { get; set; }
    }    
}
