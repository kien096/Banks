namespace Banks.Model
{
    public sealed class CreditAccount : Account
    {
        /// <summary>
        /// Кредитный лимит
        /// </summary>
        public double CreditLimit { get; }

        public CreditAccount(Client client, Bank bank, double creditLimit, double commission, double money = 0) : base(client, bank)
        {
            Percent = 0;
            Money = money;
            Commission = commission;
            PercentMoney = 0;
            CreditLimit = creditLimit;

            CanTransferMoney = true;
            CanWithdrawMoney = true;
            CanPutMoney = true;
            CanBeNegative = true;

            SendMessage += client.GetMessage;
        }

        public override string Type => "Кредитный";
    }
}