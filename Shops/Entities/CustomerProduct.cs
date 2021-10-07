using System;

namespace Shops.Entities
{
    public class CustomerProduct : IEquatable<CustomerProduct>
    {
        public CustomerProduct(Product product, int amount)
        {
            ProductOfCustomer = new Product(product);
            CustomerProductAmount = amount;
        }

        public Product ProductOfCustomer { get; }
        public int CustomerProductAmount { get; }

        public bool Equals(CustomerProduct other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(ProductOfCustomer, other.ProductOfCustomer) && CustomerProductAmount == other.CustomerProductAmount;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CustomerProduct)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductOfCustomer, CustomerProductAmount);
        }
    }
}