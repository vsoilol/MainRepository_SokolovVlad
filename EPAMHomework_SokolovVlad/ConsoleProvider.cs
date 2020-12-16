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
        private const string transferCardToAnotherCard = "Перевести с карты на карту";
        private const string withdrawMoneyFromAccount = "Снять деньги со счета";
        private const string transferMoneyToAccount = "Перевести деньги на счет";
        private const string watchDebtOnCard = "Посмотреть задолжность";
        private const string repayDebtFromCard = "Погасить кредит";
        private const string showListAccounts = "Список счетов";
        private const string addCreditToAccount = "Взять кредит";
        private const string passMonth = "Пройти месяц";

        private const string doWhat = "Что вы хотите сделать?";

        private const string notWorkOperation = "Ошибка операции.";

        private const int firstNumberOperation = 1;
        private const int numberAccountTypes = 2;

        public const string AccountCredit = "кредитный счет";
        public const string AccountDeposit = "депозитный счет";

        public const string CardCredit = "Кредитная карта";
        public const string CardDeposit = "Депозитная карта";

        public const string CardsOnAccount = "Карт на счету";

        public static int SelectOperation(OperationType operationType)
        {
            Console.WriteLine(doWhat);
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
                    Console.WriteLine($"{(int)OperationNumber.TransferCardToAnotherCard}. {transferCardToAnotherCard}");
                    Console.WriteLine($"{(int)OperationNumber.WithdrawMoneyFromAccount}. {withdrawMoneyFromAccount}");
                    Console.WriteLine($"{(int)OperationNumber.TransferMoneyToAccount}. {transferMoneyToAccount}");
                    Console.WriteLine($"{(int)OperationNumber.WatchDebtOnCard}. {watchDebtOnCard}");
                    Console.WriteLine($"{(int)OperationNumber.RepayDebtFromCard}. {repayDebtFromCard}");
                    Console.WriteLine($"{(int)OperationNumber.ShowListAccounts}. {showListAccounts}");
                    Console.WriteLine($"{(int)OperationNumber.AddCreditToAccount}. {addCreditToAccount}");
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

            Console.WriteLine($"{(int)AccountType.Credit} - {AccountCredit}");
            Console.WriteLine($"{(int)AccountType.Deposit} - {AccountDeposit}");

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
                    Console.WriteLine($"{i}. {accounts[i - 1].NameAccount} - {AccountCredit}");
                }
                else
                {
                    Console.WriteLine($"{i}. {accounts[i - 1].NameAccount} - {AccountDeposit}");
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

        public static int SetCredit()
        {
            Console.Clear();
            Console.Write("Введите сумму кредита: ");

            int money = GetNumber();
            return money;
        }

        public static Card ShowCards(List<Card> cards, bool isOperation)
        {
            Console.Clear();
            int numberCard = 1;

            foreach (Card card in cards)
            {
                Console.WriteLine($"Карта {card.Name}");
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

        public static void InputDebt(Credit credit)
        {
            Console.Clear();
            Console.WriteLine($"Задолжность по кредиту {credit.CreditNumber} равна {credit.AmountdDebt}");
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
            Console.WriteLine(notWorkOperation);
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
    }
}
