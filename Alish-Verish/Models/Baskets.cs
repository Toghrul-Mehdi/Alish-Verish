using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alish_Verish.Models
{
    public class Baskets
    {
        public int Id { get; set; }
        public int UsersId { get; set; }
        public int ProductsId { get; set; }

        public Users Users { get; set; }

        public Products Products { get; set; }

       
    }
}
