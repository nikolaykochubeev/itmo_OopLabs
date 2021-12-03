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
        private bool _addressNeeded;
        private decimal _suspendTransactionLimit;
        private decimal _creditAnnualPercentage;
        private decimal _debitAnnualPercentage;
        private decimal _creditWithdrawalLimit;
        private uint _depositAccountExpirationDate;

        public BankSettings(List<DepositPercentageRange> depositAnnualPercentages, bool passportNeeded, bool addressNeeded, decimal suspendTransactionLimit, decimal debitAnnualPercentage, decimal creditAnnualPercentage, decimal creditWithdrawalLimit, uint depositAccountExpirationDate)
        {
            _passportNeeded = passportNeeded;
            _addressNeeded = addressNeeded;
            _suspendTransactionLimit = suspendTransactionLimit;
            _debitAnnualPercentage = debitAnnualPercentage;
            _creditAnnualPercentage = creditAnnualPercentage;
            _creditWithdrawalLimit = creditWithdrawalLimit;
            _depositAccountExpirationDate = depositAccountExpirationDate;
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

        public bool AddressNeeded
        {
            get => _addressNeeded;
            set
            {
                _passportNeeded = value;
                CentralBank.GetBank(BankId).CreateNotification(value
                    ? new Notification("Now the bank needs a address to access all available opportunities")
                    : new Notification("Now the bank does not need a address to access all available opportunities"));
            }
        }

        public decimal SuspendTransactionLimit
        {
            get => _suspendTransactionLimit;
            set
            {
                CentralBank.GetBank(BankId).CreateNotification(new Notification("suspendTransactionLimit was changed"));
                _suspendTransactionLimit = value;
            }
        }

        public decimal DebitAnnualPercentage
        {
            get => _debitAnnualPercentage;
            set
            {
                CentralBank.GetBank(BankId).CreateNotification(new Notification("debitAnnualPercentage was changed"));
                _debitAnnualPercentage = value;
            }
        }

        public decimal CreditAnnualPercentage
        {
            get => _creditAnnualPercentage;
            set
            {
                CentralBank.GetBank(BankId).CreateNotification(new Notification("creditAnnualPercentage was changed"));
                _creditAnnualPercentage = value;
            }
        }

        public decimal CreditWithdrawalLimit
        {
            get => _creditWithdrawalLimit;
            set
            {
                CentralBank.GetBank(BankId).CreateNotification(new Notification("creditWithdrawalLimit was changed"));
                _creditWithdrawalLimit = value;
            }
        }

        public uint DepositAccountExpirationDate
        {
            get => _depositAccountExpirationDate;
            set
            {
                CentralBank.GetBank(BankId).CreateNotification(new Notification("depositAccountExpirationDate was changed"));
                _depositAccountExpirationDate = value;
            }
        }

        public IEnumerable<DepositPercentageRange> DepositAnnualPercentages => _depositAnnualPercentages;
    }
}