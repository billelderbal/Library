using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class GetDictionaryFromList
    {
        public static Dictionary<T,U> GetDictionary<T,U>(List<U> list,string propertyKey)
        {
            Dictionary<T, U> dico = new Dictionary<T, U>();
           list.ForEach(
                x =>
                {
                    T key = (T)x.GetType().GetProperty(propertyKey).GetValue(x);
                    if (!dico.ContainsKey(key))
                        dico.Add(key, x);
                    else
                        throw new Exception("The file contains multiple the same key");

                }
                );
            return dico;
        }
    }
}
