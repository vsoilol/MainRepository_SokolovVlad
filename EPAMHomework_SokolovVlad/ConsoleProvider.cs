using System;
using System.Collections.Generic;

namespace BankGame
{
    public static class ConsoleProvider
    {
        private const string notNumber = "Не является числом.";
        private const string invalidInput = "Неверный ввод.";

        private const string tryAgain = "Повторите снова.";

        private const string createAccount = "Создать счет";
        private const string addCard = "Добавить карту";
        private const string putMoneyToAccount = "Положить деньги на счет";
        private const string transferFunds = "Перевести с карты на карту";
        private const string withdrawMoneyFromAccount = "Снять деньги со счета";
        private const string transferMoneyToAccount = "Перевести деньги на счет";
        private const string watchDebt = "Посмотреть задолжность";
        private const string repayDebt = "Погасить кредит";
        private const string listAccounts = "Список счетов";
        private const string passMonth = "Пройти месяц";

        private const string whatDo = "Что вы хотите сделать?";

        private const string errorOperation = "Ошибка операции.";

        private const int firstNumberOperation = 1;
        private const int numberCardsTypes = 2;
        private const int numberAccountTypes = 2;

        public static int SelectOperation(OperationType operationType)
        {
            Console.WriteLine(whatDo);
            int result = 0;

            switch (operationType)
            {
                case OperationType.OnlyCreateAccount:
                    Console.WriteLine($"{(int)OperationNumber.CreateAccount}. {createAccount}");
                    result = GetNumber((int)OperationType.OnlyCreateAccount);
                    break;

                case OperationType.CreateAccountWithAddCards:
                    Console.WriteLine($"{(int)OperationNumber.CreateAccount}. {createAccount}");
                    Console.WriteLine($"{(int)OperationNumber.AddCard}. {addCard}");
                    result = GetNumber((int)OperationType.CreateAccountWithAddCards);
                    break;

                case OperationType.AllOperation:
                    Console.WriteLine($"{(int)OperationNumber.CreateAccount}. {createAccount}");
                    Console.WriteLine($"{(int)OperationNumber.AddCard}. {addCard}");
                    Console.WriteLine($"{(int)OperationNumber.PutMoneyToAccount}. {putMoneyToAccount}");
                    Console.WriteLine($"{(int)OperationNumber.TransferFunds}. {transferFunds}");
                    Console.WriteLine($"{(int)OperationNumber.WithdrawMoneyFromAccount}. {withdrawMoneyFromAccount}");
                    Console.WriteLine($"{(int)OperationNumber.TransferMoneyToAccount}. {transferMoneyToAccount}");
                    Console.WriteLine($"{(int)OperationNumber.WatchDebt}. {watchDebt}");
                    Console.WriteLine($"{(int)OperationNumber.RepayDebt}. {repayDebt}");
                    Console.WriteLine($"{(int)OperationNumber.ListAccounts}. {listAccounts}");
                    Console.WriteLine($"{(int)OperationNumber.PassMonth}. {passMonth}");

                    result = GetNumber((int)OperationType.AllOperation);
                    break;
            }
            return result;
        }

        public static void GreetCustomer(string nameBank)
        {
            Console.Clear();
            Console.WriteLine($"Вас приветствует банк {nameBank}.");
        }

