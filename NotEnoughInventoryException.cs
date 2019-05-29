using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class NotEnoughInventoryException : Exception
    {
        public IEnumerable<INameQuantity> Missing { get; }

        public NotEnoughInventoryException(IEnumerable<INameQuantity> missing)
        {
            Missing = missing;
        }
        
    }
}
