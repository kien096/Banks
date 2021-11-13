using System;
using Banks.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Banks.Test
{
    public class Tests
    {
        Bank _sber;

        [SetUp]
        public void Setup()
        {
            var percents = new Dictionary<double, double>()
            {
                { 50000, 3 },
                { 100000, 3.5 },
                { 200000, 5 }
            };

            _sber = CentralBank.CreateBank("бсх№",
                                              5,
                                              percents,
                                              3,
                                              30000,
                                              50000,
                                              50000);

            //var client1 = new Client("First", "First");
            //var client2 = new Client("Second", "Second", "Adress", 24531253);

            //var debit1 = sber.CreateDebitAccount(client1);
            //var debit2 = sber.CreateDebitAccount(client2);

            //var deposit1 = sber.CreateDepositAccount(client1, DateTime.Now.AddDays(150));
            //var deposit2 = sber.CreateDepositAccount(client2, DateTime.Now.AddDays(30));

            //var credit1 = sber.CreateCreditAccount(client1);
            //var credit2 = sber.CreateCreditAccount(client2);




            //debit1.PutMoney(50000);
            //debit2.PutMoney(100000);

            //CentralBank.AddYears(1);
        }

        [Test]
        public void PutMoneyTest()
        {
            var client1 = new Client("First", "First");
            var debit1 = _sber.CreateDebitAccount(client1);
            debit1.PutMoney(50000);

            if (debit1.Money != 50000)
            {
                throw new Exception("Money not added");
            }
        }

        [Test]
        public void RemoveMoney()
        {
            var client1 = new Client("First", "First");
            var debit1 = _sber.CreateDebitAccount(client1);
            debit1.PutMoney(50000);
            debit1.WithdrawMoney(30000);

            if (debit1.Money != 20000)
            {
                throw new Exception("Money not removed");
            }
        }

        [Test]
        public void HasLimits()
        {
            var client1 = new Client("First", "First");
            var debit1 = _sber.CreateDebitAccount(client1);

            if (!debit1.HasLimits)
            {
                throw new Exception("Doesnt have limits");
            }
        }

        [Test]
        public void UnlockLimits()
        {
            var client1 = new Client("First", "First");
            var debit1 = _sber.CreateDebitAccount(client1);
            client1.Adress = "adress";
            client1.Passport = 1242;

            if (debit1.HasLimits)
            {
                throw new Exception("Have limits");
            }
        }


    }
}