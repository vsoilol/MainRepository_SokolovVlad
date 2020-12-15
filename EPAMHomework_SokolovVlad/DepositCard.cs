namespace BankGame
{
    public class DepositCard : Card
    {
        public DepositCard() : base()
        {
            ConsoleProvider.ShowNameCard(Name, CardType.Deposit);
        }
    }
}
