
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Category
    {
        public string Name { get; set; }
        public double Discount { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Category category = obj as Category;
            if (category == null)
                return false;
            return category.Name.Equals(this.Name);
        }
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
