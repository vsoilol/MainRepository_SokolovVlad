using System;

namespace BankGame
{
    public class Credit
    {
        private const int PercentCredit = 20;
        private const int CreditNumberLength = 4;

        private readonly double monthlyDebt;

        private int numberMonthsCredit;
        private int numberMonthsPaid = 0;

        private readonly Random randomNumber;

        public double AmountdDebt { get; private set; }

        public string CreditNumber { get; private set; }

        public Credit(int creditAmount, int numberMonthsCredit)
        {
            randomNumber = new Random();
            CreateCreditNumber();

            this.numberMonthsCredit = numberMonthsCredit;

            monthlyDebt = (PercentCredit / 100D) * (creditAmount / numberMonthsCredit) + (creditAmount / numberMonthsCredit);
            AmountdDebt = 0;

            AddDebt();
        }

        public void CreateCreditNumber()
        {
            CreditNumber = "";

            for (int i = 0; i < CreditNumberLength; i++)
            {
                CreditNumber += randomNumber.Next(1, 9);
            }
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
    }
}
