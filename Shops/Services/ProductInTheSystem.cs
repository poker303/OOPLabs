namespace Shops.Services
{
    public class ProductInTheSystem
    {
        private static int _currentId = 1;
        public ProductInTheSystem(string name)
        {
            NameOfProduct = name;
            Id = _currentId;
            _currentId++;
        }

        public int Id { get; }
        public string NameOfProduct { get; }
    }
}