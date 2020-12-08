namespace BankGame
{
    public class DepositCard : Card
    {
        private const int percentDeposit = 5;

        public DepositCard()
        {
            Money = ConsoleProvider.SetMoney(CardType.Deposit);
        }

        public void AddPercent()
        {
            double monthlyPercent = (Money * percentDeposit) / 100D;
            Money += monthlyPercent;
        }
    }
}
