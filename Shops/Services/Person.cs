﻿using System.Collections.Generic;

namespace Shops.Services
{
    public class Person
    {
        public Person(string name, int money)
        {
            PersonName = name;
            PersonMoney = money;
            ShoppingСart = new Dictionary<string, int>();
        }

        public string PersonName { get; }
        public int PersonMoney { get; set; }
        public Dictionary<string, int> ShoppingСart { get; }
    }
}