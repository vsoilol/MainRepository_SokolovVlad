namespace BankGame
{
    public class CreditCard : Card
    {
        private const int percentCredit = 20;
        
        private readonly int numberMonthsCredit;
        private readonly int numberMonthsPaid = 0;
        private readonly double monthlyDebt;

        public double AmountdDebt { get; private set; }
        public CreditCard()
        {
            Money = ConsoleProvider.SetMoney(CardType.Credit);
            numberMonthsCredit = ConsoleProvider.SetNumberMonthsCredit();

            monthlyDebt = (percentCredit / 100D) * (Money / numberMonthsCredit) + (Money / numberMonthsCredit);
            AmountdDebt = 0;

            AddDebt();
        }

        public void RepayDebt()
        {
            Money -= AmountdDebt;
            AmountdDebt = 0;
        }

        public void AddDebt()
        {
            AmountdDebt += monthlyDebt;
        }

        public bool IsDeptRepay()
        {
            return AmountdDebt == 0 ? true : false;
        }

        public bool IsMoneyLessZero()
        {
            return Money < 0 ? true : false;
        }

        public override bool TransferCard(Card transferableCard, int amountMoney)
        {
            if ((transferableCard is DepositCard) || IsMoneyLessZero() || !IsDeptRepay())
            {
                return false;
            }
            else
            {
                return base.TransferCard(transferableCard, amountMoney);
            }
        }
    }
}
