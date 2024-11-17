using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alish_Verish.MyException
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException()
        {
        }
        public ProductNotFoundException(string message)
            : base(message)
        {
        }
    }
}
