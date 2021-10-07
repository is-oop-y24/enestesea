using System;

namespace Shops.Tools
{
    public class LackOfMoneyException : ShopException
    {
        public LackOfMoneyException()
        {
        }

        public LackOfMoneyException(string message)
            : base(message)
        {
        }

        public LackOfMoneyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}