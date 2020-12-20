using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BankGame
{
    public abstract class Account
    {
        private const int MaxNameLength = 20;
        private const string DictionaryString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private readonly Random randomValue;

        public string NameAccount { get; private set; }
        public double Money { get; protected set; }

        public List<Card> Cards { get; private set; }

        public Account()
        {
            Cards = new List<Card>();

            randomValue = new Random();

            Money = 0;

            GenerateName();
            ConsoleProvider.ShowNameAccount(NameAccount);
        }

        public void AddCardToAccount()
        {
            Cards.Add(new Card());
        }

        public void GenerateName()
        {
            StringBuilder resultStringBuilder = new StringBuilder();

            for (int i = 1; i <= MaxNameLength; i++)
            {
                resultStringBuilder.Append(DictionaryString[randomValue.Next(DictionaryString.Length)]);
            }

            NameAccount = resultStringBuilder.ToString();
        }

        public bool AreMoneyWithdrawFromAccount(double amountMoney)
        {
            if (Money < amountMoney)
            {
                return false;
            }

            Money -= amountMoney;
            return true;
        }

        public bool IsMoneyLessZero()
        {
            return Money < 0 ? true : false;
        }

        public void ShowAccount(int numberAccount)
        {
            if (this is CreditAccount)
            {
                Console.WriteLine($"{numberAccount}. {NameAccount} - {ConsoleProvider.AccountCredit}, на этом счету {Money} денег. {ConsoleProvider.CardsOnAccount} - {Cards.Count}");
            }
            else
            {
                Console.WriteLine($"{numberAccount}. {NameAccount} - {ConsoleProvider.AccountDeposit}, на этом счету {Money} денег. {ConsoleProvider.CardsOnAccount} - {Cards.Count}");
            }
        }

        public void CheckAccount()
        {
            if (this is CreditAccount)
            {
                foreach (Credit credit in (this as CreditAccount).Credits)
                {
                    credit.AddDebt();
                }
            }
            else
            {
                (this as DepositAccount).AddPercent();
            }
        }

        public void WithdrawMoneyFromAccount()
        {
            int moneyFromAccount = ConsoleProvider.EnterMoney(ConsoleProvider.WithdrawMoneyFromSelectedAccount);

            if (!AreMoneyWithdrawFromAccount(moneyFromAccount))
            {
                ConsoleProvider.ErrorOperation();
            }
        }

        public void PutMoneyToAccount()
        {
            int moneyToAccount = ConsoleProvider.EnterMoney(ConsoleProvider.EnterMoneyToAccount);

            Money += moneyToAccount;
        }

        public bool AreAccountHaveCards()
        {
            return (Cards.Count != 0 ? true : false);
        }

        public bool IsCardTransferToCard(Account transferableAccount, int amountMoney)
        {
            if (AreMoneyWithdrawFromAccount(amountMoney))
            {
                transferableAccount.Money += amountMoney;
                return true;
            }

            return false;
        }

        public virtual void ChooseOperation()
        {
            int numberOperation = ConsoleProvider.SelectOperation(OperationType.AccountsOperation);

            switch (numberOperation)
            {
                case (int)AccountsOperationNumber.AddCard:
                    AddCardToAccount();
                    break;
                case (int)AccountsOperationNumber.PutMoneyToAccount:
                    PutMoneyToAccount();
                    break;
                case (int)AccountsOperationNumber.WithdrawMoneyFromAccount:
                    WithdrawMoneyFromAccount();
                    break;
            }
        }

        public virtual void TransferMoneyToAccount()
        {
            string nameAccount;

            Regex nameAccountRegex = new Regex(@"^\w{20}$");

            do
            {
                nameAccount = ConsoleProvider.EnterAccountName();
            }
            while (!nameAccountRegex.IsMatch(nameAccount));

            int transferableMoney = ConsoleProvider.EnterMoney(ConsoleProvider.EnterTransferableAmount);

            if (!AreMoneyWithdrawFromAccount(transferableMoney))
            {
                ConsoleProvider.ErrorOperation();
            }
        }

        public virtual void TransferMoneyToCard(Account transferableAccount)
        {
            if (this == transferableAccount)
            {
                ConsoleProvider.ErrorOperation();
                return;
            }

            int transferableMoney = ConsoleProvider.EnterMoney(ConsoleProvider.EnterTransferableAmount);

            if (!IsCardTransferToCard(transferableAccount, transferableMoney))
            {
                ConsoleProvider.ErrorOperation();
            }
        }
    }
}
