using System;
using System.Threading;

namespace EPAMHomework_SokolovVlad
{
    class Game
    {
        const char _border = '#';//Как будут выглядить границы
        const char _viewPrincess = '*';//Как будет выглядить принцесса
        private Сoordinate _princess;//Координаты принцессы
        private Player _player;//Игрок
        private int _borderX;
        private int _borderY;
        private bool IsTrapsShow;
        private bool _inGame = true;
        private Trap[] _traps;
        Timer _time;
        DateTime _passageTime;
        private int _numberTraps = 0;
        public Game(int sizeX, int sizeY, int numberTraps)
        {
            Console.CursorVisible = false;
            _borderX = sizeX+2;
            _borderY = sizeY+2;
            _princess.x = 1;
            _princess.y = sizeY;
            _numberTraps = numberTraps;
        }
        public void AddTime(object state)
        {
            Console.SetCursorPosition(0, _borderY + 1);
            Console.ResetColor();
            _passageTime = _passageTime.AddSeconds(1);
            Console.Write($"Прошло {_passageTime.ToLongTimeString()}");
        }
        public void Play()//играть
        {
            _passageTime = new DateTime();
            TimerCallback timeCB = new TimerCallback(AddTime);
            _time = new Timer(timeCB, null, 0, 1000);
            while (_inGame)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D:
                            MovePlayer(Move.Right);
                            break;
                        case ConsoleKey.A:
                            MovePlayer(Move.Left);
                            break;
                        case ConsoleKey.S:
                            MovePlayer(Move.Down);
                            break;
                        case ConsoleKey.W:
                            MovePlayer(Move.Up);
                            break;
                        case ConsoleKey.J:
                            ShowTraps();
                            break;
                    }
                }
            }
        }
        public void CreateTraps()//Создать ловушки
        {
            IsTrapsShow = false;
            _traps = new Trap[_numberTraps];
            for (int i = 0; i < _numberTraps; i++)
                _traps[i] = new Trap(_player.Health, _borderY, _borderX, _player.GetPositionX(), _player.GetPositionY(), _princess.x, _princess.y);
        }
        public void ShowTraps()//Показать или скрыть ловушки
        {
            if (IsTrapsShow)
            {
                for (int i = 0; i < _traps.Length; i++)
                    _traps[i].HideTrap();
                IsTrapsShow = false;
            }
            else
            {
                for (int i = 0; i < _traps.Length; i++)
                    _traps[i].ViewTrap();
                IsTrapsShow = true;
            }
        }
        public void CreateHPMenu()//Счетчик HP
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, _borderY);
            Console.Write(new String(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, _borderY);
            Console.WriteLine($"HP: {_player.Health}");
            Console.ResetColor();
        }
        public void CreateField()//Создать рамку
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 1; i <= _borderY; i++)
            {
                for (int j = 1; j <= _borderX; j++)
                {
                    if (i != 1 && i != _borderY && j != 1 && j != _borderX)
                        Console.Write(' ');
                    else
                        Console.Write(_border);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
        public void CreatePlayer()//Создать игрока
        {
            _player = new Player(_borderX, _borderY);
            _player.DrawPlayer();
        }
        public void CreatePrincess()//Создать принцессу
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            _princess.Draw(_viewPrincess);
            Console.ResetColor();
        }
        public void MovePlayer(Move move)//Передвинуть игрока
        {
            _player.WhereToGo = move;
            _player.Move();
            FoundPrincess();
            EncounteredTrap();
        }
        public void EncounteredTrap()//Встречаются Ловушки
        {
            foreach (Trap trap in _traps)
            {
                if (trap.IsTrap(_player.GetPositionX(), _player.GetPositionY()))
                {
                    if (_player.TakeAwayHealth(trap.Damage))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Game Over");
                        Console.ResetColor();
                        ResetGame();
                    }
                    else
                    {
                        CreateHPMenu();
                        CauseDamage(trap.Damage);
                    }
                    break;
                }
            }
        }
        public void CauseDamage(int damage)//Показывает какой урон получил
        {
            Console.SetCursorPosition(0, _borderY+2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Вы получили урон {damage}");
            Thread.Sleep(500);
            Console.SetCursorPosition(0, _borderY + 2);
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
            Play();
        }
        public void CreateMenu()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("1 - Начать игру");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("2 - Таблица рекордов");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("3 - Выйти");
            Console.ResetColor();
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    StartNewGame();//Начать игру
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    HallOfFame.Show();//Показать рекорды
                    Console.ReadKey();
                    CreateMenu();
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    _inGame = false;
                    return;
                default:
                    Console.Clear();
                    CreateMenu();
                    break;
            }
        }
        public void ResetGame()
        {
            _time.Dispose();
            Console.Write("Нажмите SPACE чтобы войти в меню: ");
            ConsoleKey consoleKey;
            do
            {
                consoleKey = Console.ReadKey(true).Key;
                if (consoleKey == ConsoleKey.Spacebar)
                    CreateMenu();
            }
            while (consoleKey != ConsoleKey.Spacebar);     
        }
        public void WriteRecord()
        {
            _time.Dispose();
            Console.WriteLine("Запись рекорда");
            Console.Write("Введите своё имя: ");
            string name = Console.ReadLine();
            HallOfFameEntry entry = new HallOfFameEntry();
            if (name.Contains(' '))
            {
                string[] res = name.Trim().Split();
                name = res[0];
            }
            entry.Name = name;
            entry.Score = _passageTime;
            HallOfFame.AddResult(entry);
        }
        public void FoundPrincess()
        {
            if (_player.IsPrincessFound(_princess))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Вы дошли до принцессы");
                Console.ResetColor();
                WriteRecord();
                ResetGame();
            }
        }
    }
}
