using System;

namespace BankGame
{
    public abstract class Card
    {
        private const int lengthName = 16;

        private readonly Random randomNumber;

        public string Name { get; private set; }

        public Card()
        {
            randomNumber = new Random();

            Name = GenerateName();
        }

        public string GenerateName()
        {
            string randomWord = "";

            for (int i = 0; i < lengthName; i++)
            {
                randomWord += randomNumber.Next(1, 9);
            }

            return randomWord;
        }

        public virtual bool TransferToCard(Account account, Account transferableAccount, int amountMoney)
        {
            if (account.WithdrawMoneyFromAccount(amountMoney))
            {
                transferableAccount.PutMoneyToAccount(amountMoney);
                return true;
            }

            return false;
        }
    }
}
