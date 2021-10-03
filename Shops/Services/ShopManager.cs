using System.Collections.Generic;
using System.Net.Security;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager
    {
        private int _totalId = 1;
        public ShopManager()
        {
            Shops = new List<Shop>();

            // Products = new List<Product>();
            NamesOfProducts = new List<string>();

            ShopsPrices = new Dictionary<Shop, int>();
        }

        public List<Shop> Shops { get; set; }

        // public List<Product> Products { get; set; }
        public List<string> NamesOfProducts { get; set; }

        public Dictionary<Shop, int> ShopsPrices { get; set; }

        public void RegisterProduct(string name)
        {
            // foreach (Product a in Products)
            // {
            //     if (a.NameOfProduct == name)
            //     {
            //     }
            //     else
            //     {
            //         Product added_product = new Product(name, 0, 0);
            //         Products.Add(added_product);
            //     }
            // }
            if (!NamesOfProducts.Contains(name))
            {
                NamesOfProducts.Add(name);
            }
        }

        public Shop CreateShop(string shopName, string adress)
        {
            // Shop.Name = shopName;
            var newshop = new Shop(shopName, adress);
            newshop.Id = _totalId;
            _totalId++;

            newshop.AlreadyRegisteredProducts = NamesOfProducts;
            Shops.Add(newshop);
            return newshop;
        }

        public Shop MinimalPrice(Dictionary<string, int> requiredProducts)
        {
            Shop result = null;

            int flag = 0;
            int counter = 0;
            double minimalPrice = 1000000000000000;
            foreach (Shop shop in Shops)
            {
                foreach (string b in requiredProducts.Keys)
                {
                    foreach (Product c in shop.AllProducts)
                    {
                        if (b == c.NameOfProduct)
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

            foreach (Shop shop in Shops)
            {
                if (ShopsPrices[shop] <= minimalPrice)
                {
                    minimalPrice = ShopsPrices[shop];
                    result = shop;
                }
            }

            return result;
        }
    }
}