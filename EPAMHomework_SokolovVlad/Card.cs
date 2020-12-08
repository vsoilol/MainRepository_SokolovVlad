namespace BankGame
{
    public abstract class Card
    {
        public double Money { get; set; }

        public void PutMoney(int amountMoney)
        {
            Money += amountMoney;
        }

        public bool WithdrawMoney(int amountMoney)
        {
            if ((Money - amountMoney) < 0)
            {
                return false;
            }

            Money -= amountMoney;
            return true;
        }

        public virtual bool TransferCard(Card transferableCard, int amountMoney)
        {
            if (WithdrawMoney(amountMoney))
            {
                transferableCard.PutMoney(amountMoney);
                return true;
            }
            return false;
        }
    }
}
