using System;
using System.Collections.Generic;
using System.Text;

namespace BankGame
{
    public abstract class Account
    {
        private const int maxNameLength = 20;
        private const string dictionaryString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

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
    }
}
