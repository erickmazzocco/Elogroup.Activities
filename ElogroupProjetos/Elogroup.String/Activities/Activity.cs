using System.Activities;
using System.ComponentModel;

namespace Elogroup.String
{
    public abstract class Activity : CodeActivity
    {
        public Activity()
        {

        }

        public enum ThrowOptions
        {
            No,
            Yes                
        }

        [Category("Throw Error")]
        [DisplayName("InvalidCommand")]            
        [RequiredArgument]
        public ThrowOptions ThrowError { get; set; }
    }    
}
