using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alish_Verish.MyException
{
    internal class UserNotFoundException : Exception
    {
        
        public UserNotFoundException()
        {
        }        
        public UserNotFoundException(string message)
            : base(message)
        {
        }
    }
}
