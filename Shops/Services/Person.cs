using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Services
{
    public class Person
    {
        public Person(string name, int money)
        {
            PersonName = name;
            if (money >= 0)
            {
                Money = money;
            }
            else
            {
                throw new MoneyException("Create a person with a positive amount of money");
            }

            ShoppingСart = new List<OrderedProducts>();
        }

        public string PersonName { get; }
        public int Money { get; private set; }
        public List<OrderedProducts> ShoppingСart { get; }

        public void ChangingMoney(int numbers, int cost)
        {
            Money -= numbers * cost;
        }
    }
}