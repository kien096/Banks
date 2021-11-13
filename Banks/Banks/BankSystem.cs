using Banks.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Banks
{
    public static class BankSystem
    {

        public static void Menu()
        {
            while (true)
            {
                var list = new List<string>
                {
                    "1. Показать данные",
                    "2. Создать данные",
                    "3. Управление счетом",
                    "4. Перемотать время",
                    "5. Выход"
                };

                Draw(list);

                Console.WriteLine();
                Console.Write("Выберите действие: ");
                var data = Convert.ToInt32(Console.ReadLine());

                switch (data)
                {
                    case 1:
                        ShowMenu();
                        break;
                    case 2:
                        CreateMenu();
                        break;
                    case 3:
                        ChooseAccount();
                        break;
                    case 4:
                        SkipTime();
                        break;
                    case 5:
                        return;
                    default:
                        continue;
                }
            }
        }

        private static void MenageMenu(Account account)
        {
            while (true)
            {
                var list = new List<string>
                {
                    "1. Пополнить счёт",
                    "2. Снять со счета",
                    "3. Перевести на счет",
                    "4. Назад"
                };

                Draw(list);

                Console.WriteLine();
                Console.Write("Выберите действие: ");
                var data = Convert.ToInt32(Console.ReadLine());

                switch (data)
                {
                    case 1:
                        Put(account);
                        break;
                    case 2:
                        WithDraw(account);
                        break;
                    case 3:
                        Transfer(account);
                        break;
                    case 4:
                        return;
                    default:
                        continue;
                }
            }
        }

        private static void Put(Account account)
        {
            Console.WriteLine();

            Console.WriteLine("Введите сумму: ");
            var money = Convert.ToDouble(Console.ReadLine());

            account.PutMoney(money);
        }

        private static void WithDraw(Account account)
        {
            Console.WriteLine();

            Console.WriteLine("Введите сумму: ");
            var money = Convert.ToDouble(Console.ReadLine());

            account.WithdrawMoney(money);
        }

        private static void Transfer(Account account)
        {
            Console.WriteLine();

            Console.Write("Введите номер счета: ");
            var num = Convert.ToInt32(Console.ReadLine());

            Account acc = null;

            foreach (var bank in CentralBank.Banks)
            {
                var h = bank.Accounts.Find(a => a.Id == num);

                if (h != null)
                {
                    acc = h;
                    break;
                }
            }

            if (account == null || account == acc)
                return;

            Console.WriteLine("Введите сумму: ");
            var money = Convert.ToDouble(Console.ReadLine());

            account.Transfer(money, acc);
        }

        private static void ChooseAccount()
        {
            Console.Write("Введите номер счета: ");
            var num = Convert.ToInt32(Console.ReadLine());

            Account account = null;

            foreach (var bank in CentralBank.Banks)
            {
                var h = bank.Accounts.Find(a => a.Id == num);

                if (h != null)
                {
                    account = h;
                    break;
                }
            }

            if (account == null) 
                return;

            MenageMenu(account);
        }

        private static void SkipTime()
        {
            while (true)
            {
                var list = new List<string>
                {
                    "1. Пропустить дни",
                    "2. Пропустить месяцы",
                    "3. Пропустить года",
                    "4. Назад"
                };

                Draw(list);

                Console.WriteLine();
                Console.Write("Выберите действие: ");
                var data = Convert.ToInt32(Console.ReadLine());

                switch (data)
                {
                    case 1:
                        SkipDays();
                        break;
                    case 2:
                        SkipMonths();
                        break;
                    case 3:
                        SkipYears();
                        break;
                    case 4:
                        return;
                    default:
                        continue;
                }
            }
        }

        private static void SkipDays()
        {
            Console.WriteLine();

            Console.WriteLine("Введите сколько дней пропустить: ");
            var days = Convert.ToInt32(Console.ReadLine());

            CentralBank.AddDays(days);
        }

        private static void SkipMonths()
        {
            Console.WriteLine();

            Console.WriteLine("Введите сколько месяцев пропустить: ");
            var months = Convert.ToInt32(Console.ReadLine());

            CentralBank.AddMonths(months);
        }

        private static void SkipYears()
        {
            Console.WriteLine();

            Console.WriteLine("Введите сколько лет пропустить: ");
            var years = Convert.ToInt32(Console.ReadLine());

            CentralBank.AddYears(years);
        }

        private static void Draw(List<string> menu)
        {
            Console.WriteLine();
            
            foreach (var line in menu)
            {
                Console.WriteLine(line);
            }
        }

        private static void CreateMenu()
        {
            while (true)
            {
                var list = new List<string>
                {
                    "1. Создать банк",
                    "2. Создать счет",
                    "3. Создать клиента",
                    "4. Назад"
                };

                Draw(list);

                Console.WriteLine();
                Console.Write("Выберите действие: ");
                var data = Convert.ToInt32(Console.ReadLine());

                switch (data)
                {
                    case 1:
                        CreateBank();
                        break;
                    case 2:
                        ChooseBank();
                        break;
                    case 3:
                        CreateClient();
                        break;
                    case 4:
                        return;
                    default:
                        continue;
                }
            }
        }

        private static void CreateAccount(Bank bank, Client client)
        {
            while (true)
            {
                var list = new List<string>
                {
                    "1. Создать дебетовый счет",
                    "2. Создать депозитный счет",
                    "3. Создать кредитный счет",
                    "4. Назад"
                };

                Draw(list);

                Console.WriteLine();
                Console.Write("Выберите тип: ");
                var data = Convert.ToInt32(Console.ReadLine());

                switch (data)
                {
                    case 1:
                        CreateDebit(bank, client);
                        break;
                    case 2:
                        CreateDeposit(bank, client);
                        break;
                    case 3:
                        CreateCredit(bank, client);
                        break;
                    case 4:
                        return;
                    default:
                        continue;
                }
            }
        }

        private static void CreateDebit(Bank bank, Client client)
        {
            Console.WriteLine();

            Console.WriteLine("Введите первоначальный взннос: ");
            var money = Convert.ToDouble(Console.ReadLine());

            bank.CreateDebitAccount(client, money);
        }

        private static void CreateDeposit(Bank bank, Client client)
        {
            Console.WriteLine();

            Console.WriteLine("Введите первоначальный взннос: ");
            var money = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введите через сколько дней закончится депозит: ");
            var days = Convert.ToInt32(Console.ReadLine());

            bank.CreateDepositAccount(client, days, money);
        }

        private static void CreateCredit(Bank bank, Client client)
        {
            Console.WriteLine();

            Console.WriteLine("Введите первоначальный взннос: ");
            var money = Convert.ToDouble(Console.ReadLine());

            bank.CreateCreditAccount(client, money);
        }

        private static void ChooseBank()
        {
            ShowBanks();

            Console.WriteLine();
            Console.Write("Выберите банк: ");
            var data = Convert.ToInt32(Console.ReadLine());

            var bank = CentralBank.Banks[data - 1];

            ChooseClient(bank);
        }

        private static void ChooseClient(Bank bank)
        {
            Console.WriteLine();

            ShowClients();

            Console.Write("Выберите клиента: ");
            var data = Convert.ToInt32(Console.ReadLine());

            var client = CentralBank.Clients[data - 1];

            CreateAccount(bank, client);
        }

        private static void CreateBank()
        {
            Console.WriteLine();

            Console.WriteLine("Введите имя: ");
            var name = Console.ReadLine();

            Console.WriteLine("Введите дебетовый процент: ");
            var debit = Console.ReadLine();

            Console.WriteLine("Введите депозитовые проценты в формате сумма:процент через запятую: ");
            var depos = Console.ReadLine();

            Dictionary<double, double> d = new Dictionary<double, double>();
            var s = depos.Split(',');

            foreach (var s1 in s)
            {
                d.Add(Convert.ToDouble(s1.Split(':')[0]), Convert.ToDouble(s1.Split(':')[1]));
            }

            Console.WriteLine("Введите коммиссию: ");
            var commission = Console.ReadLine();

            Console.WriteLine("Введите кредитный лимит: ");
            var credit = Console.ReadLine();

            Console.WriteLine("Введите лимит на переводы: ");
            var transfer = Console.ReadLine();

            Console.WriteLine("Введите лимит на снятие: ");
            var withdraw = Console.ReadLine();

            CentralBank.CreateBank(name,
                                   Convert.ToDouble(debit),
                                   d,
                                   Convert.ToDouble(commission),
                                   Convert.ToDouble(credit),
                                   Convert.ToDouble(transfer),
                                   Convert.ToDouble(withdraw));
        }

        private static void CreateClient()
        {
            Console.WriteLine();

            Console.WriteLine("Введите имя: ");
            var name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Имя обязательное поле");
            }

            Console.WriteLine("Введите фамилию: ");
            var surname = Console.ReadLine();

            if (string.IsNullOrEmpty(surname))
            {
                Console.WriteLine("Фамилия обязательное поле");
            }

            Console.WriteLine("Введите адрес: ");
            var adres = Console.ReadLine();

            Console.WriteLine("Введите паспорт: ");
            var res = Int32.TryParse(Console.ReadLine(), out int pas);

            var client = new Client(name, surname);

            if (!string.IsNullOrEmpty(adres))
                client.Adress = adres;

            if (res)
                client.Passport = pas;
        }

        private static void ShowMenu()
        {
            while (true)
            {
                var list = new List<string>
                {
                    "1. Показать банки",
                    "2. Показать счет",
                    "3. Показать клиентов",
                    "4. Показать транзакции",
                    "5. Назад"
                };

                Draw(list);

                Console.WriteLine();
                Console.Write("Выберите действие: ");
                var data = Convert.ToInt32(Console.ReadLine());

                switch (data)
                {
                    case 1:
                        ShowBanks();
                        break;
                    case 2:
                        ShowAccounts();
                        break;
                    case 3:
                        ShowClients();
                        break;
                    case 4:
                        ShowTransactions();
                        break;
                    case 5:
                        return;
                    default:
                        continue;
                }
            }
        }

        private static void ShowTransactions()
        {
            Console.WriteLine();

            Console.WriteLine("Транзации:");

            foreach (var transaction in CentralBank.Transactions.OrderBy(x=> x.Id))
            {
               Console.WriteLine($@"{transaction.Id}:");
               Console.WriteLine($@"    Отменена: {transaction.IsCanseled}");
               Console.WriteLine($@"    Операции со счетами:");
               foreach (var i in transaction.Operations)
               { 
                   Console.WriteLine($@"        Счёт ({i.Key}): {i.Value}");
               }
            }
        }

        private static void ShowClients()
        {
            Console.WriteLine();

            Console.WriteLine("Клиенты:");

            int count = 1;
            foreach (var client in CentralBank.Clients)
            {
                string pas = string.Empty;
                string ad = string.Empty;

                if (client.Passport.HasValue)
                    pas = $" ({client.Passport.Value})";

                if (!string.IsNullOrEmpty(client.Adress))
                    ad = $": {client.Adress}";

                Console.WriteLine($@"{count}. {client.Name} {client.SurName}{pas}{ad}");
                count++;
            }
        }

        private static void ShowAccounts()
        {
            Console.WriteLine();

            Console.Write("Введите номер счета: ");
            var num = Convert.ToInt32(Console.ReadLine());

            Account account = null;

            foreach (var bank in CentralBank.Banks)
            {
                var h = bank.Accounts.Find(a => a.Id == num);

                if (h != null)
                {
                    account = h;
                    break;
                }
            }

            if (account == null)
                return;

            Console.WriteLine();
            Console.WriteLine($@"{account}:");
            Console.WriteLine($@"   Владелец: {account.Client.Name} {account.Client.SurName}");
            Console.WriteLine($@"   Тип: {account.Type}");
            Console.WriteLine($@"   Баланс: {account.Money}");
        }
        
        private static void ShowBanks()
        {
            Console.WriteLine("Банки:");

            for (int i = 1; i <= CentralBank.Banks.Count; i++)
            {
                Console.WriteLine($@"{i}. {CentralBank.Banks[i-1].Name}");
            }
        }
    }
}