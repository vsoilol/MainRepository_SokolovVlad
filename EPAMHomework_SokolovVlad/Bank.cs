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
                else if (!AreAccountsHaveCards())
                {
                    ChooseOperation(OperationType.CreateAccountWithAddCards);
                }
                else
                {
                    ChooseOperation(OperationType.AllOperation);
                }
            }
        }

        public void ChooseOperation(OperationType operationType)
        {
            int numberOperation = ConsoleProvider.SelectOperation(operationType);

            switch (numberOperation)
            {
                case (int)OperationNumber.CreateAccount:
                    AddAccount();
                    break;
                case (int)OperationNumber.AddCard:
                    AddCardToAccount();
                    break;
                case (int)OperationNumber.PutMoneyToAccount:
                    PutMoneyToAccount();
                    break;
                case (int)OperationNumber.TransferCardToAnotherCard:
                    TransferFromAccountToCard();
                    break;
                case (int)OperationNumber.WithdrawMoneyFromAccount:
                    WithdrawMoneyFromAccount();
                    break;
                case (int)OperationNumber.TransferMoneyToAccount:
                    TransferAccountToAccount();
                    break;
                case (int)OperationNumber.WatchDebtOnCard:
                    RepayDebt(false);
                    break;
                case (int)OperationNumber.RepayDebtFromCard:
                    RepayDebt(true);
                    break;
                case (int)OperationNumber.ShowListAccounts:
                    ChooseAccount(false);
                    break;
                case (int)OperationNumber.AddCreditToAccount:
                    AddCreditToAccount();
                    break;
                case (int)OperationNumber.PassMonth:
                    PassMonth();
                    break;
            }
        }

        public void AddCreditToAccount()
        {
            Account account = ChooseAccount(true);

            account.AddCreditToAccount();
        }

        public bool AreAccountsHaveCards()
        {
            foreach (Account account in accounts)
            {
                if (account.AreAccountHaveCards())
                {
                    return true;
                }
            }

            return false;
        }

        public void AddCardToAccount()
        {
            int numberAccount = ConsoleProvider.ChooseAccount(accounts);
            accounts[numberAccount].AddCardToAccount();
        }

        public void PutMoneyToAccount()
        {
            Account account = ChooseAccount(true);

            account.PutMoneyToAccount();
        }

        public void WithdrawMoneyFromAccount()
        {
            Account account = ChooseAccount(true);

            account.WithdrawMoneyFromAccount();
        }

        public void RepayDebt(bool isOperation)
        {
            Account account;

            while (true)
            {
                account = ChooseAccount(true);

                if (account == null)
                {
                    return;
                }

                if (account is DepositAccount)
                {
                    ConsoleProvider.NotCreditAccount();
                }
                else
                {
                    (account as CreditAccount).RepayDebt(isOperation);
                    break;
                }
            }
        }

        public void PassMonth()
        {
            foreach (Account account in accounts)
            {
                account.CheckAccount();
            }
        }

        public void TransferFromAccountToCard()
        {
            ConsoleProvider.ChooseCard(ConsoleProvider.ChooseAccountTransferToAccount);
            Account account = ChooseAccount(true);

            ConsoleProvider.ChooseCard(ConsoleProvider.ChoosePutToAccountMoney);
            Account transferableAccount = ChooseAccount(true);

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

        public void TransferAccountToAccount()
        {
            ConsoleProvider.ChooseCard(ConsoleProvider.ChooseAccountTransferToAccount);
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
