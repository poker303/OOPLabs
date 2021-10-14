namespace Shops.Services
{
    public class OrderedProducts
    {
        public OrderedProducts(ProductInTheSystem product, int numbers)
        {
            Product = product;
            Numbers = numbers;
        }

        public int Numbers { get; set; }
        public ProductInTheSystem Product { get; }
    }
}