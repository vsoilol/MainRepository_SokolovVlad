using System.Collections.Generic;

namespace PrincessGame
{
    public class DateTimeComparer : IComparer<HallOfFameEntry>
    {
        public int Compare(HallOfFameEntry firstValue, HallOfFameEntry secondValue)
        {
            if (firstValue.PassageTime > secondValue.PassageTime)
            {
                return 1;
            }
            else if (firstValue.PassageTime < secondValue.PassageTime)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
