namespace BankGame
{
    public class DepositAccount : Account
    {
        private const int PercentDeposit = 5;

        public void AddPercent()
        {
            double monthlyPercent = (Money * PercentDeposit) / 100D;
            Money += monthlyPercent;
        }
    }
}
