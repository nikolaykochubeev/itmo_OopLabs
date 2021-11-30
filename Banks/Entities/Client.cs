using System;
using Banks.Tools;

namespace Banks.Entities
{
    public class Client
    {
        public Client(string name, ulong passport = default, string address = default, Guid id = default)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Name = name ?? throw new BanksException("Client must have a name");

            // TODO: Validator for the passport???
            Passport = passport;
            Address = address;
        }

        public Guid Id { get; }
        public string Name { get; }
        public ulong Passport { get; }
        public string Address { get; }
    }
}