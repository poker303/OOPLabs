using System;

namespace Shops.Tools
{
    public class MoneyException : ShopsException
    {
        public MoneyException(string message)
            : base(message)
        {
        }
    }
}