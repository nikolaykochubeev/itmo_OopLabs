using System;
using System.Collections.Generic;
using Banks.AccountTypes;
using Banks.Tools;

namespace Banks.Entities
{
    public class BankSettings
    {
        private List<DepositPercentageRange> _depositAnnualPercentages;
        private decimal _suspendTransactionLimit;
        private decimal _creditAnnualPercentage;
        private decimal _debitAnnualPercentage;
        private decimal _creditWithdrawalLimit;
        private uint _depositAccountExpirationDate;

        public BankSettings(List<DepositPercentageRange> depositAnnualPercentages, decimal suspendTransactionLimit, decimal debitAnnualPercentage, decimal creditAnnualPercentage, decimal creditWithdrawalLimit, uint depositAccountExpirationDate)
        {
            _suspendTransactionLimit = suspendTransactionLimit;
            _debitAnnualPercentage = debitAnnualPercentage;
            _creditAnnualPercentage = creditAnnualPercentage;
            _creditWithdrawalLimit = creditWithdrawalLimit;
            _depositAccountExpirationDate = depositAccountExpirationDate;
            _depositAnnualPercentages = depositAnnualPercentages ?? throw new BanksException("depositAnnualPercentages can not be the null");
        }

        public Guid BankId { get; } = Guid.NewGuid();

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