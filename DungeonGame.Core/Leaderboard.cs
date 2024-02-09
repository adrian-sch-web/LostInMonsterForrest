using System.IO;

namespace DungeonGame.Core
{
    static public class Leaderboard
    {
        public static List<LeaderboardEntry> GetLeaderBoard()
        {
            List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();
            string content;
            try
            {
                content = File.ReadAllText("../DungeonGame.Core/Leaderboard/Leaderboard.csv");
            }
            catch
            {
                CreateLeaderboard();
                SaveLeaderboard(new List<LeaderboardEntry>());
                return new List<LeaderboardEntry>();
            }
            string[] lines = content.Split("\n");
            for (int i = 1; i < lines.Length - 1; i++)
            {
                if (lines[i] != "")
                {
                    leaderboard.Add(ReadEntry(lines[i]));
                }
            }
            return leaderboard;
        }

        static public void SaveRecord(LeaderboardEntry entry)
        {
            List<LeaderboardEntry> leaderboard = GetLeaderBoard();
            leaderboard.Add(entry);
            leaderboard.Sort((a, b) =>
            {
                if (a.Floor != b.Floor)
                    return b.Floor.CompareTo(a.Floor);
                if (a.Kills != b.Kills)
                    return b.Kills.CompareTo(a.Kills);
                else
                    return a.ID.CompareTo(b.ID);
            });
            if (leaderboard.Count > 10)
            {
                leaderboard.Remove(leaderboard[leaderboard.Count-1]);

            }
            SaveLeaderboard(leaderboard);
        }


        static private void SaveLeaderboard(List<LeaderboardEntry> leaderboard)
        {

            StreamWriter sw = new("../DungeonGame.Core/Leaderboard/Leaderboard.csv");
            sw.WriteLine("id(int),name(string),floor(int),kills(int)");
            foreach (var entry in leaderboard)
            {
                sw.WriteLine(String.Format("{0},{1},{2},{3}", entry.ID, entry.Name, entry.Floor, entry.Kills));
            }
            sw.Flush();
            sw.Close();
        }

        static private void CreateLeaderboard()
        {
            FileStream fs = File.Create("../DungeonGame.Core/Leaderboard/Leaderboard.csv");
            fs.Close();
        }
        static private LeaderboardEntry ReadEntry(string line)
        {
            string[] parts = line.Split(',');
            int id;
            string name;
            int floor;
            int kills;
            id = int.Parse(parts[0]);
            name = parts[1];
            floor = int.Parse(parts[2]);
            kills = int.Parse(parts[3]);

            return new(id, name, floor, kills);
        }
    }

    public class LeaderboardEntry(int id, string name, int floor, int kills)
    {
        public int ID { get; set; } = id;
        public string Name { get; set; } = name;
        public int Floor { get; set; } = floor;
        public int Kills { get; set; } = kills;
    }
}
