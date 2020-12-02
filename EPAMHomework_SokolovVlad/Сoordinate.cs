using System;

namespace PrincessGame
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void DrawPoint(char drawing)
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(drawing);
        }

        public void ClearPoint()
        {
            DrawPoint(' ');
        }
    }
}
