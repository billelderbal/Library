
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Store : IStore
    {
        public Dictionary<string,Catalog> Catalogs { get; set; }
        public Dictionary<string,Category> Categories { get; set; }
        public double Buy(params string[] basketByNames)
        {
            Dictionary<Category, Dictionary<Catalog, int>> DicoByCategory = new Dictionary<Category, Dictionary<Catalog, int>>();
            foreach(string name in basketByNames)
            {
                Catalog catalog;
                bool catalogExists= Catalogs.TryGetValue(name,out catalog);
                if (!catalogExists)
                    throw new Exception($"{catalog.Name} is unkown");
                    Category category;
                    bool categoryExists = Categories.TryGetValue(catalog.Category, out category);
                if (!categoryExists)
                    throw new Exception($"{category.Name} dosen't exists");
                    GenerateDico(DicoByCategory, catalog, category);
                

            }
            try
            {
                return GetPrices(DicoByCategory);
            }
            catch(NotEnoughInventoryException ex )
            {
                throw ex;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private double GetPrices(Dictionary<Category, Dictionary<Catalog, int>> dicoByCategory)
        {
            List<INameQuantity> missingList=null;
            bool missing = false;
            double price = 0;
            foreach(KeyValuePair<Category,Dictionary<Catalog,int>> keyValuePair in dicoByCategory)
            {
                if (keyValuePair.Value.Count > 1)
                {
                    foreach (KeyValuePair<Catalog,int> item in keyValuePair.Value)
                    {
                        
                        if (item.Key.Quantity < item.Value)
                        {
                            if (missingList == null)
                                missingList = new List<INameQuantity>();
                            missingList.Add(item.Key);
                            missing = true;
                        }

                        if (missing)
                            continue;
                       
                        if (item.Value > 1)
                            price+= (item.Key.Price *(1- keyValuePair.Key.Discount)) + ((item.Value - 1) * item.Key.Price);
                        else
                            price+= item.Key.Price * (1 - keyValuePair.Key.Discount);
                    }
                }
                else if (keyValuePair.Value.Count == 1)
                {
                    price += keyValuePair.Value.ElementAt(0).Key.Price;
                }
            }
            if (missing)
                throw new NotEnoughInventoryException(missingList);

            return price;
        }
        private void GenerateDico(Dictionary<Category, Dictionary<Catalog, int>> dicoByCategory,Catalog catalog,Category category)
        {
            Dictionary<Catalog, int> catInDeco;
            bool categoryAlreadyExists= dicoByCategory.TryGetValue(category,out catInDeco);
            if (dicoByCategory.ContainsKey(category))
            {
                if (catInDeco.ContainsKey(catalog))
                {
                    int requiredQuantity;
                    bool quantityExist = catInDeco.TryGetValue(catalog, out requiredQuantity);
                    if (quantityExist)
                        catInDeco[catalog] = ++requiredQuantity;
                }
                else
                    catInDeco.Add(catalog, 1);
            }
            else
            {
                Dictionary<Catalog, int> dicoCatalog = new Dictionary<Catalog, int>();
                dicoCatalog.Add(catalog, 1);
                dicoByCategory.Add(category, dicoCatalog);

            }

        }

        public void Import(string catalogAsJson)
        {
            StreamReader reader;
            try
            {
                reader = new StreamReader(catalogAsJson);
            }
            catch(Exception e)
            {
                throw;
            }
            using (reader)
            {
                string JsonText = reader.ReadToEnd();
               StockStore stockStore = JsonConvert.DeserializeObject<StockStore>(JsonText);
               Catalogs= GetDictionaryFromList.GetDictionary<string, Catalog>(stockStore.Catalog, "Name");
               Categories= GetDictionaryFromList.GetDictionary<string, Category>(stockStore.Category, "Name");
               stockStore = null;
            }
        }

        public int Quantity(string name)
        {
            Catalog catalog;
            bool IsExist= Catalogs.TryGetValue(name, out catalog);
            if (IsExist)
                return catalog.Quantity;
            return 0;
        }
    }
}
