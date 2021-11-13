using System;

namespace Banks.Model
{
    /// <summary>
    /// Счет в банке
    /// </summary>
    public abstract class Account
    {
        private static int ord = 0;

        /// <summary>
        /// Уникальный идентификатор счёта
        /// </summary>
        public int Id { get; } = ord++;

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Id.ToString().Split('-')[0];
        }

        /// <summary>
        /// Комиссия
        /// </summary>
        public double Commission { get; protected set; }

        /// <summary>
        /// Процент
        /// </summary>
        public double Percent { get; protected set; }

        /// <summary>
        /// Процентные деньги
        /// </summary>
        public double PercentMoney { get; protected set; }

        public double Money { get; protected set; }

        /// <summary>
        /// Клиент
        /// </summary>
        public Client Client { get; }

        /// <summary>
        /// Банк
        /// </summary>
        public Bank Bank { get; }

        /// <summary>
        /// Можно переводить деньги
        /// </summary>
        public virtual bool CanTransferMoney { get; protected set; }

        /// <summary>
        /// Можно снимать деньги
        /// </summary>
        public virtual bool CanWithdrawMoney { get; protected set; }

        /// <summary>
        /// Можно пополнять счёт
        /// </summary>
        public virtual bool CanPutMoney { get; protected set; } 

        /// <summary>
        /// Может ли быть меньше нуля (долг)
        /// </summary>
        public virtual bool CanBeNegative { get; protected set; }

        /// <summary>
        /// Лимиты на перевод и снятие
        /// </summary>
        public virtual bool HasLimits { get; protected set; }
        
        public Account(Client client, Bank bank)
        {
            client.Accounts.Add(this);
            Client = client;

            Bank = bank;

            if (!client.Passport.HasValue || string.IsNullOrEmpty(client.Adress))
            {
                HasLimits = true;
                client.UnlockLimits += Unlock;
            }
        }

        public void Unlock()
        {
            HasLimits = false;
            Client.UnlockLimits -= Unlock;
        }

        /// <summary>
        /// Положить деньги на счет
        /// </summary>
        /// <param name="money">Сумма</param>
        public virtual Transaction PutMoney(double money)
        {
            if (!CanPutMoney)
                return null;
            
            Money += money;
            var tran = new Transaction();
            tran.Operations.Add(this, money);

            return tran;
        }

        /// <summary>
        /// Снять деньги
        /// </summary>
        /// <param name="money">Сумма</param>
        public virtual Transaction WithdrawMoney(double money)
        {
            if (!CanWithdrawMoney)
                return null;

            if (money > Money || Money <= 0)
                if (!CanBeNegative)
                    return null;

            Money -= money;
            var tran = new Transaction();
            tran.Operations.Add(this, -money);

            if (Money < 0)
            {
                var com = Money * (Commission / 100);
                Money -= com; // коммиссия
                tran.Operations.Add(this, -com);
            }

            return tran;
        }

        /// <summary>
        /// Перевод денег между счетами
        /// </summary>
        /// <param name="money">Сумма</param>
        /// <param name="account">Счет зачисления</param>
        public virtual Transaction Transfer(double money, Account account)
        {
            if (!CanTransferMoney)
                return null;

            if (money > Money || Money <= 0)
                if (!CanBeNegative)
                    return null;

            account.Money += money;
            Money -= money;

            var tran = new Transaction();
            tran.Operations.Add(this, -money);
            tran.Operations.Add(account, money);

            if (Money < 0)
            {
                var com = Money * (Commission / 100);
                Money -= com; // коммиссия
                tran.Operations.Add(this, -com);
            }

            return tran;
        }

        /// <summary>
        /// Добавить ежедневный процент
        /// </summary>
        public virtual void AddPercent()
        {
            if (Percent == 0)
                return;

            PercentMoney += Money * (Percent / 100 / 365);
        }

        /// <summary>
        /// Выплатить месячные проценты
        /// </summary>
        public virtual void PayPercent()
        {
            var money = Money;
            var pm = Math.Round(PercentMoney, 2); 
            Money += pm;
            Money = Math.Round(Money,2);
            string type = string.Empty;

            if (this is DebitAccount)
                type = "дебитовый";

            if (this is DepositAccount)
                type = "депозитный";

            if (this is CreditAccount)
                type = "кредитный";

            SendMessage?.Invoke($@"{pm} рублей добавлено на {this} {type} счет {Bank.Name} банка: {money} -> {Money}");
            PercentMoney = 0;
        }

        /// <summary>
        /// Отменить транзакцию
        /// </summary>
        /// <param name="money">Сумма</param>
        public void CanselTransaction(double money)
        {
            Money += money;
        }

        public abstract string Type { get; }

        public delegate void NotificationDelegate(string message);

        public event NotificationDelegate SendMessage;
    }
}