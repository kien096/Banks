using System;
using System.Collections.Generic;
using System.Linq;

namespace Banks.Model
{
    public class Bank
    {
        public string Name { get; }

        public List<Account> Accounts { get; } = new List<Account>();

        public List<Transaction> Transactions { get; } = new List<Transaction>();

        public double DebitPercent { get; private set; }

        public SortedDictionary<double, double> DepositPercents { get; }

        public double Commission { get; private set; }

        public double CreditLimit { get; private set; }

        /// <summary>
        /// Лимит на переводы
        /// </summary>
        public double TransferLimit { get; protected set; }

        /// <summary>
        /// Лимит на снятие
        /// </summary>
        public double WithdrawLimit { get; protected set; }

        /// <summary>
        /// Изменить лимит на переводы
        /// </summary>
        /// <param name="limit"></param>
        public void ChangeTransferLimit(double limit)
        {
            TransferLimit = limit;
            SendMessage?.Invoke($@"Bank {Name} changed transfer limit to {limit}");
        }

        /// <summary>
        /// Изменить лимит на снятие
        /// </summary>
        /// <param name="limit"></param>
        public void ChangeWithdrawLimit(double limit)
        {
            WithdrawLimit = limit;
            SendMessage?.Invoke($@"Bank {Name} changed withdraw limit to {limit}");
        }

        public Bank(string name, double debitPercent, Dictionary<double, double> depositPercents, double commission, double creditLimit, double transferLimit, double withdrawLimit)
        {
            Name = name;
            DebitPercent = debitPercent;
            CreditLimit = creditLimit;
            DepositPercents = new SortedDictionary<double, double>();
            foreach (var d in depositPercents)
            {
                DepositPercents.Add(d.Key, d.Value);
            }

            Commission = commission;
            TransferLimit = transferLimit;
            WithdrawLimit = withdrawLimit;
        }

        public void DoPercents()
        {
            foreach (var account in Accounts)
            {
                account.AddPercent();

                if (CentralBank.CurrentDate.Day == 1)
                {
                    account.PayPercent();
                }
            }
        }

        public DebitAccount CreateDebitAccount(Client client, double money = 0)
        {
            var account = new DebitAccount(client, this, DebitPercent, money);

            Accounts.Add(account);

            return account;
        }

        public DepositAccount CreateDepositAccount(Client client, int depositEnd, double money = 0)
        {
            var percent = DepositPercents.First(x => x.Key > money).Value;
            var account = new DepositAccount(client, this, percent, depositEnd);

            Accounts.Add(account);

            return account;
        }

        public CreditAccount CreateCreditAccount(Client client, double money = 0)
        {
            var account = new CreditAccount(client, this, CreditLimit, Commission, money);

            Accounts.Add(account);
            
            return account;
        }

        public delegate void NotificationDelegate(string message);

        public event NotificationDelegate SendMessage;
    }
}