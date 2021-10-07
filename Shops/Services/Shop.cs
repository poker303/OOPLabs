using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Services
{
    public class Shop
    {
        public Shop(string name, string location)
        {
            Name = name;
            Location = location;
            Id = default(int);
            MoneyInTheShop = 0;
            AllProducts = new List<ProductInTheShop>();
            AlreadyRegisteredProducts = new List<ProductInTheSystem>();
        }

        public static string Name { get; set; }
        public string Location { get; }
        public int Id { get; set; }
        public int MoneyInTheShop { get; private set; }
        public List<ProductInTheShop> AllProducts { get; }
        public List<ProductInTheSystem> AlreadyRegisteredProducts { get; set; }

        // public void AddProducts(ProductInTheShop product)
        // {
        //     foreach (ProductInTheShop a in AllProducts)
        //     {
        //         if (product.Product == a.Product && a.NumbersOfProduct != 0)
        //         {
        //             a.NumbersOfProduct += product.NumbersOfProduct;
        //         }
        //         else
        //         {
        //             AllProducts.Add(product);
        //         }
        //     }
        // }

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
        public void AddProducts(List<ProductInTheShop> products)
        {
            foreach (ProductInTheShop a in products)
            {
                if (AlreadyRegisteredProducts.Contains(a.Product))
                {
                    if (!AllProducts.Contains(a))
                    {
                        AllProducts.Add(a);
                    }
                    else
                    {
                        foreach (ProductInTheShop b in AllProducts.Where(b => b.Product == a.Product))
                        {
                            // foreach (Product b in AllProducts.Where(b => b.NameOfProduct.Id == a.NameOfProduct.Id))
                            b.NumbersOfProduct += a.NumbersOfProduct;
                        }
                    }
                }
                else
                {
                    throw new RegistrationException("Product wasn't registered");
                }
            }
        }

        public void Buy(Person customer1, Dictionary<ProductInTheSystem, int> buildProducts)
        {
            foreach (ProductInTheSystem a in buildProducts.Keys)
            {
                foreach (ProductInTheShop b in AllProducts.Where(b => a == b.Product))
                {
                    // foreach (Product b in AllProducts.Where(b => a.Id == b.NameOfProduct.Id))
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

        public void ChangePrice(ProductInTheSystem changedProduct, int newCoast)
        {
            foreach (ProductInTheShop a in AllProducts.Where(a => a.Product == changedProduct))
            {
                a.Coast = newCoast;
            }
        }
    }
}