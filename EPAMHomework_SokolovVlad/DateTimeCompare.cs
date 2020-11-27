using System.Collections.Generic;

namespace EPAMHomework_SokolovVlad
{
    class DateTimeCompare : IComparer<HallOfFameEntry>
    {
        public int Compare(HallOfFameEntry x, HallOfFameEntry y)
        {
            if (x.Score > y.Score)
                return 1;
            else if (x.Score < y.Score)
                return -1;
            else
                return 0;
        }
    }
}
