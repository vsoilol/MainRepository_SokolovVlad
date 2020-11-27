using System;

namespace EPAMHomework_SokolovVlad
{
    class Player
    {
        private char _viewPlayer = '@';
        private Сoordinate _position;
        private Сoordinate _oldPosition;
        private int _borderPlayerX;
        private int _borderPlayerY;
        public int Health { get; private set; }
        public Move WhereToGo { get; set; }
        public bool TakeAwayHealth(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                return true;
            return false;
        }
        public int GetPositionX()
        {
            return _position.x;
        }
        public int GetPositionY()
        {
            return _position.y;
        }
        public void DrawPlayer()
        {
            _oldPosition.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            _position.Draw(_viewPlayer);
            Console.ResetColor();
        }
        public void Move()
        {
            _oldPosition.x = _position.x;
            _oldPosition.y = _position.y;
            switch (WhereToGo)
            {
                case EPAMHomework_SokolovVlad.Move.Up:
                    _position.y--;
                    break;
                case EPAMHomework_SokolovVlad.Move.Down:
                    _position.y++;
                    break;
                case EPAMHomework_SokolovVlad.Move.Right:
                    _position.x++;
                    break;
                case EPAMHomework_SokolovVlad.Move.Left:
                    _position.x--;
                    break;
            }
            if(!IsAbroad())
                DrawPlayer();
            else
            {
                _position.x = _oldPosition.x;
                _position.y = _oldPosition.y;
            }
        }
        public bool IsAbroad()
        {
            if (_position.x == 0 || _position.x == _borderPlayerX || _position.y == 0 || _position.y == _borderPlayerY)
                return true;
            return false;
        }
        public bool IsPrincessFound(Сoordinate princess)
        {
            if (_position.x == princess.x && _position.y == princess.y)
                return true;
            return false;
        }
        public Player(int borderX, int borderY)
        {
            _borderPlayerX = borderX-1;
            _borderPlayerY = borderY-1;
            Health = 10;
            _position.x = borderX-2;
            _position.y = 1;
            _oldPosition.x = _position.x;
            _oldPosition.y = _position.y;
        }
    }
}
