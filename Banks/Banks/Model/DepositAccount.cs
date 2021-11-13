using System;

namespace Banks.Model
{
    public sealed class DepositAccount : Account
    {
        /// <summary>
        /// Окончание депозита в днях
        /// </summary>
        public int DepositEnd { get; private set; }

        public DepositAccount(Client client, Bank bank, double percent, int depositEnd, double money = 0) : base(client, bank)
        {
            Percent = percent;
            Money = money;
            Commission = 0;
            PercentMoney = 0;
            DepositEnd = depositEnd;

            CanTransferMoney = false;
            CanWithdrawMoney = false;
            CanPutMoney = true;
            CanBeNegative = false;

            SendMessage += client.GetMessage;
        }

        /// <summary>
        /// Добавить ежедневный процент
        /// </summary>
        public override void AddPercent()
        {
            if (DepositEnd == 0)
            {
                CanTransferMoney = true;
                CanWithdrawMoney = true;
            }
            else
            {
                DepositEnd--;
            }

            base.AddPercent();
        }

        public override string Type => "Депозитный";
    }
}