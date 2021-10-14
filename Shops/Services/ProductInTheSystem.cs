namespace Shops.Services
{
    public class ProductInTheSystem
    {
        private static int _currentId = 0;
        public ProductInTheSystem(string name)
        {
            Name = name;
            Id = _currentId++;
        }

        public int Id { get; }
        public string Name { get; }
    }
}