using System;
using System.Collections.Generic;

namespace BombGame
{
    public class DateTimeComparer : IComparer<HallOfFameEntry>
    {
        public int Compare(HallOfFameEntry firstValue, HallOfFameEntry secondValue)
        {
            if (TimeSpan.Parse(firstValue.PassageTime) > TimeSpan.Parse(secondValue.PassageTime))
            {
                return 1;
            }
            else if (TimeSpan.Parse(firstValue.PassageTime) < TimeSpan.Parse(secondValue.PassageTime))
            {
                return -1;
            }
            else
            {
                if (firstValue.NumberAttempts > firstValue.NumberAttempts)
                {
                    return 1;
                }
                else if (firstValue.NumberAttempts < firstValue.NumberAttempts)
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
}
