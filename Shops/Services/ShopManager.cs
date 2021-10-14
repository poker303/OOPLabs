using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager
    {
        public ShopManager()
        {
            Shops = new List<Shop>();

            RegisteredProducts = new List<ProductInTheSystem>();
        }

        public List<Shop> Shops { get; }
        public List<ProductInTheSystem> RegisteredProducts { get; }

        public void RegisterProduct(List<ProductInTheSystem> productsForRegistration)
        {
            foreach (ProductInTheSystem a in productsForRegistration.Where(
                a => !RegisteredProducts.Contains(a)))
            {
                RegisteredProducts.Add(a);
            }
        }

        public Shop CreateShop(string shopName, string adress)
        {
            var newshop = new Shop(shopName, adress);
            newshop.AlreadyRegisteredProducts = RegisteredProducts;
            Shops.Add(newshop);
            return newshop;
        }

        public void AdditionalRegisterProduct(/*Delivery */ List<ProductInTheSystem> newProducts, Shop chosedShop)
        {
            foreach (ProductInTheSystem t in newProducts.Where(
                t => !chosedShop.AlreadyRegisteredProducts.Contains(t)))
            {
                chosedShop.AlreadyRegisteredProducts.Add(t);
            }
        }

        public void AddProducts(List<ProductInTheShop> newProducts, Shop chosedShop)
        {
            foreach (ProductInTheShop t in newProducts)
            {
                if (!RegisteredProducts.Contains(t.Product))
                {
                    throw new RegistrationException("Product wasn't registred");
                }
            }

            chosedShop.AddProducts(newProducts);
        }

        public Shop MinimalPrice(List<OrderedProducts> requiredProducts)
        {
            var shopsPrices = new Dictionary<Shop, int>();
            Shop result = null;

            int numberOfAvailableProducts = 0;
            int counter = 0;
            int minimalPrice = int.MaxValue;
            foreach (Shop shop in Shops)
            {
                foreach (OrderedProducts b in requiredProducts)
                {
                    foreach (ProductInTheShop c in shop.AllProducts.Where(c => b.Product == c.Product))
                    {
                        numberOfAvailableProducts++;
                        if (b.Numbers <= c.Numbers)
                        {
                            counter += b.Numbers * c.Cost;
                        }
                        else
                        {
                            numberOfAvailableProducts--;
                        }

                        break;
                    }
                }

                if (numberOfAvailableProducts == requiredProducts.Count)
                {
                    shopsPrices.Add(shop, counter);
                }

                counter = 0;
                numberOfAvailableProducts = 0;
            }

            if (shopsPrices.Count != 0)
            {
                foreach (Shop shop in Shops.Where(shop => shopsPrices[shop] <= minimalPrice))
                {
                    minimalPrice = shopsPrices[shop];
                    result = shop;
                }
            }
            else
            {
                    throw new ShopsException("There is no such set of quantity of goods in any store");
            }

            return result;
        }
    }
}