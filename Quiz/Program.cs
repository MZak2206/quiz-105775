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
        static List<Product> AddedProducts = new List<Product> { };

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
            WybranyProdukt.ilosc = ilosc;

            AddedProducts.Add(WybranyProdukt);



            Paragon Paragon = new Paragon();

            Paragon.Kod = Receipts.Count() + 1;
           

            


            Console.WriteLine("Nastepny produkt(N)");
            Console.WriteLine("Wydruk paragonu(P)");

            string Akcja = Console.ReadLine();

            switch (Akcja)
            {
                case "N":
                    shopping(Till);
                    break;
                case "n":
                    shopping(Till);
                    break;
                case "P":
                    showReceipt(Paragon);

                    break;
                case "p":
                    showReceipt(Paragon);

                    break;
            }
        }

        static void showReceipt(Paragon paragon)
        {
            double netto = 0;
            double total = 0;
            double vat8 = 0;
            double vat23 = 0;
            Console.WriteLine($"Data sprzedazy: {DateTime.Now.ToString("dd.MM.yyy")}");
            Console.WriteLine($"Numer Paragonu: {paragon.Kod}");
            Console.WriteLine("---------------------");
            AddedProducts.ForEach(product =>
            {
                Console.WriteLine($"{product.Produkt} {product.ilosc} {Math.Round(product.Cena + product.Cena * product.Vat, 2)}");
                netto += product.Cena;
                total += Math.Round(product.Cena + product.Cena * product.Vat, 2);
                switch (product.Vat)
                {
                    case 0.08:
                        vat8+=Math.Round(product.Cena * product.Vat, 2);
                        break;
                    case 0.23:
                        vat23 += Math.Round(product.Cena * product.Vat, 2);
                        break;
                }

            });
            Console.WriteLine($"Łacznie do zaplaty: {total} PLN");
            Console.WriteLine("w tym:");
            Console.WriteLine($"Vat 8% {vat8} PLN");
            Console.WriteLine($"Vat 23% {vat23} PLN");

            paragon.Netto = netto;
            paragon.Brutto = total;
            Receipts.Add(paragon);
            UserSelect(new Till());
        }


        static void productList(Till Till)
        {
            Console.WriteLine("Lista Produktów:");
            Till.Products.ForEach(product =>
            {
                Console.WriteLine($"{product.Kod}. {product.Produkt} - {product.Cena} PLN");

            });
            UserSelect(Till);
        }

        static void receiptList()
        {

            if (Receipts.Count() != 0) {
                Console.WriteLine("Lista Paragonów:");
                Receipts.ForEach(product =>
                {
                    Console.WriteLine($"{product.Kod}. Kwota brutto: {product.Brutto} PLN, Netto: {product.Netto} PLN");
                    
                });
                UserSelect(new Till());
            }
            else
            {
                Console.WriteLine("Brak Paragonów. Naciśnij (Z) aby dokonać swojego pierwszego zakupu");

                string Input = Console.ReadLine();

                if (Input == "Z" || Input == "z")
                {
                    shopping(new Till());
                } 
            };
            
        }

    }
}
