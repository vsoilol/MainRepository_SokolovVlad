using System;
using System.Collections.Generic;

namespace BankGame
{
    public class CreditAccount : Account
    {
        private const int PercentCredit = 20;

        public List<Credit> Credits { get; private set; }

        public CreditAccount() : base()
        {
            Credits = new List<Credit>();
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
                Console.WriteLine($"{numberCredit}. Номер кредита {credit.CreditNumber}");
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

        public void RepayDebt(bool isOperation)
        {
            Credit credit = ShowCredits(true);

            if (credit != null)
            {
                ConsoleProvider.InputDebt(credit);

                if (isOperation)
                {
                    if (IsMoneyLessZero())
                    {
                        ConsoleProvider.ErrorOperation();
                    }
                    else
                    {
                        if (!IsDebtRepay(credit))
                        {
                            ConsoleProvider.ErrorOperation();
                        }
                    }

                    if (credit.IsDeptRepay())
                    {
                        DeleteCreditFromAccount(credit);
                    }
                }
            }
        }

        public void AddCreditToAccount()
        {
            Console.Clear();
            Console.WriteLine($"Процент кредита {PercentCredit}");

            int creditAmount = ConsoleProvider.SetCredit();
            int numberMonthsCredit = ConsoleProvider.SetNumberMonthsCredit();

            Credits.Add(new Credit(creditAmount, numberMonthsCredit, PercentCredit));

            Money += creditAmount;
        }

        public override void ChooseOperation()
        {
            int numberOperation = ConsoleProvider.SelectOperation(OperationType.CreditAccountsOperation);

            switch (numberOperation)
            {
                case (int)AccountsOperationNumber.AddCard:
                    AddCardToAccount();
                    break;
                case (int)AccountsOperationNumber.PutMoneyToAccount:
                    PutMoneyToAccount();
                    break;
                case (int)AccountsOperationNumber.WithdrawMoneyFromAccount:
                    WithdrawMoneyFromAccount();
                    break;
                case (int)AccountsOperationNumber.RepayDebtFromCard:
                    RepayDebt(true);
                    break;
                case (int)AccountsOperationNumber.WatchDebtOnCard:
                    RepayDebt(false);
                    break;
                case (int)AccountsOperationNumber.AddCreditToAccount:
                    AddCreditToAccount();
                    break;
            }
        }

        public override void TransferMoneyToAccount()
        {
            if (!IsMonthlyDeptRepay())
            {
                ConsoleProvider.ErrorOperation();
            }
            else
            {
                base.TransferMoneyToAccount();
            }
        }

        public override void TransferMoneyToCard(Account transferableAccount)
        {
            if ((transferableAccount is DepositAccount) || IsMoneyLessZero() || !IsMonthlyDeptRepay())
            {
                ConsoleProvider.ErrorOperation();
            }
            else
            {
                base.TransferMoneyToCard(transferableAccount);
            }
        }
    }
}
