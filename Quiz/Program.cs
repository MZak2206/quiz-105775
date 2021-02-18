using Quiz.Backend;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiz
{
    class Program
    {
        // Prototype of Product -> Saving User's Options
        static List<Paragon> Receipts = new List<Paragon> { };

        static void Main()
        {
            Till Till = new Till();
            UserSelect(Till);
        }

        static void UserSelect(Till Till)
        {
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("a. zakupy, ");
            Console.WriteLine("b. lista produktów,");
            Console.WriteLine("c. lista paragonów");

            string UserSelect = Console.ReadLine();

            switch (UserSelect)
            {
                case "a":
                    shopping(Till);
                    break;
                case "b":
                    productList(Till);
                    break;
                case "c":
                    receiptList();
                    break;
            }
        }

        static void shopping(Till Till)
        {
            Console.WriteLine("Podaj Kod Kreskowy");

            string KodKreskowy = Console.ReadLine();

            Product WybranyProdukt = new Product();

            bool prawidlowyKod = false;

            while(!prawidlowyKod)
            {
                if (Till.Products.Exists(product => product.Kod == KodKreskowy))
                {
                    WybranyProdukt = Till.Products.Find(product => product.Kod == KodKreskowy);
                    prawidlowyKod = true;
                } else
                {
                    Console.WriteLine("Podaj Poprawny Kod Kreskowy :)");
                    KodKreskowy = Console.ReadLine();
                }
            }

            Console.WriteLine(WybranyProdukt.Produkt);
            Console.WriteLine("Podaj ilość");

            string ilosc = Console.ReadLine();

            WybranyProdukt.Cena = WybranyProdukt.Cena * Int32.Parse(ilosc);

            Paragon Paragon = new Paragon();

            Paragon.Kod = Receipts.Count() + 1;
            Paragon.Netto = WybranyProdukt.Cena;
            Paragon.Brutto = Math.Round(WybranyProdukt.Cena + WybranyProdukt.Cena * WybranyProdukt.Vat, 2);

            Receipts.Add(Paragon);


            Console.WriteLine("Nastepny produkt(N)");
            Console.WriteLine("Wydruk paragonu(P)");

            string Akcja = Console.ReadLine();

            switch (Akcja)
            {
                case "N":
                    shopping(Till);
                    break;
                case "P":
                    receiptList();
                    break;
            }
        }

        static void productList(Till Till)
        {
            Console.WriteLine("Lista Produktów:");
            Till.Products.ForEach(product =>
            {
                Console.WriteLine($"{product.Kod}. {product.Produkt} - {product.Cena} PLN");
            });
        }

        static void receiptList()
        {

            if (Receipts.Count() != 0) {
                Console.WriteLine("Lista Paragonów:");
                Receipts.ForEach(product =>
                {
                    Console.WriteLine($"{product.Kod}. Kwota brutto: {product.Brutto} PLN, Netto: {product.Netto} PLN");
                });
             }
            else
            {
                Console.WriteLine("Brak Paragonów. Naciśnij (Z) aby dokonać swojego pierwszego zakupu");

                string Input = Console.ReadLine();

                if (Input == "Z")
                {
                    shopping(new Till());
                } 
            };
        }

    }
}
