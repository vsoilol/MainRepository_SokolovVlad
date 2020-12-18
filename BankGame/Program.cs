namespace BankGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Bank bank = new Bank("ТрансБанк");
            bank.StartWork();
        }
    }
}