using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BankGame
{
    public class Bank
    {
        private const string chooseAccountTransferToAccount = "Выберете счет с которой будет производится перевод.";
        private const string choosePutToAccountMoney = "Выберете счет на который будет сделан перевод.";
        private const string enterTransferableAmount = "Введите сумму для перевода: ";
        private const string putMoneyToAccount = "Введите сумму которую хотите положить на счёт: ";
        private const string withdrawMoneyFromAccount = "Введите сумму которую хотите снять со счета: ";

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
                else if (!AreHaveCards())
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
                    ShowListAccounts(false);
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
            Account account = ShowListAccounts(true);

            if (account is DepositAccount)
            {
                ConsoleProvider.ErrorOperation();
            }
            else
            {
                (account as CreditAccount).AddCreditToAccount();
            }
        }

        public bool AreHaveCards()
        {
            foreach (Account account in accounts)
            {
                if (account.Cards.Count != 0)
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
            Account account = ShowListAccounts(true);
            int moneyToAccount = ConsoleProvider.EnterMoney(putMoneyToAccount);

            account.PutMoneyToAccount(moneyToAccount);
        }

        public void WithdrawMoneyFromAccount()
        {
            Account account = ShowListAccounts(true);
            int moneyFromAccount = ConsoleProvider.EnterMoney(withdrawMoneyFromAccount);

            if (!account.AreMoneyWithdrawFromAccount(moneyFromAccount))
            {
                ConsoleProvider.ErrorOperation();
            }
        }

        public void RepayDebt(bool isOperation)
        {
            Credit credit;
            Account account;

            while (true)
            {
                account = ShowListAccounts(true);

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
                    credit = (account as CreditAccount).ShowCredits(true);
                    break;
                }
            }

            ConsoleProvider.InputDebt(credit);

            if (isOperation)
            {
                if (account.IsMoneyLessZero())
                {
                    ConsoleProvider.ErrorOperation();
                }
                else
                {
                    if (!(account as CreditAccount).IsDebtRepay(credit))
                    {
                        ConsoleProvider.ErrorOperation();
                    }
                }

                if (credit.IsDeptRepay())
                {
                    (account as CreditAccount).DeleteCreditFromAccount(credit);
                }
            }
        }

        public void PassMonth()
        {
            foreach (Account account in accounts)
            {
                if (account is CreditAccount)
                {
                    foreach (Credit credit in (account as CreditAccount).Credits)
                    {
                        credit.AddDebt();
                    }
                }
                else
                {
                    (account as DepositAccount).AddPercent();
                }
            }
        }

        public void TransferFromAccountToCard()
        {
            ConsoleProvider.ChooseCard(chooseAccountTransferToAccount);
            Account account = ShowListAccounts(true);

            Card card;

            if (account is CreditAccount)
            {
                card = ConsoleProvider.ShowCards((account as CreditAccount).Cards, true);
            }
            else
            {
                card = ConsoleProvider.ShowCards((account as DepositAccount).Cards, true);
            }

            ConsoleProvider.ChooseCard(choosePutToAccountMoney);
            Account transferableAccount = ShowListAccounts(true);

            int transferableMoney = ConsoleProvider.EnterMoney(enterTransferableAmount);

            if (account is CreditAccount)
            {
                if ((account as CreditAccount).IsMonthlyDeptRepay())
                {
                    if ((transferableAccount is DepositAccount) || account.IsMoneyLessZero() || !card.IsCardTransferToCard(account, transferableAccount, transferableMoney))
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
                if (!card.IsCardTransferToCard(account, transferableAccount, transferableMoney))
                {
                    ConsoleProvider.ErrorOperation();
                }
            }
        }

        public void TransferAccountToAccount()
        {
            ConsoleProvider.ChooseCard(chooseAccountTransferToAccount);
            Account account = ShowListAccounts(true);

            string nameAccount;

            Regex nameAccountRegex = new Regex(@"^\w{20}$");

            do
            {
                nameAccount = ConsoleProvider.EnterAccountName();
            }
            while (!nameAccountRegex.IsMatch(nameAccount));

            int transferableMoney = ConsoleProvider.EnterMoney(enterTransferableAmount);

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

        public Account ShowListAccounts(bool isOperation)
        {
            Console.Clear();
            int numberAccount = 1;

            foreach (Account account in accounts)
            {
                if (account is CreditAccount)
                {
                    Console.WriteLine($"{numberAccount}. {account.NameAccount} - {ConsoleProvider.AccountCredit}, на этом счету {account.Money} денег. {ConsoleProvider.CardsOnAccount} - {account.Cards.Count}");
                }
                else
                {
                    Console.WriteLine($"{numberAccount}. {account.NameAccount} - {ConsoleProvider.AccountDeposit}, на этом счету {account.Money} денег. {ConsoleProvider.CardsOnAccount} - {account.Cards.Count}");
                }
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
