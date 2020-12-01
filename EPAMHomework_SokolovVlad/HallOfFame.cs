using System;
using System.Collections.Generic;
using System.IO;

namespace PrincessGame
{
    public static class HallOfFame
    {
        private static string fileName = "HallOfFame.txt";
        private const int maxEntries = 10;

        private static List<HallOfFameEntry> entries = new List<HallOfFameEntry>();
        private static DateTimeCompare passageTime = new DateTimeCompare();
        private static void GetHallOfFame()
        {
            entries.Clear();
            if (File.Exists(fileName))
            {
                using (StreamReader read = new StreamReader(fileName))
                {
                    string line = read.ReadLine();

                    while (line != null)
                    {
                        string[] res = line.Split();

                        HallOfFameEntry entry = new HallOfFameEntry
                        {
                            Name = res[0],
                            PassageTime = DateTime.Parse(res[1])
                        };

                        entries.Add(entry);
                        line = read.ReadLine();
                    }
                }
            }
        }
        public static void AddResult(HallOfFameEntry entry)
        {
            GetHallOfFame();

            entries.Add(entry);
            entries.Sort(passageTime);

            if (entries.Count > maxEntries)
            {
                entries.RemoveAt(entries.Count - 1);
            }
            SaveHallOfFame();
        }
        private static void SaveHallOfFame()
        {
            using (StreamWriter write = new StreamWriter(fileName, false))
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    write.WriteLine("{0} {1}", entries[i].Name, entries[i].PassageTime.ToLongTimeString());
                }
            }
        }
        public static void ShowHallOfFame()
        {
            GetHallOfFame();
            Console.Clear();
            int counter = 1;

            foreach (HallOfFameEntry entry in entries)
            {
                Console.WriteLine($"{counter} - {entry.Name} - {entry.PassageTime.ToLongTimeString()}");
                counter++;
            }
        }
    }
}
