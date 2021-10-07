using System;

namespace Shops.Tools
{
    public class NoProductException : ShopException
    {
        public NoProductException()
        {
        }

        public NoProductException(string message)
            : base(message)
        {
        }

        public NoProductException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}