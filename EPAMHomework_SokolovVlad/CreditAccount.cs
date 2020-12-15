using System.Collections.Generic;

namespace BankGame
{
    public class CreditAccount : Account
    {
        public List<CreditCard> Cards { get; private set; }

        public CreditAccount() : base()
        {
            Cards = new List<CreditCard>();
        }

        public override void AddCardToAccount()
        {
            int creditAmount = ConsoleProvider.SetMoney(CardType.Credit);
            int numberMonthsCredit = ConsoleProvider.SetNumberMonthsCredit();

            Cards.Add(new CreditCard(creditAmount, numberMonthsCredit));

            Money += creditAmount;
        }

        public override bool AnyCardsInAccount()
        {
            return (Cards.Count != 0) ? true : false;
        }

        public override int GetNumberCards()
        {
            return Cards.Count;
        }

        public bool RepayDebt(CreditCard card)
        {
            if (WithdrawMoneyFromAccount(card.AmountdDebt))
            {
                card.RepayDebt();
                return true;
            }

            return false;
        }

        public void DeleteCard(CreditCard card)
        {
            Cards.Remove(card);
        }

        public bool IsMonthlyDeptRepay()
        {
            foreach (CreditCard card in Cards)
            {
                if (!card.IsMonthlyDeptRepay())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
