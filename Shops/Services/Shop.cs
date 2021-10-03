using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Services
{
    public class Shop
    {
        // private Dictionary<string, int> _buildProducts = new Dictionary<string, int>();
        public Shop(string name, string location)
        {
            Name = name;
            Location = location;
            Id = default(int);
            MoneyInTheShop = 0;
            AllProducts = new List<Product>();
            AlreadyRegisteredProducts = new List<string>();
        }

        public static string Name { get; set; }
        public string Location { get; }
        public int Id { get; set; }
        public int MoneyInTheShop { get; private set; }
        public List<Product> AllProducts { get; }
        public List<string> AlreadyRegisteredProducts { get; set; }

        public void AddProducts(Product product)
        {
            foreach (Product a in AllProducts)
            {
                if (product.NameOfProduct == a.NameOfProduct && a.NumbersOfProduct != 0)
                {
                    a.NumbersOfProduct += product.NumbersOfProduct;
                }
                else
                {
                    AllProducts.Add(product);
                }
            }
        }

        // public void AddProducts(List<Product> products)
        // {
        //     for (int a = 0; a < 2; a++)
        //     {
        //         if (AllProducts.Count == 0)
        //         {
        //             AllProducts.Add(products[0]);
        //         }
        //         else
        //         {
        //             for (int i = 1; i < products.Count; i++)
        //             {
        //                 foreach (Product b in AllProducts)
        //                 {
        //                     if (b.NameOfProduct == products[i].NameOfProduct)
        //                     {
        //                         b.NumbersOfProduct += products[i].NumbersOfProduct;
        //                     }
        //                     else
        //                     {
        //                         AllProducts.Add(products[i]);
        //                     }
        //                 }
        //             }
        //         }
        //     }
        // }
        public void AddProducts(List<Product> products)
        {
            foreach (Product a in products)
            {
                if (AlreadyRegisteredProducts.Contains(a.NameOfProduct))
                {
                    if (!AllProducts.Contains(a))
                    {
                        AllProducts.Add(a);
                    }
                }
                else
                {
                    throw new RegistrationException("Product has't been registered");
                }
            }
        }

        public void Buy(Person customer1, Dictionary<string, int> buildProducts)
        {
            foreach (string a in buildProducts.Keys)
            {
                foreach (Product b in AllProducts.Where(b => a == b.NameOfProduct))
                {
                    if (buildProducts[a] <= b.NumbersOfProduct)
                    {
                        if (customer1.PersonMoney >= buildProducts[a] * b.Coast)
                        {
                            b.NumbersOfProduct -= buildProducts[a];
                            MoneyInTheShop += buildProducts[a] * b.Coast;
                            customer1.PersonMoney -= buildProducts[a] * b.Coast;
                            customer1.ShoppingСart.Add(a, buildProducts[a]);

                            // AmountOfOnePurchase += buildProducts[a] * b.Coast;
                        }
                        else
                        {
                            throw new MoneyException("You don't have enough money to buy this product");
                        }
                    }
                    else
                    {
                        throw new ProductNumberException("We don't have enough numbers of product");
                    }
                }
            }
        }

        public void ChangePrice(string changedProduct, int newCoast)
        {
            foreach (Product a in AllProducts.Where(a => a.NameOfProduct == changedProduct))
            {
                a.Coast = newCoast;
            }
        }
    }
}