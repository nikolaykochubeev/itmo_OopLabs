using System;
using System.Collections.Generic;
using Banks.Tools;

namespace Banks.Entities
{
    public class Client
    {
        public Client(string firstName, string lastName, Guid id = default)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            FirstName = firstName ?? throw new BanksException("Client must have a firstname");
            LastName = lastName ?? throw new BanksException("Client must have a lastname");
        }

        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Passport { get; private set; }
        public string Address { get; private set; }
        public bool IsSuspend => Passport == null || Address == null;

        public List<Notification> Notifications { get; private set; }

        public void UpdateClientPassport(string passport)
        {
            Passport = passport;
        }

        public void UpdateClientAddress(string address)
        {
            Address = address;
        }

        public void AddNotification(Notification notification)
        {
            Notifications.Add(notification);
        }

        public override string ToString()
        {
            return FirstName + LastName + "\n" + "Passport: " + Passport + "\n" + "Client Id: " + "\n" + Address + "\n" + Id + "\n";
        }
    }
}