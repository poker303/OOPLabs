namespace Shops.Services
{
    public class ProductInTheShop
    {
        public ProductInTheShop(ProductInTheSystem product, int numbers, int cost)
        {
            Product = product;
            Numbers = numbers;
            Cost = cost;
        }

        public int Cost { get; set; }
        public int Numbers { get; set; }
        public ProductInTheSystem Product { get; }
    }
}