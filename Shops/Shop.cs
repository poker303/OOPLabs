using System;
using System.Collections.Generic;

namespace Shops
{
    public class Shop
    {
        private static int _totalId;

        private int moneyInTheShop;

        public Shop(string name, string location)
        {
            Name = name;
            Location = location;
            Id = ++_totalId;
            AllProducts = new List<ProductsInTheShop>();
        }

        public string Name { get; set; }
        public string Location { get; set; }

        public int Id { get; set; }

        public List<ProductsInTheShop> AllProducts { get; set; }

        public void AddProducts(ProductsInTheShop product)
        {
            foreach (ProductsInTheShop a in AllProducts)
            {
                // OldProductCoast = a.Coast;
                if (product.NamesOfProduct == a.NamesOfProduct && a.NumbersOfProduct != 0)
                {
                    a.NumbersOfProduct += product.NumbersOfProduct;
                }
                else
                {
                    AllProducts.Add(product);
                }
            }
        }

// What //How much
        public int Buy(Person customer1, string What, int How)
        {
            foreach (ProductsInTheShop a in AllProducts)
            {
                if (What == a.NamesOfProduct)
                {
                    if (How <= a.NumbersOfProduct)
                    {
                        a.NumbersOfProduct = a.NumbersOfProduct - How;
                        moneyInTheShop += How * a.Coast;
                    }
                    else
                    {
                        // throw Exception("We don't have enough numbers of product");
                    }
                }
            }

            return moneyInTheShop;
        }
    }
}