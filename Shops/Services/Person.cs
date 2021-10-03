using System.Collections.Generic;

namespace Shops.Services
{
    public class Person
    {
        // private Dictionary<string, int> _shoppingСart = new Dictionary<string, int>();
        public Person(string name, int money)
        {
            PersonName = name;
            PersonMoney = money;
            ShoppingСart = new Dictionary<string, int>();
        }

        public string PersonName { get; set; }
        public int PersonMoney { get; set; }
        public Dictionary<string, int> ShoppingСart { get; set; }
    }
}