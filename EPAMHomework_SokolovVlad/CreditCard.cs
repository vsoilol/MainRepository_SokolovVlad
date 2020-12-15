namespace BankGame
{
    public class CreditCard : Card
    {
        private const int percentCredit = 20;

        private readonly double monthlyDebt;

        private int numberMonthsCredit;
        private int numberMonthsPaid = 0;

        public double AmountdDebt { get; private set; }

        public CreditCard(int creditAmount, int numberMonthsCredit)
        {
            this.numberMonthsCredit = numberMonthsCredit;

            monthlyDebt = (percentCredit / 100D) * (creditAmount / numberMonthsCredit) + (creditAmount / numberMonthsCredit);
            AmountdDebt = 0;

            AddDebt();

            ConsoleProvider.ShowNameCard(Name, CardType.Credit);
        }

        public void RepayDebt()
        {
            numberMonthsPaid += (int)(AmountdDebt / monthlyDebt);
            AmountdDebt = 0;
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

        public override bool TransferToCard(Account account, Account transferableAccount, int amountMoney)
        {
            if ((transferableAccount is DepositAccount) || account.IsMoneyLessZero())
            {
                return false;
            }
            else
            {
                return base.TransferToCard(account, transferableAccount, amountMoney);
            }
        }
    }
}
