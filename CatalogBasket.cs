using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class CatalogBasket
    {
        public Catalog Catalog { get; set; }
        public int RequiredQuantity { get; private set; }
        public void AddCatalog(Catalog catalog)
        {
            if (Catalog.Equals(catalog))
                this.RequiredQuantity++;
        }

        public CatalogBasket(Catalog catalog)
        {
            Catalog = catalog;
            RequiredQuantity++;
        }
    }
}
