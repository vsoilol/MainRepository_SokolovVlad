using System;
using System.Threading;
using System.Threading.Tasks;

namespace BombGame
{
    public static class Game
    {
        private const int OneSecond = 1000;
        private static int gameTime;
        private const int MinValuePassword = 1;
        private const int MaxValuePassowrd = 10;

        private const int PasswordLength = 4;
        private const int GuessPasswordPosition = 1;

        private static int[] Password;
        private static int[] guessedPassword;

        private static int numberAttempts = 0;
        private static int maxNumberAttempts;
        private static bool inGame = true;

        private static Random randomNumber;
        private static Timer mainTimer;

        private static DateTime remainingTime;
        private static HallOfFameEntry entry;

        private static void SetPassword()
        {
            randomNumber = new Random();
            Password = new int[PasswordLength];

            for (int i = 0; i < PasswordLength; i++)
            {
                Password[i] = randomNumber.Next(MinValuePassword, MaxValuePassowrd);
            }
        }

        public static async void CheckPasswordAsync()
        {
            for (int i = 0; i < PasswordLength; i++)
            {
                if (guessedPassword[i] != Password[i])
                {
                    numberAttempts++;

                    if(numberAttempts >= maxNumberAttempts)
                    {
                        EndTime();
                        ConsoleProvider.LostGame();
                        return;
                    }

                    ConsoleProvider.MistakePassword();
                    ConsoleProvider.ShowAttempts(numberAttempts);

                    await Task.Delay(1000);

                    ConsoleProvider.DeleteLine(3);
                    return;
                }
            }
            WinGame();
        }

        public static void StartGame()
        {
            Console.CursorVisible = false;
            Console.Clear();

            ConsoleProvider.ChooseData(ref gameTime, ref maxNumberAttempts);

            inGame = true;
            numberAttempts = 0;

            ConsoleProvider.ShowAttempts(numberAttempts);

            SetPassword();
            StartTimer();
            GuessPassword();
        }

        public static void StartTimer()
        {
            remainingTime = new DateTime();
            remainingTime = remainingTime.AddMinutes(gameTime);

            TimerCallback outputTimeCB = new TimerCallback(ShowRemainingTime);

            mainTimer = new Timer(outputTimeCB, null, 0, OneSecond);
        }

        public static void ShowRemainingTime(object state)
        {
            if (remainingTime <= DateTime.MinValue)
            {
                EndTime();
                ConsoleProvider.LostGame();
            }
            else
            {
                remainingTime -= new TimeSpan(0, 0, 1);

                ConsoleProvider.ShowTime(remainingTime);
            }
        }

        public static void RunMenu()
        {
            while (true)
            {
                Console.Clear();
                int menuNumber = ConsoleProvider.CreateMenu();

                switch (menuNumber)
                {
                    case (int)Menu.StartGame:
                        StartGame();
                        break;
                    case (int)Menu.ShowHighscores:
                        ShowHighscores();
                        break;
                    case (int)Menu.ExitGame:
                        return;
                    case (int)Menu.DeleteHallOfFame:
                        HallOfFame.DeleteHallOfFame();
                        break;
                }
            }
        }

        public static void ShowHighscores()
        {
            HallOfFame.ShowHallOfFame();
            Console.ReadKey();
        }

        public static void GuessPassword()//Гадание пароля
        {
            guessedPassword = new int[PasswordLength];

            int passwordPosition = ConsoleProvider.DivinePassword.Length;

            while (inGame)
            {
                Console.SetCursorPosition(0, GuessPasswordPosition);
                Console.Write(ConsoleProvider.DivinePassword);

                EnterPassword(passwordPosition);
                ConfirmPassword(ref passwordPosition);
            }
        }

        private static void WinGame()
        {
            EndTime();
            TimeSpan guessTime = new TimeSpan(0, gameTime, 0) - remainingTime.TimeOfDay;

            ConsoleProvider.ShowResult(numberAttempts, guessTime);

            entry = new HallOfFameEntry() { 
                NumberAttempts = numberAttempts, 
                PassageTime = guessTime.ToString(), 
                Name = ConsoleProvider.LeadName() };

            HallOfFame.AddResult(entry);
        }

        public static void EndTime()
        {
            mainTimer.Dispose();
            inGame = false;
        }

        private static void EnterPassword(int passwordPosition)
        {
            for (int i = 0; i < guessedPassword.Length; i++)
            {
                while (inGame)
                {
                    if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out guessedPassword[i]))
                    {
                        Console.SetCursorPosition(passwordPosition, GuessPasswordPosition);
                        Console.Write(guessedPassword[i]);
                        passwordPosition++;
                        break;
                    }
                    else
                    {
                        ConsoleProvider.ShowPassword(Password);
                    }
                }
            }
        }

        private static void ConfirmPassword(ref int passwordPosition)
        {
            while (inGame)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    ConsoleProvider.DeleteLine(1, ConsoleProvider.DivinePassword.Length);
                    passwordPosition = ConsoleProvider.DivinePassword.Length;

                    CheckPasswordAsync();
                    break;
                }
            }
        }
    }
}
