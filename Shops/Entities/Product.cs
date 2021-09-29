using System;
namespace Shops.Entities
{
    public class Product
    {
        public Product(string name, Guid id)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}