using System;
using System.Collections.Generic;

namespace BankGame
{
    public static class ConsoleProvider
    {
        private const string NotNumber = "Не является числом.";
        private const string InvalidInput = "Неверный ввод.";

        private const string TryAgain = "Повторите снова.";

        private const string CreateAccount = "Создать счет";
        private const string AddCard = "Добавить карту";
        private const string PutMoneyToAccount = "Положить деньги на счет";
        private const string TransferCardToAnotherCard = "Перевести с карты на карту";
        private const string WithdrawMoneyFromAccount = "Снять деньги со счета";
        private const string TransferMoneyToAccount = "Перевести деньги на счет";
        private const string WatchDebtOnCard = "Посмотреть задолжность";
        private const string RepayDebtFromCard = "Погасить кредит";
        private const string ShowListAccounts = "Список счетов";
        private const string AddCreditToAccount = "Взять кредит";
        private const string PassMonth = "Пройти месяц";

        private const string DoWhat = "Что вы хотите сделать?";

        private const string NotWorkOperation = "Ошибка операции.";

        private const int FirstNumberOperation = 1;
        private const int NumberAccountTypes = 2;

        public const string AccountCredit = "кредитный счет";
        public const string AccountDeposit = "депозитный счет";

        public const string CardCredit = "Кредитная карта";
        public const string CardDeposit = "Депозитная карта";

        public const string WithdrawMoneyFromSelectedAccount = "Введите сумму которую хотите снять со счета: ";
        public const string EnterMoneyToAccount = "Введите сумму которую хотите положить на счёт: ";
        public const string ChooseAccountTransferToAccount = "Выберете счет с которой будет производится перевод.";
        public const string ChoosePutToAccountMoney = "Выберете счет на который будет сделан перевод.";
        public const string EnterTransferableAmount = "Введите сумму для перевода: ";

        public const string CardsOnAccount = "Карт на счету";

        public static int SelectOperation(OperationType operationType)
        {
            Console.WriteLine(DoWhat);
            int result = 0;

            switch (operationType)
            {
                case OperationType.OnlyCreateAccount:
                    Console.WriteLine($"{(int)OperationNumber.CreateAccount}. {CreateAccount}");
                    result = GetNumber((int)OperationType.OnlyCreateAccount);
                    break;

                case OperationType.CreateAccountWithAddCards:
                    Console.WriteLine($"{(int)OperationNumber.CreateAccount}. {CreateAccount}");
                    Console.WriteLine($"{(int)OperationNumber.AddCard}. {AddCard}");
                    result = GetNumber((int)OperationType.CreateAccountWithAddCards);
                    break;

                case OperationType.AllOperation:
                    Console.WriteLine($"{(int)OperationNumber.CreateAccount}. {CreateAccount}");
                    Console.WriteLine($"{(int)OperationNumber.AddCard}. {AddCard}");
                    Console.WriteLine($"{(int)OperationNumber.PutMoneyToAccount}. {PutMoneyToAccount}");
                    Console.WriteLine($"{(int)OperationNumber.TransferCardToAnotherCard}. {TransferCardToAnotherCard}");
                    Console.WriteLine($"{(int)OperationNumber.WithdrawMoneyFromAccount}. {WithdrawMoneyFromAccount}");
                    Console.WriteLine($"{(int)OperationNumber.TransferMoneyToAccount}. {TransferMoneyToAccount}");
                    Console.WriteLine($"{(int)OperationNumber.WatchDebtOnCard}. {WatchDebtOnCard}");
                    Console.WriteLine($"{(int)OperationNumber.RepayDebtFromCard}. {RepayDebtFromCard}");
                    Console.WriteLine($"{(int)OperationNumber.ShowListAccounts}. {ShowListAccounts}");
                    Console.WriteLine($"{(int)OperationNumber.AddCreditToAccount}. {AddCreditToAccount}");
                    Console.WriteLine($"{(int)OperationNumber.PassMonth}. {PassMonth}");

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
                    if (resultNumber <= maxNumber && resultNumber >= FirstNumberOperation)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(InvalidInput);
                        Console.WriteLine(TryAgain);
                    }
                }
                else
                {
                    Console.WriteLine(NotNumber);
                    Console.WriteLine(TryAgain);
                }
            }

            return resultNumber;
        }

        public static int GetNumber()
        {
            int resultNumber;

            while (!int.TryParse(Console.ReadLine(), out resultNumber))
            {
                Console.WriteLine(NotNumber);
                Console.WriteLine(TryAgain);
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

            AccountType result = (AccountType)GetNumber(NumberAccountTypes);
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
            Console.WriteLine("Этот счет не кредитный.\n" + TryAgain);
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
            Console.WriteLine(NotWorkOperation);
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
