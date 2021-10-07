using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager
    {
        private int _totalId = 1;

        public ShopManager()
        {
            Shops = new List<Shop>();

            RegisteredProducts = new List<ProductInTheSystem>();

            ShopsPrices = new Dictionary<Shop, int>();
        }

        public List<Shop> Shops { get; }
        public List<ProductInTheSystem> RegisteredProducts { get; }

        public Dictionary<Shop, int> ShopsPrices { get; }

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
            newshop.Id = _totalId;
            _totalId++;

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

        // public void Delivery(List<Products2> newProducts, Shop chosedShop)
        // {
        //     foreach (Products2 t in newProducts.Where(t => !chosedShop.AlreadyRegisteredProducts.Contains(t)))
        //     {
        //         chosedShop.AlreadyRegisteredProducts.Add(t);
        //     }
        // }
        public Shop MinimalPrice(Dictionary<ProductInTheSystem, int> requiredProducts)
        {
            Shop result = null;

            int flag = 0;
            int counter = 0;
            int minimalPrice = int.MaxValue;
            foreach (Shop shop in Shops)
            {
                foreach (ProductInTheSystem b in requiredProducts.Keys)
                {
                    foreach (ProductInTheShop c in shop.AllProducts.Where(c => b == c.Product))
                    {
                        flag++;
                        if (requiredProducts[b] <= c.NumbersOfProduct)
                        {
                            counter += requiredProducts[b] * c.Coast;
                        }
                        else
                        {
                            throw new ProductNumberException("We don't have enough numbers of product");
                        }
                    }

                    if (flag == 0)
                    {
                        throw new ProductNumberException("We don't have this product");
                    }

                    flag = 0;
                }

                ShopsPrices.Add(shop, counter);
                counter = 0;
            }

            foreach (Shop shop in Shops.Where(shop => ShopsPrices[shop] <= minimalPrice))
            {
                minimalPrice = ShopsPrices[shop];
                result = shop;
            }

            return result;
        }
    }
}