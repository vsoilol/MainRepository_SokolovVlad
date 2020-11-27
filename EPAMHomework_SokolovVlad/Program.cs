using System;

namespace EPAMHomework_SokolovVlad
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(10, 10, 10);
            game.CreateMenu();
        }
    }
}
