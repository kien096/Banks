namespace Banks.Model
{
    public sealed class DebitAccount : Account
    {
        public DebitAccount(Client client, Bank bank, double percent, double money = 0) : base(client, bank)
        {
            Percent = percent;
            Money = money;
            Commission = 0;
            PercentMoney = 0;

            CanTransferMoney = true;
            CanWithdrawMoney = true;
            CanPutMoney = true;
            CanBeNegative = false;

            SendMessage += client.GetMessage;
        }

        public override string Type => "Дебетовый";
    }
}