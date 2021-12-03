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
        public ulong Passport { get;  private set; }
        public string Address { get;  private set; }
        public bool IsSuspend { get; private set; } = true;
        public List<Notification> Notifications { get; private set; }
        public void UpdateClientPassport(ulong passport)
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

        public void UpdateClientSuspend(bool isSuspend)
        {
            IsSuspend = isSuspend;
        }
    }
}