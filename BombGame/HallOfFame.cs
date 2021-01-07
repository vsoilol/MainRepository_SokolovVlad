using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BombGame
{
    public static class HallOfFame
    {
        private static readonly string fileName = "HallOfFame.json";
        private const int MaxEntries = 10;

        private static List<HallOfFameEntry> entries = new List<HallOfFameEntry>();
        private static readonly DateTimeComparer passageTime = new DateTimeComparer();

        public static void GetHallOfFame()
        {
            entries.Clear();

            if (File.Exists(fileName))
            {
                string jsonString = File.ReadAllText(fileName);
                entries = JsonSerializer.Deserialize<List<HallOfFameEntry>>(jsonString);
            }
        }

        public static async Task AddResult(HallOfFameEntry entry)
        {
            GetHallOfFame();

            entries.Add(entry);
            entries.Sort(passageTime);

            if (entries.Count > MaxEntries)
            {
                entries.RemoveAt(entries.Count - 1);
            }

            SaveHallOfFameAsync();
        }

        public static async Task SaveHallOfFameAsync()
        {
            using (FileStream write = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(write, entries);
            }
        }

        public static void ShowHallOfFame()
        {
            GetHallOfFame();

            Console.Clear();
            int counter = 1;

            foreach (HallOfFameEntry entry in entries)
            {
                Console.WriteLine($"{counter}) Имя: {entry.Name}\t  Количество попыток: {entry.NumberAttempts}\t  Время: {entry.PassageTime}");
                counter++;
            }
        }

        public static void DeleteHallOfFame()
        {
            File.Delete(fileName);
        }
    }
}
