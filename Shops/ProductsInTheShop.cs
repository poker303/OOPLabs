namespace Shops
{
    public class ProductsInTheShop
    {
        public ProductsInTheShop(string name, int numbers, int coast)
        {
            NamesOfProduct = name;
            NumbersOfProduct = numbers;
            Coast = coast;
        }

        public int Coast { get; set; }
        public int NumbersOfProduct { get; set; }
        public string NamesOfProduct { get; set; }
    }
}