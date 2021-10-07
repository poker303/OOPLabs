namespace Shops.Services
{
    public class ProductInTheShop
    {
        public ProductInTheShop(ProductInTheSystem product, int numbers, int coast)
        {
            Product = product;
            NumbersOfProduct = numbers;
            Coast = coast;
        }

        public int Coast { get; set; }
        public int NumbersOfProduct { get; set; }
        public ProductInTheSystem Product { get; }
    }
}