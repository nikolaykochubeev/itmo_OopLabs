using System;
namespace Shops.Entities
{
    public class Product
    {
        public Product(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}