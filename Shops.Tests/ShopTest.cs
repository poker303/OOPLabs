using System.Collections.Generic;
using System.Linq;
using Shops.Services;
using NUnit.Framework;

namespace Shops.Tests
{
    public class Tests
    {
        private ShopManager _shopManager;
        
        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void AddShop_AddProducts_BuyChecking()
        {
            var timeNamesOfProducts = new List<ProductInTheSystem>()
            {
                new ProductInTheSystem ("Water"), 
                new ProductInTheSystem ("Bread"),
                new ProductInTheSystem ("Juice"),
                new ProductInTheSystem ("Cola")
            };
            // foreach (Product t in timeNamesOfProducts)
            // {
                // _shopManager.RegisterProduct(t.NameOfProduct);
            // }
            _shopManager.RegisterProduct(timeNamesOfProducts);
            Shop timeShop = _shopManager.CreateShop("Magnit", "Pskovskay_Street");
            var timeShopNamesOfProducts = new List<ProductInTheShop>()
            {
                new ProductInTheShop (timeNamesOfProducts[0], 100, 39), 
                new ProductInTheShop (timeNamesOfProducts[1], 200, 20),
                new ProductInTheShop (timeNamesOfProducts[2], 50, 50),
                new ProductInTheShop (timeNamesOfProducts[3], 10, 80)
            };
            timeShop.AddProducts(timeShopNamesOfProducts);
            Assert.Contains(timeShop, _shopManager.Shops);
            foreach (ProductInTheSystem ter in timeNamesOfProducts)
            {
                Assert.Contains(ter, _shopManager.RegisteredProducts);
            }

            foreach (ProductInTheShop t in timeShopNamesOfProducts)
            {
                Assert.Contains(t, timeShop.AllProducts);
            }
        }

        [Test]
        public void Changing_Coasts()
        {
            var timeNamesOfProducts = new List<ProductInTheSystem>()
            {
                new ProductInTheSystem ("Water")
            };
            
            // foreach (Products2 t in timeNamesOfProducts)
            // {
            //     _shopManager.RegisterProduct(t.NameOfProduct);
            // }
            _shopManager.RegisterProduct(timeNamesOfProducts);
            var timeShopNamesOfProducts = new List<ProductInTheShop>()
            {
                new ProductInTheShop(timeNamesOfProducts[0], 100, 39)
            };

            Shop timeShop = _shopManager.CreateShop("Magnit", "Pskovskay_Street");
            // int oldCoast = timeNamesOfProducts[0].Coast;
            timeShop.AddProducts(timeShopNamesOfProducts);
            timeShop.ChangePrice(timeShopNamesOfProducts[0].Product, 56);
            Assert.AreEqual(56, timeShopNamesOfProducts[0].Coast);
        }

        [Test]
        public void Checking_Min_price()
        {
            var timeNamesOfProducts = new List<ProductInTheSystem>()
            {
                new ProductInTheSystem ("Water"),
                new ProductInTheSystem ("Milk"), 
                new ProductInTheSystem ("Ham"),
            };
            _shopManager.RegisterProduct(timeNamesOfProducts);
            
            Shop timeShop1 = _shopManager.CreateShop("Magnit", "Pskovskay_Street");
            var timeShopNamesOfProducts1 = new List<ProductInTheShop>()
            {
                new ProductInTheShop (timeNamesOfProducts[0], 100, 39),
                new ProductInTheShop (timeNamesOfProducts[1], 150, 90),
                new ProductInTheShop (timeNamesOfProducts[2], 20, 120),
            };
            timeShop1.AddProducts(timeShopNamesOfProducts1);
            
            // var timeNamesOfProducts2 = new List<Products2>()
            // {
            //     new Products2 ("Water"),
            //     new Products2 ("Milk"), 
            //     new Products2 ("Ham"),
            // };
            
            Shop timeShop2 = _shopManager.CreateShop("Piaterochka", "Lenina_Street");
            var timeShopNamesOfProducts2 = new List<ProductInTheShop>()
            {
                new ProductInTheShop (timeNamesOfProducts[0], 100, 70),
                new ProductInTheShop (timeNamesOfProducts[1], 150, 130),
                new ProductInTheShop (timeNamesOfProducts[2], 20, 100),
            };
            timeShop2.AddProducts(timeShopNamesOfProducts2);
        
            // var timeNamesOfProducts3 = new List<Products2>()
            // {
            //     new Products2 ("Water"),
            //     new Products2 ("Milk"), 
            //     new Products2 ("Ham"),
            // };

            Shop timeShop3 = _shopManager.CreateShop("Diksi", "Fovorskogo_Street");
            var timeShopNamesOfProducts3 = new List<ProductInTheShop>()
            {
                new ProductInTheShop(timeNamesOfProducts[0], 100, 120),
                new ProductInTheShop(timeNamesOfProducts[1], 150, 170),
                new ProductInTheShop(timeNamesOfProducts[2], 20, 85),
            };
            timeShop3.AddProducts(timeShopNamesOfProducts3);
            
            var timeRequiredProducts = new Dictionary<ProductInTheSystem, int>
            {
                {timeNamesOfProducts[0], 5},
                {timeNamesOfProducts[1], 2},
                {timeNamesOfProducts[2], 2}
            };
            Assert.AreEqual(_shopManager.MinimalPrice(timeRequiredProducts), timeShop1);
        }
        [Test]
        public void Changing_Buy()
        {
            var timeNamesOfProducts = new List<ProductInTheSystem>()
            {
                new ProductInTheSystem ("Water"), 
                new ProductInTheSystem ("Bread"),
                new ProductInTheSystem ("Juice"),
                new ProductInTheSystem ("Cola")
            };
            _shopManager.RegisterProduct(timeNamesOfProducts);
            Shop timeShop = _shopManager.CreateShop("Magnit", "Pskovskay_Street");
            
            var timeShopNamesOfProducts = new List<ProductInTheShop>()
            {
                new ProductInTheShop (timeNamesOfProducts[0], 100, 39), 
                new ProductInTheShop (timeNamesOfProducts[1], 200, 20),
                new ProductInTheShop (timeNamesOfProducts[2], 50, 50),
                new ProductInTheShop (timeNamesOfProducts[3], 10, 80)
            };
            timeShop.AddProducts(timeShopNamesOfProducts);
            
            var timeBuildProducts = new Dictionary<ProductInTheSystem, int>
            {
                {timeNamesOfProducts[0], 5},
                {timeNamesOfProducts[1], 2},
                {timeNamesOfProducts[2], 10},
                {timeNamesOfProducts[3], 2}
            };
            var testCustomer = new Person("Danilo Olegovich", 1000);
            int startPersonMoney = testCustomer.PersonMoney;
            int startShopMoney = timeShop.MoneyInTheShop;
            timeShop.Buy(testCustomer, timeBuildProducts);
            Assert.AreEqual(startPersonMoney-testCustomer.PersonMoney, 
                timeShop.MoneyInTheShop - startShopMoney);
            foreach (ProductInTheSystem a in timeBuildProducts.Keys)
            {
                // foreach (Product b in timeShop.AllProducts.Where(b => a == b.NameOfProduct))
                // {
                    Assert.AreEqual(timeBuildProducts[a], testCustomer.ShoppingСart[a]);
                // }
            }
        }
    }
}