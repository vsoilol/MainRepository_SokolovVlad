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
            accounts.Add(new Account());
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
                    AddCard();
                    break;
                case (int)OperationNumber.PutMoneyCard:
                    PutMoneyCard();
                    break;
                case (int)OperationNumber.TransferFunds:
                    TransferToCard();
                    break;
                case (int)OperationNumber.ListCards:
                    ShowListCards(false);
                    break;
                case (int)OperationNumber.WithdrawMoney:
                    WithdrawMoneyCard();
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
                case (int)OperationNumber.PassMonth:
                    PassMonth();
                    break;
            }
        }

        public Card ShowListCards(bool isOperation)
        {
            int numberAccount = ConsoleProvider.ChooseAccount(accounts);

            return ConsoleProvider.ShowCards(accounts[numberAccount].Cards, accounts[numberAccount].NameAccount, isOperation);
        }

        public bool AreAccountsHaveCards()
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

        public void AddCard()
        {
            int numberAccount = ConsoleProvider.ChooseAccount(accounts);
            accounts[numberAccount].ChooseCard();
        }

        public void PutMoneyCard()
        {
            Card card = ShowListCards(true);
            int moneyCard = ConsoleProvider.EnterMoney("Введите сумму которую хотите положить на карту: ");

            card.PutMoney(moneyCard);
        }

        public void WithdrawMoneyCard()
        {
            Card card = ShowListCards(true);
            int moneyCard = ConsoleProvider.EnterMoney("Введите сумму которую хотите снять с карты: ");

            if (!card.WithdrawMoney(moneyCard))
            {
                ConsoleProvider.ErrorOperation();
            }
        }

        public void RepayDebt(bool isOperation)
        {
            Card card;
            while (true)
            {
                card = ShowListCards(true);

                if (card == null)
                {
                    return;
                }

                if (card is DepositCard)
                {
                    ConsoleProvider.NotCreditCard();
                }
                else
                {
                    break;
                }
            }

            ConsoleProvider.InputDebt(card as CreditCard);

            if (isOperation)
            {
                if ((card as CreditCard).IsMoneyLessZero())
                {
                    ConsoleProvider.ErrorOperation();
                }
                else
                {
                    if (!(card as CreditCard).RepayDebt())
                    {
                        ConsoleProvider.ErrorOperation();
                    }
                }

                if ((card as CreditCard).IsDeptRepay())
                {
                    RemoveCard(card);
                }
            }
        }

        public void RemoveCard(Card card)
        {
            foreach (Account account in accounts)
            {
                if (account.Cards.Contains(card))
                {
                    account.RemoveCard(card);
                }
            }
        }

        public void PassMonth()
        {
            foreach (Account account in accounts)
            {
                foreach (Card card in account.Cards)
                {
                    if (card is CreditCard)
                    {
                        (card as CreditCard).AddDebt();
                    }
                    else
                    {
                        (card as DepositCard).AddPercent();
                    }
                }
            }
        }

        public void TransferToCard()
        {
            ConsoleProvider.ChooseCard("Выберете карту с которой будет производится перевод.");
            Card card = ShowListCards(true);

            ConsoleProvider.ChooseCard("Выберете карту на которую будет сделан перевод.");
            Card transferableCard = ShowListCards(true);

            int transferableMoney = ConsoleProvider.EnterMoney("Введите сумму для перевода: ");

            if (card is CreditCard)
            {
                if (!(card as CreditCard).TransferCard(transferableCard, transferableMoney))
                {
                    ConsoleProvider.ErrorOperation();
                }
            }
            else
            {
                if (!(card as DepositCard).TransferCard(transferableCard, transferableMoney))
                {
                    ConsoleProvider.ErrorOperation();
                }
            }
        }

        public void TransferToAccount()
        {
            ConsoleProvider.ChooseCard("Выберете карту с которой будет производится перевод.");
            Card card = ShowListCards(true);

            string nameAccount;

            Regex nameAccountRegex = new Regex(@"^\w{20}$");

            do
            {
                nameAccount = ConsoleProvider.EnterAccountName();
            }
            while (!nameAccountRegex.IsMatch(nameAccount));

            int transferableMoney = ConsoleProvider.EnterMoney("Введите сумму для перевода: ");

            if ((card is CreditCard) && !(card as CreditCard).IsMonthlyDeptRepay())
            {
                ConsoleProvider.ErrorOperation();
            }
            else
            {
                if (!card.WithdrawMoney(transferableMoney))
                {
                    ConsoleProvider.ErrorOperation();
                }
            }
        }
    }
}
