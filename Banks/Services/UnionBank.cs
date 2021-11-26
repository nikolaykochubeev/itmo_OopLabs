using System.Collections.Generic;
using Banks.Entites;

namespace Banks.Services
{
    public class UnionBank
    {
        private readonly List<Bank> _banks = new ();
        private readonly List<Client> _clients = new ();
        public IReadOnlyList<Bank> Banks => _banks;
        public IReadOnlyList<Client> Clients => _clients;
    }
}