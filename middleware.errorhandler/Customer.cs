using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace middleware.errorhandler
{
    public class Customer : ICustomer
    {
        public DateTime GetDate()
        {
            return DateTime.Now;
        }
    }
}
