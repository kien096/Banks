using Banks.Model;
using System.Collections.Generic;

namespace Banks
{
    class Program
    {
        static void Main(string[] args)
        {
            var percents = new Dictionary<double, double>()
            {
                { 50000, 3 },
                { 100000, 3.5 },
                { 200000, 5 }
            };

            var sber =CentralBank.CreateBank("Сбер",
                                             5,
                                             percents,
                                             3,
                                             30000,
                                             50000,
                                             50000);

            var client1 = new Client("First", "First");
            var client2 = new Client("Second", "Second", "Adress", 24531253);

            var debit1 = sber.CreateDebitAccount(client1);
            var debit2 = sber.CreateDebitAccount(client2);

            //var deposit1 = sber.CreateDepositAccount(client1, DateTime.Now.AddDays(150));
            //var deposit2 = sber.CreateDepositAccount(client2, DateTime.Now.AddDays(30));

            //var credit1 = sber.CreateCreditAccount(client1);
            //var credit2 = sber.CreateCreditAccount(client2);




            debit1.PutMoney(50000);
            debit2.PutMoney(100000);

            CentralBank.AddYears();

            BankSystem.Menu();

        }
    }
}