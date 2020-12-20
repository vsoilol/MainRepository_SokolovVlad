using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BankGame
{
    public class Bank
    {
        private readonly List<Account> accounts;

        public string NameBank { get; private set; }

        public Bank(string nameBank)
        {
            NameBank = nameBank;

            accounts = new List<Account>();
        }

        public void AddAccount()
        {
            AccountType typeAccount = ConsoleProvider.ChooseAccountType();

            switch (typeAccount)
            {
                case AccountType.Credit:
                    accounts.Add(new CreditAccount());
                    break;
                case AccountType.Deposit:
                    accounts.Add(new DepositAccount());
                    break;
            }
        }

        public void StartWork()
        {
            while (true)
            {
                ConsoleProvider.GreetCustomer(NameBank);

                if (accounts.Count == 0)
                {
                    ChooseOperation(OperationType.OnlyCreateAccount);
                }
                else
                {
                    ChooseOperation(OperationType.AllBankOperation);
                }
            }
        }

        public void ChooseOperation(OperationType operationType)
        {
            int numberOperation = ConsoleProvider.SelectOperation(operationType);

            switch (numberOperation)
            {
                case (int)BankOperationNumber.CreateAccount:
                    AddAccount();
                    break;
                case (int)BankOperationNumber.ChooseOperationAccounts:
                    ChooseAccount(true).ChooseOperation();
                    break;
                case (int)BankOperationNumber.TransferMoneyToCard:
                    TransferMoneyToCard();
                    break;
                case (int)BankOperationNumber.TransferMoneyToAccount:
                    TransferMoneyToAccount();
                    break;
                case (int)BankOperationNumber.ShowListAccounts:
                    ChooseAccount(false);
                    break;
                case (int)BankOperationNumber.PassMonth:
                    PassMonth();
                    break;
            }
        }

        public void PassMonth()
        {
            foreach (Account account in accounts)
            {
                account.CheckAccount();
            }
        }

        public void TransferMoneyToCard()
        {
            ConsoleProvider.ChooseAccount(ConsoleProvider.ChooseTransferToAccount);
            Account account = ChooseAccount(true);

            ConsoleProvider.ChooseAccount(ConsoleProvider.ChoosePutToAccountMoney);
            Account transferableAccount = ChooseAccount(true);

            if (account == transferableAccount)
            {
                ConsoleProvider.ErrorOperation();
                return;
            }

            int transferableMoney = ConsoleProvider.EnterMoney(ConsoleProvider.EnterTransferableAmount);

            if (account is CreditAccount)
            {
                if ((account as CreditAccount).IsMonthlyDeptRepay())
                {
                    if ((transferableAccount is DepositAccount) || account.IsMoneyLessZero() || !account.IsCardTransferToCard(transferableAccount, transferableMoney))
                    {
                        ConsoleProvider.ErrorOperation();
                    }
                }
                else
                {
                    ConsoleProvider.ErrorOperation();
                }
            }
            else
            {
                if (!account.IsCardTransferToCard(transferableAccount, transferableMoney))
                {
                    ConsoleProvider.ErrorOperation();
                }
            }
        }

        public void TransferMoneyToAccount()
        {
            ConsoleProvider.ChooseAccount(ConsoleProvider.ChooseTransferToAccount);
            Account account = ChooseAccount(true);

            string nameAccount;

            Regex nameAccountRegex = new Regex(@"^\w{20}$");

            do
            {
                nameAccount = ConsoleProvider.EnterAccountName();
            }
            while (!nameAccountRegex.IsMatch(nameAccount));

            int transferableMoney = ConsoleProvider.EnterMoney(ConsoleProvider.EnterTransferableAmount);

            if ((account is CreditAccount) && !(account as CreditAccount).IsMonthlyDeptRepay())
            {
                ConsoleProvider.ErrorOperation();
            }
            else
            {
                if (!account.AreMoneyWithdrawFromAccount(transferableMoney))
                {
                    ConsoleProvider.ErrorOperation();
                }
            }
        }

        public Account ChooseAccount(bool isOperation)
        {
            Console.Clear();

            int numberAccount = 1;

            foreach (Account account in accounts)
            {
                account.ShowAccount(numberAccount);
                numberAccount++;
            }

            if (isOperation)
            {
                int numberAccountResult = ConsoleProvider.GetNumber(accounts.Count) - 1;
                return accounts[numberAccountResult];
            }
            else
            {
                Console.ReadKey();
                return null;
            }
        }
    }
}
