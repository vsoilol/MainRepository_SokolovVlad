using System;

namespace EPAMHomework_SokolovVlad
{
    class Trap
    {
        Сoordinate _trapPosition;
        Random _random;
        public int Damage { get; private set; }
        public bool IsTrap(int positionX, int positionY)
        {
            if (positionX == _trapPosition.x && positionY == _trapPosition.y)
                return true;
            return false;
        }
        public Trap(int maxHealth, int maxY, int maxX, int positionPlayerX, int positionPlayerY, int positionPrincessX, int positionPrincessY)
        {
            _random = new Random();
            Damage = _random.Next(1, maxHealth);
            do
            {
                _trapPosition.x = _random.Next(1, maxX - 1);
                _trapPosition.y = _random.Next(1, maxY - 1);
            }
            while ((_trapPosition.x == positionPlayerX && _trapPosition.y == positionPlayerY) || (_trapPosition.x == positionPrincessX && _trapPosition.y == positionPrincessY));
        }
        public void HideTrap()
        {
            _trapPosition.Draw(' ');
        }
        public void ViewTrap()
        {
            _trapPosition.Draw('1');
        }

    }
}
