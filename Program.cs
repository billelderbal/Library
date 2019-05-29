using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store();
            store.Import("Test.json");
            int quant = store.Quantity("Ayn Rand - FountainHead");
            double prix;
            try
            {
                prix = store.Buy("Isaac Asimov - Robot series", "Isaac Asimov - Foundation", "Robin Hobb - Assassin Apprentice", "Robin Hobb - Assassin Apprentice");
            }
            catch(NotEnoughInventoryException ex)
            {
                foreach (var item in ex.Missing)
                    Console.WriteLine($"{item.Name} not enough in store");
            }
            }
    }
}
