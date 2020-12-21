namespace PrincessGame
{
    public class Program
    {
        private const int fieldSizeX = 10;
        private const int fieldSizeY = 10;
        private const int numberTraps = 10;

        private static void Main(string[] args)
        {
            Game game = new Game(fieldSizeX, fieldSizeY, numberTraps);
            game.RunMenu();
        }
    }
}
