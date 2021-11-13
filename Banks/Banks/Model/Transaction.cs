using System;
using System.Collections.Generic;

namespace Banks.Model
{
    /// <summary>
    /// Транзакция
    /// </summary>
    public class Transaction
    {
        private static int ord = 0;

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; } = ord++;
        
        /// <summary>
        /// Отменена ли транзакция
        /// </summary>
        public bool IsCanseled { get; set; }

        /// <summary>
        /// Список операций в транзакции
        /// </summary>
        public Dictionary<Account, double> Operations { get; } = new Dictionary<Account, double>(2);

        public Transaction()
        {
            CentralBank.Transactions.Add(this);
        }

        public void Cansel()
        {
            if (IsCanseled)
                return;

            foreach (var operation in Operations)
            {
                var money = operation.Value * -1;

                operation.Key.CanselTransaction(money);
            }

            IsCanseled = true;
        }
    }
}