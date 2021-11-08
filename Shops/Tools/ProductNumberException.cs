using System;

namespace Shops.Tools
{
    public class ProductNumberException : ShopsException
    {
        public ProductNumberException(string message)
            : base(message)
        {
        }
    }
}