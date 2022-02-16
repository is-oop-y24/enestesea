using System;

namespace Shops.Tools
{
    public class LackOfProductException : ShopException
    {
        public LackOfProductException()
        {
        }

        public LackOfProductException(string message)
            : base(message)
        {
        }

        public LackOfProductException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}