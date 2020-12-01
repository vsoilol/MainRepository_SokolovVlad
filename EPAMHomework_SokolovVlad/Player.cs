using System;

namespace PrincessGame
{
    public class Player
    {
        private const char viewPlayer = '@';
        private const int startPositionX = 1;
        private const int startPositionY = 1;
        private const int initialHealth = 10;

        private Point position;
        private Point oldPosition;

        private readonly int borderPlayerXValue;
        private readonly int borderPlayerYValue;
        public int Health { get; private set; }
        public Move WhereToGo { get; set; }
        public Player(int borderX, int borderY)
        {
            position.X = startPositionX;
            position.Y = startPositionY;

            borderPlayerXValue = borderX - 1;
            borderPlayerYValue = borderY - 1;

            Health = initialHealth;

            oldPosition.X = position.X;
            oldPosition.Y = position.Y;
        }
        public bool DepriveHealth(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                return true;
            }
            return false;
        }
        public int GetPositionX()
        {
            return position.X;
        }
        public int GetPositionY()
        {
            return position.Y;
        }
        public void DrawPlayer()
        {
            oldPosition.ClearPoint();

            Console.ForegroundColor = ConsoleColor.Blue;
            position.DrawPoint(viewPlayer);

            Console.ResetColor();
        }
        public void MovePlayer()
        {
            oldPosition.X = position.X;
            oldPosition.Y = position.Y;

            switch (WhereToGo)
            {
                case Move.Up:
                    position.Y--;
                    break;
                case Move.Down:
                    position.Y++;
                    break;
                case Move.Right:
                    position.X++;
                    break;
                case Move.Left:
                    position.X--;
                    break;
            }
            if (!LeaveBorder())
            {
                DrawPlayer();
            }
            else
            {
                position.X = oldPosition.X;
                position.Y = oldPosition.Y;
            }
        }
        public bool LeaveBorder()
        {
            if (position.X == 0 || position.X == borderPlayerXValue || position.Y == 0 || position.Y == borderPlayerYValue)
            {
                return true;
            }
            return false;
        }
        public bool FoundPrincess(Point princess)
        {
            if (position.X == princess.X && position.Y == princess.Y)
            {
                return true;
            }
            return false;
        }
    }
}
