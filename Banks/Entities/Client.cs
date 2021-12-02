using System;
using Banks.Tools;

namespace Banks.Entities
{
    public class Client
    {
        public Client(string firstName, string lastName, ulong passport = default, string address = default, Guid id = default)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            FirstName = firstName ?? throw new BanksException("Client must have a firstname");
            LastName = lastName ?? throw new BanksException("Client must have a lastname");

            // TODO: Validator for the passport???
            Passport = passport;
            Address = address;
        }

        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public ulong Passport { get; }
        public string Address { get; }

        public Client UpdateClientFirstName(Client client, string firstName)
        {
            return UpdateClient(firstName, LastName, Passport, Address, Id);
        }

        public Client UpdateClientLastName(Client client, string lastName)
        {
            return UpdateClient(FirstName, lastName, Passport, Address, Id);
        }

        public Client UpdateClientPassport(Client client, ulong passport)
        {
            return UpdateClient(FirstName, LastName, passport, Address, Id);
        }

        public Client UpdateClientAddress(string address)
        {
            return UpdateClient(FirstName, LastName, Passport, address, Id);
        }

        private Client UpdateClient(string firstName, string lastName, ulong passport, string address, Guid id)
        {
            return new Client(firstName, lastName, passport, address, id);
        }
    }
}