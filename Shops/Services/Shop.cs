using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Services
{
    public class Shop
    {
        private static int _currentId = 0;

        public Shop(string name, string location)
        {
            Name = name;
            Location = location;
            Id = _currentId++;
            Money = 0;
            AllProducts = new List<ProductInTheShop>();
            AlreadyRegisteredProducts = new List<ProductInTheSystem>();
        }

        public static string Name { get; set; }
        public string Location { get; }
        public int Id { get; }
        public int Money { get; private set; }
        public List<ProductInTheShop> AllProducts { get; }
        public List<ProductInTheSystem> AlreadyRegisteredProducts { get; set; }

        public void AddProducts(List<ProductInTheShop> products)
        {
            foreach (ProductInTheShop a in products)
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
                        b.Numbers += a.Numbers;
                    }
                }
            }
        }

        public void Buy(Person customer1, List<OrderedProducts> buildProducts)
        {
            foreach (OrderedProducts a in buildProducts)
            {
                foreach (ProductInTheShop b in AllProducts.Where(b => a.Product == b.Product))
                {
                    // foreach (Product b in AllProducts.Where(b => a.Id == b.NameOfProduct.Id))
                    if (a.Numbers <= b.Numbers)
                    {
                        if (customer1.Money >= a.Numbers * b.Cost)
                        {
                            b.Numbers -= a.Numbers;
                            Money += a.Numbers * b.Cost;
                            customer1.ChangingMoney(a.Numbers, b.Cost);
                            customer1.ShoppingСart.Add(a);
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

                    break;
                }
            }
        }

        public void ChangePrice(ProductInTheShop changedProduct, int newCoast)
        {
            changedProduct.Cost = newCoast;
        }
    }
}