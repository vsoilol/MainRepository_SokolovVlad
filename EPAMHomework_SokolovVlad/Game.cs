using System;
using System.Threading;

namespace PrincessGame
{
    public class Game
    {
        private const char viewBorder = '#';//Как будут выглядить границы
        private const char viewPrincess = '*';//Как будет выглядить принцесса

        private const int timeWait = 500;

        private const int inscriptionDamagePositionX = 0;
        private readonly int inscriptionDamagePositionY;

        private const int inscriptionHPMenuPositionX = 0;
        private readonly int inscriptionHPMenuPositionY;

        private readonly int numberTraps = 0;

        private Point princess;//Координаты принцессы
        private Player player;//Игрок

        private readonly int borderSizeX;
        private readonly int borderSizeY;

        private bool areTrapsShow;
        private bool inGame = true;

        private Trap[] traps;

        private Timer timerSecond;
        private DateTime passageTime;
        public Game(int fieldSizeX, int fieldSizeY, int numberTraps)
        {
            Console.CursorVisible = false;

            borderSizeX = fieldSizeX + 2;
            borderSizeY = fieldSizeY + 2;

            princess.X = fieldSizeX;
            princess.Y = fieldSizeY;

            this.numberTraps = numberTraps;

            inscriptionDamagePositionY = borderSizeY + 2;
            inscriptionHPMenuPositionY = borderSizeY;
        }
        public void AddTime(object state)
        {
            Console.SetCursorPosition(0, borderSizeY + 1);
            Console.ResetColor();
            passageTime = passageTime.AddSeconds(1);
            Console.Write($"Прошло {passageTime.ToLongTimeString()}");
        }
        public void PlayGame()//играть
        {
            passageTime = new DateTime();
            TimerCallback timeCB = new TimerCallback(AddTime);
            timerSecond = new Timer(timeCB, null, 0, 1000);

            while (inGame)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case (ConsoleKey)Button.Right:
                            MovePlayer(Move.Right);
                            break;
                        case (ConsoleKey)Button.Left:
                            MovePlayer(Move.Left);
                            break;
                        case (ConsoleKey)Button.Down:
                            MovePlayer(Move.Down);
                            break;
                        case (ConsoleKey)Button.Up:
                            MovePlayer(Move.Up);
                            break;
                        case (ConsoleKey)Button.VisibilityTraps:
                            ShowTraps();
                            break;
                    }
                }
            }
        }
        public void CreateTraps()//Создать ловушки
        {
            areTrapsShow = false;
            traps = new Trap[numberTraps];

            for (int i = 0; i < numberTraps; i++)
            {
                traps[i] = new Trap(player.Health, borderSizeY, borderSizeX, player.GetPositionX(), player.GetPositionY(), princess.X, princess.Y);
            }
        }
        public void ShowTraps()//Показать или скрыть ловушки
        {
            if (areTrapsShow)
            {
                for (int i = 0; i < traps.Length; i++)
                {
                    traps[i].HideTrap();
                }
                areTrapsShow = false;
            }
            else
            {
                for (int i = 0; i < traps.Length; i++)
                {
                    traps[i].ViewTrap();
                }
                areTrapsShow = true;
            }
        }
        public void CreateHPMenu()//Счетчик HP
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(inscriptionHPMenuPositionX, inscriptionHPMenuPositionY);

            Console.Write(new String(' ', Console.BufferWidth));

            Console.SetCursorPosition(inscriptionHPMenuPositionX, inscriptionHPMenuPositionY);
            Console.WriteLine($"HP: {player.Health}");
            Console.ResetColor();
        }
        public void CreateField()//Создать рамку
        {
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 1; i <= borderSizeY; i++)
            {
                for (int j = 1; j <= borderSizeX; j++)
                {
                    if (i != 1 && i != borderSizeY && j != 1 && j != borderSizeX)
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.Write(viewBorder);
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
        public void CreatePlayer()//Создать игрока
        {
            player = new Player(borderSizeX, borderSizeY);
            player.DrawPlayer();
        }
        public void CreatePrincess()//Создать принцессу
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            princess.DrawPoint(viewPrincess);
            Console.ResetColor();
        }
        public void MovePlayer(Move move)//Передвинуть игрока
        {
            player.WhereToGo = move;
            player.MovePlayer();

            FoundPrincess();
            EncounteredTrap();
        }
        public void EncounteredTrap()//Встречаются Ловушки
        {
            foreach (Trap trap in traps)
            {
                if (trap.FallTrap(player.GetPositionX(), player.GetPositionY()))
                {
                    if (player.DepriveHealth(trap.Damage))
                    {
                        ConsoleOutput.LoseGame();
                        ResetGame();
                    }
                    else
                    {
                        CreateHPMenu();
                        ShowCausedDamage(trap.Damage);
                    }
                    break;
                }
            }
        }
        public void ShowCausedDamage(int damage)//Показывает какой урон получил
        {
            Console.SetCursorPosition(inscriptionDamagePositionX, inscriptionDamagePositionY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Вы получили урон {damage}");

            Thread.Sleep(timeWait);

            Console.SetCursorPosition(inscriptionDamagePositionX, inscriptionDamagePositionY);
            Console.Write(new String(' ', Console.BufferWidth));
            Console.ResetColor();

        }
        public void StartNewGame()//Начать новую игру
        {
            CreateField();
            CreatePlayer();
            CreatePrincess();
            CreateHPMenu();
            CreateTraps();
            PlayGame();
        }
        public void RunMenu()
        {
            ConsoleOutput.CreateMenu();

            switch (Console.ReadKey().Key)
            {
                case (ConsoleKey)Button.StartGame:
                    Console.Clear();
                    StartNewGame();//Начать игру
                    break;
                case (ConsoleKey)Button.Records:
                    Console.Clear();
                    HallOfFame.ShowHallOfFame();//Показать рекорды
                    Console.ReadKey();
                    RunMenu();
                    break;
                case (ConsoleKey)Button.Exit:
                    Console.Clear();
                    inGame = false;
                    return;
                default:
                    Console.Clear();
                    RunMenu();
                    break;
            }
        }
        public void ResetGame()
        {
            timerSecond.Dispose();
            Console.Write("Нажмите SPACE чтобы войти в меню: ");
            ConsoleKey consoleKey;

            do
            {
                consoleKey = Console.ReadKey(true).Key;
                if (consoleKey == (ConsoleKey)Button.ResetGame)
                {
                    RunMenu();
                }
            }
            while (consoleKey != (ConsoleKey)Button.ResetGame);
        }
        public void WriteRecord()
        {
            timerSecond.Dispose();
            string namePlayer = ConsoleOutput.EnterRecord();

            HallOfFameEntry entry = new HallOfFameEntry();

            if (namePlayer.Contains(' '))
            {
                string[] res = namePlayer.Trim().Split();
                namePlayer = res[0];
            }
            entry.Name = namePlayer;
            entry.PassageTime = passageTime;

            HallOfFame.AddResult(entry);
        }
        public void FoundPrincess()
        {
            if (player.FoundPrincess(princess))
            {
                ConsoleOutput.WinGame();
                WriteRecord();
                ResetGame();
            }
        }
    }
}
