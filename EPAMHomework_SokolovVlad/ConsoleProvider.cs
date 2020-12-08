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
        private const string putMoneyCard = "Положить деньги на карту";
        private const string transferFunds = "Перевести с карты на карту";
        private const string listCards = "Список карт";
        private const string withdrawMoney = "Снять деньги с карты";
        private const string transferMoneyToAccount = "Перевести деньги на счет";
        private const string watchDebt = "Посмотреть задолжность";
        private const string repayDebt = "Погасить кредит";
        private const string passMonth = "Пройти месяц";

        private const string whatDo = "Что вы хотите сделать?";

        private const string errorOperation = "Ошибка операции.";

        private const int firstNumberOperation = 1;
        private const int numberCardsTypes = 2;

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
                    Console.WriteLine($"{(int)OperationNumber.PutMoneyCard}. {putMoneyCard}");
                    Console.WriteLine($"{(int)OperationNumber.TransferFunds}. {transferFunds}");
                    Console.WriteLine($"{(int)OperationNumber.ListCards}. {listCards}");
                    Console.WriteLine($"{(int)OperationNumber.WithdrawMoney}. {withdrawMoney}");
                    Console.WriteLine($"{(int)OperationNumber.TransferMoneyToAccount}. {transferMoneyToAccount}");
                    Console.WriteLine($"{(int)OperationNumber.WatchDebt}. {watchDebt}");
                    Console.WriteLine($"{(int)OperationNumber.RepayDebt}. {repayDebt}");
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

        public static int ChooseCardType()
        {
            Console.Clear();
            Console.WriteLine("Выберете тип карты.");

            Console.WriteLine($"{(int)CardType.Credit}. Кредитная");
            Console.WriteLine($"{(int)CardType.Deposit}. Депозитная");

            int result = GetNumber(numberCardsTypes);
            return result;
        }

        public static int ChooseAccount(List<Account> accounts)
        {
            Console.Clear();
            Console.WriteLine("Выберете счет:");
            for (int i = 1; i <= accounts.Count; i++)
            {
                Console.WriteLine($"{i}. {accounts[i - 1].NameAccount}");
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

        public static double SetMoney(CardType cardType)
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

        public static Card ShowCards(List<Card> cards, string nameAccount, bool isOperation)
        {
            Console.Clear();
            int numberCard = 1;

            foreach (Card card in cards)
            {
                if (card is CreditCard)
                {
                    Console.WriteLine($"({numberCard}) На счету {nameAccount} кредитная карта - {card.Money}");
                }
                else
                {
                    Console.WriteLine($"({numberCard}) На счету {nameAccount} депозитная карта - {card.Money}");
                }
                numberCard++;
            }

            if (!isOperation)
            {
                Console.ReadKey();
                return null;
            }
            else
            {
                int numberCardResult = GetNumber(cards.Count) - 1;
                return cards[numberCardResult];
            }
        }

        public static void NotCreditCard()
        {
            Console.Clear();
            Console.WriteLine("Эта карта не кредитная.\n" + tryAgain);
            Console.ReadKey();
        }

        public static void InputDebt(CreditCard creditCard)
        {
            Console.Clear();
            Console.WriteLine($"Кредитная карта с {creditCard.Money}. Задолжность {creditCard.AmountdDebt}");
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
    }
}
