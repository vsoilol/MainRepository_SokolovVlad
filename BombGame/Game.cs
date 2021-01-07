using System;
using System.Threading;
using System.Threading.Tasks;

namespace BombGame
{
    public class Game
    {
        private const int OneSecond = 1000;
        private const int GameTime = 3; //Время таймера в минутах

        private const int MinValuePassword = 1;
        private const int MaxValuePassowrd = 10;

        private const int PasswordLength = 4;

        private readonly int[] Password;
        private int[] guessedPassword;

        private int numberAttempts = 0; //количество попыток
        private bool inGame = true;

        private readonly Random randomNumber;
        private Timer mainTimer;

        private DateTime remainingTime;
        private HallOfFameEntry entry;

        public Game()
        {
            randomNumber = new Random();

            Password = new int[PasswordLength];
        }

        private void SetPassword()
        {
            for (int i = 0; i < PasswordLength; i++)
            {
                Password[i] = randomNumber.Next(MinValuePassword, MaxValuePassowrd);
            }
        }

        public async void CheckPasswordAsync()
        {
            for (int i = 0; i < PasswordLength; i++)
            {
                if (guessedPassword[i] != Password[i])
                {
                    numberAttempts++;

                    ConsoleProvider.MistakePassword();
                    ConsoleProvider.ShowAttempts(numberAttempts);

                    await Task.Delay(1000);

                    ConsoleProvider.DeleteLine(3);
                    return;
                }
            }
            WinGame();
        }

        public void StartGame()
        {
            Console.CursorVisible = false;
            Console.Clear();

            ConsoleProvider.ShowAttempts(numberAttempts);

            inGame = true;
            numberAttempts = 0;

            SetPassword();
            StartTimer();
            GuessPassword();
        }

        public void StartTimer()
        {
            remainingTime = new DateTime();
            remainingTime = remainingTime.AddMinutes(GameTime);

            TimerCallback outputTimeCB = new TimerCallback(ShowRemainingTime);

            mainTimer = new Timer(outputTimeCB, null, 0, OneSecond);
        }

        public void ShowRemainingTime(object state)
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

        public void RunMenu()
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

        public void ShowHighscores()
        {
            HallOfFame.ShowHallOfFame();
            Console.ReadKey();
        }

        public void GuessPassword()//Гадание пароля
        {
            guessedPassword = new int[PasswordLength];

            int passwordPosition = ConsoleProvider.DivinePassword.Length;

            while (inGame)
            {
                Console.SetCursorPosition(0, 1);
                Console.Write(ConsoleProvider.DivinePassword);

                for (int i = 0; i < guessedPassword.Length; i++)
                {
                    while (inGame)
                    {
                        if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out guessedPassword[i]))
                        {
                            Console.SetCursorPosition(passwordPosition, 1);
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

        private void WinGame()
        {
            EndTime();
            TimeSpan guessTime = new TimeSpan(0, GameTime, 0) - remainingTime.TimeOfDay;

            ConsoleProvider.ShowResult(numberAttempts, guessTime);

            entry = new HallOfFameEntry() { 
                NumberAttempts = numberAttempts, 
                PassageTime = guessTime.ToString(), 
                Name = ConsoleProvider.LeadName() };

            HallOfFame.AddResult(entry);
        }

        public void EndTime()
        {
            mainTimer.Dispose();
            inGame = false;
        }
    }
}
