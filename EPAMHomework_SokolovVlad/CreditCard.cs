namespace BankGame
{
    public class CreditCard : Card
    {
        private const int percentCredit = 20;
        
        private int numberMonthsCredit;
        private int numberMonthsPaid = 0;
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

        public bool RepayDebt()
        {
            if (WithdrawMoney(AmountdDebt))
            {
                numberMonthsPaid += (int)(AmountdDebt / monthlyDebt);
                AmountdDebt = 0;
                return true;
            }
            return false;
        }

        public bool IsDeptRepay()
        {
            return (numberMonthsPaid == numberMonthsCredit) ? true : false;
        }

        public void AddDebt()
        {
            if (!IsMonthlyDeptRepay())
            {
                numberMonthsCredit++;
            }
            AmountdDebt += monthlyDebt;

        }

        public bool IsMonthlyDeptRepay()
        {
            return AmountdDebt == 0 ? true : false;
        }

        public bool IsMoneyLessZero()
        {
            return Money < 0 ? true : false;
        }

        public override bool TransferCard(Card transferableCard, int amountMoney)
        {
            if ((transferableCard is DepositCard) || IsMoneyLessZero() || !IsMonthlyDeptRepay())
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
