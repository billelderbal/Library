using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
     class Catalog:INameQuantity
    {
        #region Properties

        public string Category { get; set; }
        public double Price { get; set; }
        public string Name { get; }
        public int Quantity { get;}
        #endregion
        public Catalog(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Catalog catalog = obj as Catalog;
            if (catalog == null)
                return false;
            return catalog.Name.Equals(this.Name);
        }
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
