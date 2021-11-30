using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Tools;

namespace Banks.Entities
{
    public static class CentralBank
    {
        private static readonly List<Bank> _banks = new ();
        private static readonly List<Client> _clients = new ();
        public static IReadOnlyList<Bank> Banks => _banks;
        public static IReadOnlyList<Client> Clients => _clients;

        public static Client AddClient(string name, ulong passport, string address)
        {
            var client = new Client(name, passport, address);
            _clients.Add(client);
            return client;
        }

        public static Client GetClient(Guid clientId)
        {
            return Clients.FirstOrDefault(client => client.Id == clientId);
        }

        public static Client UpdateClientName(Client client, string name)
        {
            int index = _clients.IndexOf(client);
            _clients[index] = index != -1
                ? new Client(name, client.Passport, client.Address, client.Id)
                : throw new BanksException("Client not found in ClientService");

            return Clients[index];
        }

        public static Client UpdateClientPassport(Client client, ulong passport)
        {
            int index = _clients.IndexOf(client);
            _clients[index] = index != -1
                ? new Client(client.Name, passport, client.Address, client.Id)
                : throw new BanksException("Client not found in ClientService");

            return Clients[index];
        }

        public static Client UpdateClientAddress(Client client, string address)
        {
            int index = _clients.IndexOf(client);
            _clients[index] = index != -1
                ? new Client(client.Name, client.Passport, address, client.Id)
                : throw new BanksException("Client not found in ClientService");

            return Clients[index];
        }
    }
}