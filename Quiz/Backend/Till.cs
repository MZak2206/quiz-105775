using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;

namespace Quiz.Backend
{
    public class Till
    {
        public Till()
        {
            createDatabase();
        }
       
        public List<Product> Products { get; set; }



        private void createDatabase()
        {
            // TODO: Zmień na funkcje zwracającą PathName
            string pathname = $"{Directory.GetCurrentDirectory()}/baza_kasa.json";

            string dbProducts = File.ReadAllText(pathname);

            Products = JsonConvert.DeserializeObject<List<Product>>(dbProducts);
        }
    }
}
