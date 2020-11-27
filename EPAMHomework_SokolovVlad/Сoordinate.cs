
using System;

namespace EPAMHomework_SokolovVlad
{
    struct Сoordinate
    {
        public int x { get; set; }
        public int y { get; set; }
        public void Draw(char drawing)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(drawing);
        }
        public void Clear()
        {
            Draw(' ');
        }
    }
}
