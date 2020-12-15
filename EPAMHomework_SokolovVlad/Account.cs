using System;
using System.Text;

namespace BankGame
{
    public abstract class Account
    {
        private const int maxNameLength = 20;
        private const string dictionaryString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private readonly Random randomValue;

        public string NameAccount { get; set; }
        public double Money { get; set; }

        public Account()
        {
            randomValue = new Random();

            Money = 0;

            GenerateName();
            ConsoleProvider.ShowNameAccount(NameAccount);
        }

        public void GenerateName()
        {
            StringBuilder resultStringBuilder = new StringBuilder();

            for (int i = 1; i <= maxNameLength; i++)
            {
                resultStringBuilder.Append(dictionaryString[randomValue.Next(dictionaryString.Length)]);
            }

            NameAccount = resultStringBuilder.ToString();
        }

        public void PutMoneyToAccount(double amountMoney)
        {
            Money += amountMoney;
        }

        public bool WithdrawMoneyFromAccount(double amountMoney)
        {
            if ((Money - amountMoney) < 0)
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

        public abstract void AddCardToAccount();

        public abstract bool AnyCardsInAccount();

        public abstract int GetNumberCards();
    }
}
