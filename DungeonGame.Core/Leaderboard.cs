namespace DungeonGame.Core
{
    public class Leaderboard
    {
        private const string path = "../DungeonGame.Core/Leaderboard";
        private const string filename = "/Leaderboard.csv";
        public static List<LeaderboardEntry> GetLeaderBoard(string path = path, string filename = filename)
        {
            List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();
            string content;
            try
            {
                content = File.ReadAllText(path + filename);
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
            catch
            {
                CreateLeaderboard(path, filename);
                SaveLeaderboard(new List<LeaderboardEntry>(), path, filename);
                return new List<LeaderboardEntry>();
            }
        }

        static public void SaveRecord(LeaderboardEntry entry, string path = path, string filename = filename)
        {
            List<LeaderboardEntry> leaderboard = GetLeaderBoard(path, filename);
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
                leaderboard.Remove(leaderboard[leaderboard.Count - 1]);

            }
            SaveLeaderboard(leaderboard, path, filename);
        }


        static private void SaveLeaderboard(List<LeaderboardEntry> leaderboard, string path = path, string filename = filename)
        {
            StreamWriter sw = new(path + filename);
            sw.WriteLine("id(int),name(string),floor(int),kills(int)");
            foreach (var entry in leaderboard)
            {
                sw.WriteLine(String.Format("{0},{1},{2},{3}", entry.ID, entry.Name, entry.Floor, entry.Kills));
            }
            sw.Flush();
            sw.Close();
        }

        static private void CreateLeaderboard(string path = path, string filename = filename)
        {
            FileStream fs = File.Create(path + filename);
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
