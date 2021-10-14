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

            _shopManager.RegisterProduct(timeNamesOfProducts);
            Shop timeShop = _shopManager.CreateShop("Magnit", "Pskovskay_Street");
            var timeShopNamesOfProducts = new List<ProductInTheShop>()
            {
                new ProductInTheShop (timeNamesOfProducts[0], 100, 39), 
                new ProductInTheShop (timeNamesOfProducts[1], 200, 20),
                new ProductInTheShop (timeNamesOfProducts[2], 50, 50),
                new ProductInTheShop (timeNamesOfProducts[3], 10, 80)
            };
            _shopManager.AddProducts(timeShopNamesOfProducts, timeShop);
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

            _shopManager.RegisterProduct(timeNamesOfProducts);
            var timeShopNamesOfProducts = new List<ProductInTheShop>()
            {
                new ProductInTheShop(timeNamesOfProducts[0], 100, 39)
            };

            Shop timeShop = _shopManager.CreateShop("Magnit", "Pskovskay_Street");
            _shopManager.AddProducts(timeShopNamesOfProducts, timeShop);
            timeShop.AddProducts(timeShopNamesOfProducts);
            timeShop.ChangePrice(timeShopNamesOfProducts[0], 56);
            Assert.AreEqual(56, timeShopNamesOfProducts[0].Cost);
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
            _shopManager.AddProducts(timeShopNamesOfProducts1, timeShop1);
            timeShop1.AddProducts(timeShopNamesOfProducts1);

            Shop timeShop2 = _shopManager.CreateShop("Piaterochka", "Lenina_Street");
            var timeShopNamesOfProducts2 = new List<ProductInTheShop>()
            {
                new ProductInTheShop (timeNamesOfProducts[0], 100, 70),
                new ProductInTheShop (timeNamesOfProducts[1], 150, 130),
                new ProductInTheShop (timeNamesOfProducts[2], 20, 100),
            };
            _shopManager.AddProducts(timeShopNamesOfProducts2, timeShop2);
            timeShop2.AddProducts(timeShopNamesOfProducts2);

            Shop timeShop3 = _shopManager.CreateShop("Diksi", "Fovorskogo_Street");
            var timeShopNamesOfProducts3 = new List<ProductInTheShop>()
            {
                new ProductInTheShop(timeNamesOfProducts[0], 100, 120),
                new ProductInTheShop(timeNamesOfProducts[1], 150, 170),
                new ProductInTheShop(timeNamesOfProducts[2], 20, 85),
            };
            _shopManager.AddProducts(timeShopNamesOfProducts3, timeShop3);
            timeShop3.AddProducts(timeShopNamesOfProducts3);
            
            var timeRequiredProducts = new List<OrderedProducts>
            {
                new OrderedProducts(timeNamesOfProducts[0], 5),
                new OrderedProducts(timeNamesOfProducts[1], 2),
                new OrderedProducts(timeNamesOfProducts[2], 2)
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
            _shopManager.AddProducts(timeShopNamesOfProducts, timeShop);
            timeShop.AddProducts(timeShopNamesOfProducts);
            
            var timeBuildProducts = new List<OrderedProducts>
            {
                new OrderedProducts(timeNamesOfProducts[0], 5),
                new OrderedProducts(timeNamesOfProducts[1], 2),
                new OrderedProducts(timeNamesOfProducts[2], 10),
                new OrderedProducts(timeNamesOfProducts[3], 2)
            };
            var testCustomer = new Person("Danilo Olegovich", 1000);
            int startPersonMoney = testCustomer.Money;
            int startShopMoney = timeShop.Money;
            timeShop.Buy(testCustomer, timeBuildProducts);
            Assert.AreEqual(startPersonMoney-testCustomer.Money, 
                timeShop.Money - startShopMoney);
            foreach (OrderedProducts a in timeBuildProducts)
            {
                Assert.Contains(a, testCustomer.ShoppingСart);
            }
        }
    }
}