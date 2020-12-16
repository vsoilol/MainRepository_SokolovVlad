using System.Collections.Generic;

namespace BankGame
{
    public class DepositAccount : Account
    {
        private const int percentDeposit = 5;

        public void AddPercent()
        {
            double monthlyPercent = (Money * percentDeposit) / 100D;
            Money += monthlyPercent;
        }
    }
}
