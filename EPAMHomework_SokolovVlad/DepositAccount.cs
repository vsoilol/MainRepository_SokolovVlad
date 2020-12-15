using System.Collections.Generic;

namespace BankGame
{
    public class DepositAccount : Account
    {
        private const int percentDeposit = 5;

        public List<DepositCard> Cards { get; private set; }

        public DepositAccount() : base()
        {
            Cards = new List<DepositCard>();
        }

        public override void AddCardToAccount()
        {
            Cards.Add(new DepositCard());
        }

        public override bool AnyCardsInAccount()
        {
            return (Cards.Count != 0) ? true : false;
        }

        public override int GetNumberCards()
        {
            return Cards.Count;
        }

        public void AddPercent()
        {
            double monthlyPercent = (Money * percentDeposit) / 100D;
            Money += monthlyPercent;
        }
    }
}
