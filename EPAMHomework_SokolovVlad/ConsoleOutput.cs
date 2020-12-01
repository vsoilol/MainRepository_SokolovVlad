using System;

namespace PrincessGame
{
    public static class ConsoleOutput
    {
        public static void CreateMenu()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("1 - Начать игру");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("2 - Таблица рекордов");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("3 - Выйти");
            Console.ResetColor();
        }
        public static string EnterRecord()
        {
            Console.WriteLine("Запись рекорда");
            Console.Write("Введите своё имя: ");

            string namePlayer = Console.ReadLine();
            return namePlayer;
        }
        public static void LoseGame()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Game Over");
            Console.ResetColor();
        }
        public static void WinGame()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Вы дошли до принцессы");
            Console.ResetColor();
        }
    }
}
