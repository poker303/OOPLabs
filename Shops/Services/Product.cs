namespace Shops.Services
{
    public class Product
    {
        public Product(string name, int numbers, int coast)
        {
            NameOfProduct = name;
            NumbersOfProduct = numbers;
            Coast = coast;
        }

        public int Coast { get; set; }
        public int NumbersOfProduct { get; set; }
        public string NameOfProduct { get; }
    }
}