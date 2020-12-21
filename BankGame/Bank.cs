using System;
using System.Collections.Generic;

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
                    ConsoleProvider.ChooseAccount(ConsoleProvider.ChooseTransferToAccount);
                    Account account = ChooseAccount(true);

                    ConsoleProvider.ChooseAccount(ConsoleProvider.ChoosePutToAccountMoney);
                    account.TransferMoneyToCard(ChooseAccount(true));
                    break;
                case (int)BankOperationNumber.TransferMoneyToAccount:
                    ConsoleProvider.ChooseAccount(ConsoleProvider.ChooseTransferToAccount);
                    ChooseAccount(true).TransferMoneyToAccount();
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
