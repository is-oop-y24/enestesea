using System.Collections.Generic;

namespace Shops.Entities
{
    public class Customer
    {
        private int _money;
        public Customer(string name, int money, List<CustomerProduct> shoppingList)
        {
            Name = name;
            Money = money;
            ShoppingList = shoppingList;
        }

        public string Name { get; }

        public int Money
        {
            get => _money;
            set => _money = value;
        }

        public List<CustomerProduct> ShoppingList { get; }
    }
}