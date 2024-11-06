
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TheGame
{
    public static class Highscores
    {
        private static string filePath = "Highscore.txt";
        public static Dictionary<string, int> ScoreList = new Dictionary<string, int>();

        public static void LoadScores()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                return;
            }

            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split('฿');
                if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                {
                    var name = parts[0];
                    if (ScoreList.ContainsKey(name))
                    {
                        ScoreList[name] = ScoreList[name] > score ? ScoreList[name] : score;
                    }
                    else
                    {
                        ScoreList.Add(name, score);
                    }
                }
            }
        }
        public static void AddScore(string name, int score)
        {
            if (ScoreList.ContainsKey(name))
            {
                ScoreList[name] = ScoreList[name] > score ? ScoreList[name] : score;
            }
            else
            {
                ScoreList[name] = score;
            }
            SaveScores();
        }

        private static void SaveScores()
        {
            var sortedScores = ScoreList.OrderByDescending(kvp => kvp.Value).Take(5);
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var kvp in sortedScores)
                {
                    writer.WriteLine($"{kvp.Key}฿{kvp.Value}");
                }
            }
        }
    }
}
