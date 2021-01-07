using System;

namespace BombGame
{
    public static class ConsoleProvider
    {
        private const string NotNumber = "Не является числом.";
        private const string InvalidInput = "Неверный ввод.";

        private const string TryAgain = "Повторите снова.";

        private const string StartGame = "Начать игру";
        private const string ShowHighscores = "Показать рекорды";
        private const string ExitGame = "Выйти из игры";
        private const string DeleteHallOfFame = "Очистить список рекордов";

        public const string DivinePassword = "Угадайте 4-х значное число: ";

        private const int FirstNumberMenu = 1;
        private const int LastNumberMenu = 4;

        private const int TimePosition = 0;

        private const int PasswordPosition = 3;
        private const int MistakePasswordPosition = 3;
        private const int AttemptsPosition = 2;

        public static int CreateMenu()
        {
            int result;

            Console.WriteLine($"{(int)Menu.StartGame}. {StartGame}");
            Console.WriteLine($"{(int)Menu.ShowHighscores}. {ShowHighscores}");
            Console.WriteLine($"{(int)Menu.ExitGame}. {ExitGame}");
            Console.WriteLine($"{(int)Menu.DeleteHallOfFame}. {DeleteHallOfFame}");

            result = GetMenuNumber();

            return result;
        }

        public static int GetMenuNumber()
        {
            int resultNumber;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out resultNumber))
                {
                    if (resultNumber <= LastNumberMenu && resultNumber >= FirstNumberMenu)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(InvalidInput);
                        Console.WriteLine(TryAgain);
                    }
                }
                else
                {
                    Console.WriteLine(NotNumber);
                    Console.WriteLine(TryAgain);
                }
            }

            return resultNumber;
        }

        public static void ShowTime(DateTime time)
        {
            Console.ResetColor();
            Console.SetCursorPosition(0, TimePosition);
            Console.Write($"Осталось {time.ToLongTimeString()}");
        }

        public static void ShowAttempts(int attempts)
        {
            Console.ResetColor();
            Console.SetCursorPosition(0, AttemptsPosition);
            Console.WriteLine($"Количество неверных вариантов {attempts}");
        }

        public static void ShowPassword(int[] password)
        {
            Console.SetCursorPosition(0, PasswordPosition);
            foreach (int item in password)
            {
                Console.Write(item);
            }
        }

        public static void MistakePassword()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, MistakePasswordPosition);
            Console.WriteLine("Неправильный пароль");
        }

        public static void DeleteLine(int lineTopPosition, int lineLeftPosition = 0)
        {
            Console.SetCursorPosition(lineLeftPosition, lineTopPosition);
            Console.ResetColor();
            Console.Write(new String(' ', Console.BufferWidth));
        }

        public static void ShowResult(int numberAttempts, TimeSpan guessTime)
        {
            Console.Clear();
            Console.WriteLine($"Пароль угадан за {numberAttempts} попыток, {guessTime}");
        }

        public static string LeadName()
        {
            Console.Write("Введите имя: ");
            return Console.ReadLine();
        }

        public static void LostGame()
        {
            Console.Clear();
            Console.WriteLine("Ты проиграл");
            Console.ReadKey();
        }
    }
}
