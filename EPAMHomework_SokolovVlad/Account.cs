using System;
using System.Collections.Generic;
using System.Text;

namespace BankGame
{
    public class Account
    {
        private const int maxNameLength = 20;
        private readonly Random randomValue;

        public List<Card> Cards { get; private set; }

        public string NameAccount { get; set; }

        public Account()
        {
            Cards = new List<Card>();
            randomValue = new Random();

            GenerateName();
            ConsoleProvider.ShowNameAccount(NameAccount);
        }

        public void GenerateName()
        {
            const string dictionaryString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder resultStringBuilder = new StringBuilder();

            for (int i = 1; i <= maxNameLength; i++)
            {
                resultStringBuilder.Append(dictionaryString[randomValue.Next(dictionaryString.Length)]);
            }

            NameAccount = resultStringBuilder.ToString();
        }

        public void AddCreditCard()
        {
            Cards.Add(new CreditCard());
        }

        public void AddDepositCard()
        {
            Cards.Add(new DepositCard());
        }

        public void ChooseCard()
        {
            CardType cardType = (CardType)ConsoleProvider.ChooseCardType();

            switch (cardType)
            {
                case CardType.Credit:
                    AddCreditCard();
                    break;
                case CardType.Deposit:
                    AddDepositCard();
                    break;
            }
        }
    }
}
