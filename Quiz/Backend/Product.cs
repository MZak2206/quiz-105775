namespace Quiz.Backend
{
    public class Product
    {
        public string Kod { get; set; }

        public string Produkt { get; set; }

        public double Cena { get; set; }

        public double Vat { get; set; }

        public string ilosc { get; set; }

    }

    public class Paragon
    {
        public int Kod { get; set; }
        public double Brutto { get; set; }
        public double Netto { get; set; }
    }

}
