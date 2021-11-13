using System;
using System.Collections.Generic;

namespace Banks.Model
{
    public static class CentralBank
    {
        /// <summary>
        /// Банки
        /// </summary>
        public static List<Bank> Banks { get; } = new List<Bank>();

        /// <summary>
        /// Клиенты
        /// </summary>
        public static List<Client> Clients { get; } = new List<Client>();

        /// <summary>
        /// Транзакции
        /// </summary>
        public static List<Transaction> Transactions { get; } = new List<Transaction>();

        /// <summary>
        /// Текущая дата для симуляции времени
        /// </summary>
        public static DateTime CurrentDate { get; private set; }

        private static event Action SkipDay;

        static CentralBank()
        {
            CurrentDate = DateTime.Now;
        }

        /// <summary>
        /// Пропустить дни
        /// </summary>
        /// <param name="day"></param>
        public static void AddDays(int day = 1)
        {
            for (int i = 0; i < day; i++)
            {
                CurrentDate = CurrentDate.AddDays(1);
                try
                {
                    SkipDay();
                }
                catch { }
            }
        }

        /// <summary>
        /// Пропустить месяца
        /// </summary>
        /// <param name="month"></param>
        public static void AddMonths(int month = 1)
        {
            var days = (CurrentDate.AddMonths(month) - CurrentDate).Days;

            AddDays(days);
        }

        /// <summary>
        /// Пропустить года
        /// </summary>
        /// <param name="year"></param>
        public static void AddYears(int year = 1)
        {
            var days = (CurrentDate.AddYears(year) - CurrentDate).Days;

            AddDays(days);
        }

        public static Bank CreateBank(string name, double debitPercent, Dictionary<double, double> depositPercents, double commission, double creditLimit, double transferLimit, double withdrawLimit)
        {
            var bank = new Bank(name, debitPercent, depositPercents, commission, creditLimit, transferLimit, withdrawLimit);
            SkipDay += bank.DoPercents;

            Banks.Add(bank);

            return bank;
        }
    }
}