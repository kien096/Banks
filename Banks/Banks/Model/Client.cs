using System;
using System.Collections.Generic;

namespace Banks.Model
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class Client
    {
        public Client(string name, string surName)
        {
            Name = name;
            SurName = surName;

            CentralBank.Clients.Add(this);
        }

        public Client(string name, string surName, string adress, int passport) : this(name, surName)
        {
            Adress = adress;
            Passport = passport;
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string SurName { get; set; }

        private string _adress;

        /// <summary>
        /// Адрес
        /// </summary>
        public string Adress
        {
            get => _adress;
            set
            {
                if (_adress == value)
                    return;

                _adress = value;

                if (!string.IsNullOrEmpty(_adress) && Passport.HasValue)
                    UnlockLimits?.Invoke();
            }
        }

        private int? _passport;

        /// <summary>
        /// Паспорт
        /// </summary>
        public int? Passport
        {
            get => _passport;
            set
            {
                if (_passport.HasValue &&_passport.Value == value)
                    return;

                _passport = value;

                if (!string.IsNullOrEmpty(_adress) && Passport.HasValue)
                    UnlockLimits?.Invoke();
            }
        }

        /// <summary>
        /// Список счетов
        /// </summary>
        public List<Account> Accounts { get; } = new List<Account>();

        /// <summary>
        /// Снять ограничения со счетов
        /// </summary>
        public event Action UnlockLimits;

        public void GetMessage(string message)
        {
            Console.WriteLine($@"{Name} {SurName}: {message}");
        }
    }
}