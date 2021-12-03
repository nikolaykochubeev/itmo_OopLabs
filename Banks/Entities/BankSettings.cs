using System;
using System.Collections.Generic;
using Banks.AccountTypes;
using Banks.Tools;

namespace Banks.Entities
{
    public class BankSettings
    {
        private List<DepositPercentageRange> _depositAnnualPercentages;
        private bool _passportNeeded;
        public BankSettings(List<DepositPercentageRange> depositAnnualPercentages, bool passportNeeded, bool addressNeeded, decimal suspendTransactionLimit, decimal debitAnnualPercentage, decimal creditAnnualPercentage, decimal creditWithdrawalLimit, uint depositAccountExpirationDate)
        {
            _passportNeeded = passportNeeded;
            AddressNeeded = addressNeeded;
            SuspendTransactionLimit = suspendTransactionLimit;
            DebitAnnualPercentage = debitAnnualPercentage;
            CreditAnnualPercentage = creditAnnualPercentage;
            CreditWithdrawalLimit = creditWithdrawalLimit;
            DepositAccountExpirationDate = depositAccountExpirationDate;
            _depositAnnualPercentages = depositAnnualPercentages ?? throw new BanksException("depositAnnualPercentages can not be the null");
        }

        public Guid BankId { get; } = Guid.NewGuid();
        public bool PassportNeeded
        {
            get => _passportNeeded;
            set
            {
                _passportNeeded = value;
                CentralBank.GetBank(BankId).CreateNotification(value
                    ? new Notification("Now the bank needs a passport to access all available opportunities")
                    : new Notification("Now the bank does not need a passport to access all available opportunities"));
            }
        }

        public bool AddressNeeded { get; set; }
        public decimal SuspendTransactionLimit { get; set; }
        public decimal DebitAnnualPercentage { get; set; }
        public decimal CreditAnnualPercentage { get; set; }
        public decimal CreditWithdrawalLimit { get; set; }
        public uint DepositAccountExpirationDate { get; set; }
        public IEnumerable<DepositPercentageRange> DepositAnnualPercentages => _depositAnnualPercentages;
    }
}