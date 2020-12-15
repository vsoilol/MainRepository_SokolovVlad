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
                case (int)OperationNumber.TransferFunds:
                    TransferToCard();
                    break;
                case (int)OperationNumber.WithdrawMoneyFromAccount:
                    WithdrawMoneyFromAccount();
                    break;
                case (int)OperationNumber.TransferMoneyToAccount:
                    TransferToAccount();
                    break;
                case (int)OperationNumber.WatchDebt:
                    RepayDebt(false);
                    break;
                case (int)OperationNumber.RepayDebt:
                    RepayDebt(true);
                    break;
                case (int)OperationNumber.ListAccounts:
                    ConsoleProvider.ShowListAccounts(accounts, false);
                    break;
                case (int)OperationNumber.PassMonth:
                    PassMonth();
                    break;
            }
        }

        public bool AreAccountsHaveCards()
        {
            foreach (Account account in accounts)
            {
                if (account.AnyCardsInAccount())
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
            Account account = ConsoleProvider.ShowListAccounts(accounts, true);
            int moneyToAccount = ConsoleProvider.EnterMoney("Введите сумму которую хотите положить на счёт: ");

            account.PutMoneyToAccount(moneyToAccount);
        }

        public void WithdrawMoneyFromAccount()
        {
            Account account = ConsoleProvider.ShowListAccounts(accounts, true);
            int moneyFromAccount = ConsoleProvider.EnterMoney("Введите сумму которую хотите снять со счета: ");

            if (!account.WithdrawMoneyFromAccount(moneyFromAccount))
            {
                ConsoleProvider.ErrorOperation();
            }
        }

        public void RepayDebt(bool isOperation)
        {
            CreditCard card;
            Account account;

            while (true)
            {
                account = ConsoleProvider.ShowListAccounts(accounts, true);

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
                    card = ConsoleProvider.ShowCards((account as CreditAccount).Cards, true);
                    break;
                }
            }

            ConsoleProvider.InputDebt(card);

            if (isOperation)
            {
                if (account.IsMoneyLessZero())
                {
                    ConsoleProvider.ErrorOperation();
                }
                else
                {
                    if (!(account as CreditAccount).RepayDebt(card))
                    {
                        ConsoleProvider.ErrorOperation();
                    }
                }

                if (card.IsDeptRepay())
                {
                    (account as CreditAccount).DeleteCard(card);
                }
            }
        }

        public void PassMonth()
        {
            foreach (Account account in accounts)
            {
                if (account is CreditAccount)
                {
                    foreach (CreditCard card in (account as CreditAccount).Cards)
                    {
                        card.AddDebt();
                    }
                }
                else
                {
                    (account as DepositAccount).AddPercent();
                }
            }
        }

        public void TransferToCard()
        {
            ConsoleProvider.ChooseCard("Выберете счет и карту с которой будет производится перевод.");
            Account account = ConsoleProvider.ShowListAccounts(accounts, true);

            Card card;

            if (account is CreditAccount)
            {
                card = ConsoleProvider.ShowCards((account as CreditAccount).Cards, true);
            }
            else
            {
                card = ConsoleProvider.ShowCards((account as DepositAccount).Cards, true);
            }

            ConsoleProvider.ChooseCard("Выберете счет на который будет сделан перевод.");
            Account transferableAccount = ConsoleProvider.ShowListAccounts(accounts, true);

            int transferableMoney = ConsoleProvider.EnterMoney("Введите сумму для перевода: ");

            if (account is CreditAccount)
            {
                if ((account as CreditAccount).IsMonthlyDeptRepay())
                {
                    if (!(card as CreditCard).TransferToCard(account, transferableAccount, transferableMoney))
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
                if (!(card as DepositCard).TransferToCard(account, transferableAccount, transferableMoney))
                {
                    ConsoleProvider.ErrorOperation();
                }
            }
        }

        public void TransferToAccount()
        {
            ConsoleProvider.ChooseCard("Выберете счет с которого будет производится перевод.");
            Account account = ConsoleProvider.ShowListAccounts(accounts, true);

            string nameAccount;

            Regex nameAccountRegex = new Regex(@"^\w{20}$");

            do
            {
                nameAccount = ConsoleProvider.EnterAccountName();
            }
            while (!nameAccountRegex.IsMatch(nameAccount));

            int transferableMoney = ConsoleProvider.EnterMoney("Введите сумму для перевода: ");

            if ((account is CreditAccount) && !(account as CreditAccount).IsMonthlyDeptRepay())
            {
                ConsoleProvider.ErrorOperation();
            }
            else
            {
                if (!account.WithdrawMoneyFromAccount(transferableMoney))
                {
                    ConsoleProvider.ErrorOperation();
                }
            }
        }
    }
}
