using System;
using System.Collections.Generic;

namespace BankGame
{
    public class CreditAccount : Account
    {
        public List<Credit> Credits { get; private set; }

        public CreditAccount() : base()
        {
            Credits = new List<Credit>();
        }

        public void AddCreditToAccount()
        {
            int creditAmount = ConsoleProvider.SetCredit();
            int numberMonthsCredit = ConsoleProvider.SetNumberMonthsCredit();

            Credits.Add(new Credit(creditAmount, numberMonthsCredit));

            Money += creditAmount;
        }

        public bool IsDebtRepay(Credit credit)
        {
            if (AreMoneyWithdrawFromAccount(credit.AmountdDebt))
            {
                credit.RepayDebt();
                return true;
            }

            return false;
        }

        public void DeleteCreditFromAccount(Credit credit)
        {
            Credits.Remove(credit);
        }

        public bool IsMonthlyDeptRepay()
        {
            foreach (Credit credit in Credits)
            {
                if (!credit.IsMonthlyDeptRepay())
                {
                    return false;
                }
            }
            return true;
        }

        public Credit ShowCredits(bool isOperation)
        {
            Console.Clear();

            int numberCredit = 1;

            foreach (Credit credit in Credits)
            {
                Console.WriteLine($"{numberCredit}. Кредит {credit.CreditNumber}");
                numberCredit++;
            }

            if (numberCredit == 1)
            {
                Console.WriteLine("Нет кредитов");
                Console.ReadKey();
                return null;
            }

            if (!isOperation)
            {
                Console.ReadKey();
                return null;
            }
            else
            {
                int numberCreditResult = ConsoleProvider.GetNumber(Credits.Count) - 1;
                return Credits[numberCreditResult];
            }
        }
    }
}
