using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;
namespace Shops.Entities
{
    public class Product : IEquatable<Product>
    {
        public Product(string name, Guid id)
        {
            ProductName = name;
            Id = id;
        }

        public Product(Product p)
        {
            Id = p.Id;
            ProductName = p.ProductName;
        }

        public string ProductName { get; }
        public Guid Id { get; }

        public bool Equals(Product other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ProductName == other.ProductName && Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Product)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductName, Id);
        }
    }
}