using System;

namespace BankGame
{
    public class Card
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
    }
}