        public static int GetNumber(int maxNumber)
        {
            int resultNumber;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out resultNumber))
                {
                    if (resultNumber <= maxNumber && resultNumber >= firstNumberOperation)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(invalidInput);
                        Console.WriteLine(tryAgain);
                    }
                }
                else
                {
                    Console.WriteLine(notNumber);
                    Console.WriteLine(tryAgain);
                }
            }

            return resultNumber;
        }

        public static int GetNumber()
        {
            int resultNumber;

            while (!int.TryParse(Console.ReadLine(), out resultNumber))
            {
                Console.WriteLine(notNumber);
                Console.WriteLine(tryAgain);
            }

            return resultNumber;
        }

        public static void ShowNameAccount(string nameAccounts)
        {
            Console.Clear();
            Console.WriteLine($"Имя счета {nameAccounts}");
            Console.ReadKey();
        }

        public static AccountType ChooseAccountType()
        {
            Console.Clear();
            Console.WriteLine("Выберете тип счета.");

            Console.WriteLine($"{(int)AccountType.Credit}. Кредитный");
            Console.WriteLine($"{(int)AccountType.Deposit}. Депозитный");

            AccountType result = (AccountType)GetNumber(numberAccountTypes);
            return result;
        }

        public static int ChooseAccount(List<Account> accounts)
        {
            Console.Clear();
            Console.WriteLine("Выберете счет:");

            for (int i = 1; i <= accounts.Count; i++)
            {
                if (accounts[i - 1] is CreditAccount)
                {
                    Console.WriteLine($"{i}. {accounts[i - 1].NameAccount} - кредитный счет");
                }
                else
                {
                    Console.WriteLine($"{i}. {accounts[i - 1].NameAccount} - депозитный счет");
                }
            }

            int numberAccount = GetNumber(accounts.Count);
            return numberAccount - 1;
        }

        public static int SetNumberMonthsCredit()
        {
            Console.Write("Введите количество месяцев: ");
            int numberMonthsCredit = GetNumber();
            return numberMonthsCredit;
        }

        public static int SetMoney(CardType cardType)
        {
            Console.Clear();
            switch (cardType)
            {
                case CardType.Credit:
                    Console.Write("Введите сумму кредита: ");
                    break;
                case CardType.Deposit:
                    Console.Write("Введите сумму депозита: ");
                    break;
            }

            int money = GetNumber();
            return money;
        }

        public static T ShowCards<T>(List<T> cards, bool isOperation)
        {
            Console.Clear();
            int numberCard = 1;

            foreach (T card in cards)
            {
                if (typeof(T) == typeof(CreditCard))
                {
                    Console.WriteLine($"{numberCard}. Кредитная карта - {(card as CreditCard).Name}");
                }
                else
                {
                    Console.WriteLine($"{numberCard}. Депозитная карта - {(card as DepositCard).Name}");
                }
                numberCard++;
            }

            if (numberCard == 1)
            {
                Console.WriteLine("Нет карт");
                Console.ReadKey();
                return default;
            }

            if (!isOperation)
            {
                Console.ReadKey();
                return default;
            }
            else
            {
                int numberCardResult = GetNumber(cards.Count) - 1;
                return cards[numberCardResult];
            }
        }

        public static void NotCreditAccount()
        {
            Console.Clear();
            Console.WriteLine("Этот счет не кредитный.\n" + tryAgain);
            Console.ReadKey();
        }

        public static void InputDebt(CreditCard creditCard)
        {
            Console.Clear();
            Console.WriteLine($"Задолжность на карте {creditCard.Name} равна {creditCard.AmountdDebt}");
            Console.ReadKey();
        }

        public static void ChooseCard(string text)
        {
            Console.Clear();
            Console.WriteLine(text);
            Console.ReadKey();
        }

        public static void ErrorOperation()
        {
            Console.Clear();
            Console.WriteLine(errorOperation);
            Console.ReadKey();
        }

        public static int EnterMoney(string text)
        {
            Console.Clear();
            Console.Write(text);

            int moneyCard = GetNumber();
            return moneyCard;
        }

        public static string EnterAccountName()
        {
            Console.Clear();
            Console.Write("Введите номер счета (20 символов состоящих из английских букв и цифр): ");
            string nameAccount = Console.ReadLine();
            return nameAccount;
        }

        public static Account ShowListAccounts(List<Account> accounts, bool isOperation)
        {
            Console.Clear();
            int numberAccount = 1;

            foreach (Account account in accounts)
            {
                if (account is CreditAccount)
                {
                    Console.WriteLine($"{numberAccount}. {account.NameAccount} - кредитный счет, на этом счету {account.Money} денег. Карт на счету - {account.GetNumberCards()}");
                }
                else
                {
                    Console.WriteLine($"{numberAccount}. {account.NameAccount} - депозитный счет, на этом счету {account.Money} денег. Карт на счету - {account.GetNumberCards()}");
                }
                numberAccount++;
            }

            if (isOperation)
            {
                int numberAccountResult = GetNumber(accounts.Count) - 1;
                return accounts[numberAccountResult];
            }
            else
            {
                Console.ReadKey();
                return null;
            }
        }

        public static void ShowNameCard(string name, CardType cardType)
        {
            Console.Clear();

            switch (cardType)
            {
                case CardType.Credit:
                    Console.WriteLine($"Кредитная карта - {name}");
                    break;
                case CardType.Deposit:
                    Console.WriteLine($"Депозитная карта - {name}");
                    break;
            }

            Console.ReadKey();
        }
    }
}
