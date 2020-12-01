using System;

namespace PrincessGame
{
    public class Trap
    {
        private const char viewTrap = '1';

        private Point trapPosition;
        private readonly Random randomValue;
        public int Damage { get; private set; }
        public Trap(int maxHealth, int maxPositionY, int maxPositionX, int positionPlayerX, int positionPlayerY, int positionPrincessX, int positionPrincessY)
        {
            randomValue = new Random();
            Damage = randomValue.Next(1, maxHealth);

            do
            {
                trapPosition.X = randomValue.Next(1, maxPositionX - 1);
                trapPosition.Y = randomValue.Next(1, maxPositionY - 1);
            }
            while ((trapPosition.X == positionPlayerX && trapPosition.Y == positionPlayerY) || (trapPosition.X == positionPrincessX && trapPosition.Y == positionPrincessY));
        }
        public bool FallTrap(int positionX, int positionY)
        {
            if (positionX == trapPosition.X && positionY == trapPosition.Y)
            {
                return true;
            }
            return false;
        }
        public void HideTrap()
        {
            trapPosition.DrawPoint(' ');
        }
        public void ViewTrap()
        {
            trapPosition.DrawPoint(viewTrap);
        }

    }
}
