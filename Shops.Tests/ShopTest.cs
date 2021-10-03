using System.Collections.Generic;
using System.Linq;
using Shops.Services;
using Shops.Tools;
using NUnit.Framework;

namespace Shops.Tests
{
    public class Tests
    {
        private ShopManager _shopManager;
        
        [SetUp]
        public void Setup()
        {
            //fixed: implement
            _shopManager = new ShopManager();
        }

        [Test]
        public void AddShop_AddProducts_BuyChecking()
        {
            var timeNamesOfProducts = new List<Product>()
            {
                new Product ("Water", 100, 39), 
                new Product ("Bread", 200, 20),
                new Product ("Juice", 50, 50),
                new Product ("Cola", 10, 80)
            };
            for (int i = 0; i < timeNamesOfProducts.Count; i++)
            {
                _shopManager.RegisterProduct(timeNamesOfProducts[i].NameOfProduct);
            }
            Shop timeShop = _shopManager.CreateShop("Magnit", "Pskovskay_Street");
            timeShop.AddProducts(timeNamesOfProducts);
            Assert.Contains(timeShop, _shopManager.Shops);
            Assert.Contains(timeNamesOfProducts[0].NameOfProduct, _shopManager.NamesOfProducts);
            for (int i = 0; i < timeNamesOfProducts.Count; i++)
            {
                Assert.Contains(timeNamesOfProducts[i], timeShop.AllProducts);
            }
        }

        [Test]
        public void Changing_Coasts()
        {
            var timeNamesOfProducts = new List<Product>()
            {
                new Product ("Water", 100, 39)
            };
            for (int i = 0; i < timeNamesOfProducts.Count; i++)
            {
                _shopManager.RegisterProduct(timeNamesOfProducts[i].NameOfProduct);
            }
            Shop timeShop = _shopManager.CreateShop("Magnit", "Pskovskay_Street");
            // int oldCoast = timeNamesOfProducts[0].Coast;
            timeShop.AddProducts(timeNamesOfProducts);
            timeShop.ChangePrice("Water", 56);
            Assert.AreEqual(56, timeNamesOfProducts[0].Coast);
        }

        [Test]
        public void Chekong_Min_price()
        {
            var timeNamesOfProducts1 = new List<Product>()
            {
                new Product ("Water", 100, 39),
                new Product ("Bread", 200, 20),
                new Product ("Juice", 50, 50),
                new Product ("Cola", 10, 80),
                new Product ("Milk", 150, 90),
                new Product ("Ham", 20, 120),
                new Product ("Shish", 1000, 20)
            };
            for (int i = 0; i < timeNamesOfProducts1.Count; i++)
            {
                _shopManager.RegisterProduct(timeNamesOfProducts1[i].NameOfProduct);
            }
            Shop timeShop1 = _shopManager.CreateShop("Magnit", "Pskovskay_Street");
            timeShop1.AddProducts(timeNamesOfProducts1);
            
            var timeNamesOfProducts2 = new List<Product>()
            {
                new Product ("Water", 100, 70),
                new Product ("Milk", 150, 130),
                new Product ("Ham", 20, 100),
                new Product ("Shish", 1000, 35)
            };
            for (int i = 0; i < timeNamesOfProducts2.Count; i++)
            {
                _shopManager.RegisterProduct(timeNamesOfProducts2[i].NameOfProduct);
            }
            Shop timeShop2 = _shopManager.CreateShop("Piaterochka", "Lenina_Street");
            timeShop2.AddProducts(timeNamesOfProducts2);

            var timeNamesOfProducts3 = new List<Product>()
            {
                new Product ("Water", 100, 120),
                new Product ("Milk", 150, 170),
                new Product ("Ham", 20, 85),
                new Product ("Shish", 1000, 8),
                new Product ("Salt", 90, 15),
                new Product ("Cookies", 200, 65),
            };
            for (int i = 0; i < timeNamesOfProducts3.Count; i++)
            {
                _shopManager.RegisterProduct(timeNamesOfProducts3[i].NameOfProduct);
            }
            Shop timeShop3 = _shopManager.CreateShop("Diksi", "Fovorskogo_Street");
            timeShop3.AddProducts(timeNamesOfProducts3);
            
            var timeRequiredProducts = new Dictionary<string, int>
            {
                {"Water", 5},
                {"Milk", 2},
                {"Shish", 10},
                {"Ham", 2}
            };
            Assert.AreEqual(_shopManager.MinimalPrice(timeRequiredProducts), timeShop1);
        }
        [Test]
        public void Changing_Buy()
        {
            var timeNamesOfProducts = new List<Product>()
            {
                new Product ("Water", 100, 39),
                new Product ("Bread", 200, 20),
                new Product ("Juice", 50, 50),
                new Product ("Cola", 10, 80),
                new Product ("Milk", 150, 90),
                new Product ("Ham", 20, 120),
                new Product ("Shish", 1000, 20)
            };
            for (int i = 0; i < timeNamesOfProducts.Count; i++)
            {
                _shopManager.RegisterProduct(timeNamesOfProducts[i].NameOfProduct);
            }
            Shop timeShop = _shopManager.CreateShop("Magnit", "Pskovskay_Street");
            timeShop.AddProducts(timeNamesOfProducts);
            
            var timeBuildProducts = new Dictionary<string, int>
            {
                {"Water", 5},
                {"Milk", 2},
                {"Shish", 10},
                {"Ham", 2}
            };
            var testCustomer = new Person("Danilo Olegovich", 1000);
            int startPersonMoney = testCustomer.PersonMoney;
            int startShopMoney = timeShop.MoneyInTheShop;
            timeShop.Buy(testCustomer, timeBuildProducts);
            Assert.AreEqual(startPersonMoney-testCustomer.PersonMoney, 
                timeShop.MoneyInTheShop - startShopMoney);
            foreach (string a in timeBuildProducts.Keys)
            {
                foreach (Product b in timeShop.AllProducts)
                {
                    if (a == b.NameOfProduct)
                    {
                        Assert.AreEqual(timeBuildProducts[a], testCustomer.ShoppingСart[a]);
                    }
                }
            }
        }
    }
}