namespace Quiz.Backend
{
    public class Product
    {
        public string Kod { get; set; }

        public string Produkt { get; set; }

        public decimal Cena { get; set; }

        public decimal Vat { get; set; }

    }

    public class Paragon
    {
        public int Kod { get; set; }
        public decimal Brutto { get; set; }
        public decimal Netto { get; set; }
    }

}
