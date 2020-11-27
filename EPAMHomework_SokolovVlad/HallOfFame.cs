using System;
using System.Collections.Generic;
using System.IO;

namespace EPAMHomework_SokolovVlad
{
    static class HallOfFame
    {
        private static string _file = "HallOfFame.txt";
        private static List<HallOfFameEntry> _entries = new List<HallOfFameEntry>();
        private static DateTimeCompare _dateTimeCompare = new DateTimeCompare();
        private static void GetHallOfFame()
        {
            _entries.Clear();
            if (File.Exists(_file))
            {
                using (StreamReader read = new StreamReader(_file))
                {
                    string line = read.ReadLine();
                    while (line != null)
                    {
                        string[] res = line.Split();
                        HallOfFameEntry entry = new HallOfFameEntry();
                        entry.Name = res[0];
                        entry.Score = DateTime.Parse(res[1]);
                        _entries.Add(entry);
                        line = read.ReadLine();
                    }
                }
            }
        }
        public static void AddResult(HallOfFameEntry entry)
        {
            GetHallOfFame();
            _entries.Add(entry);
            _entries.Sort(_dateTimeCompare);
            if (_entries.Count > 10)
                _entries.RemoveAt(_entries.Count - 1);
            SaveHallOfFame();
        }
        private static void SaveHallOfFame()
        {
            using (StreamWriter write = new StreamWriter(_file, false))
            {
                for (int i = 0; i < _entries.Count; i++)
                    write.WriteLine("{0} {1}", _entries[i].Name, _entries[i].Score.ToLongTimeString());
            }
        }
        public static void Show()
        {
            GetHallOfFame();
            Console.Clear();
            int counter = 1;
            foreach (HallOfFameEntry entry in _entries)
            {
                Console.WriteLine($"{counter} - {entry.Name} - {entry.Score.ToLongTimeString()}");
                counter++;
            }
        }
    }
}
