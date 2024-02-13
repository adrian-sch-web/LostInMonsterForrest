using DungeonGame.Core;
using Xunit;

namespace DungeonGame.Tests
{
    public class LeaderboardTests : IDisposable
    {
        private string path = "../../../../DungeonGame.Tests/testLeaderboard";
        private string filename = "/testLeaderboard.csv";

        public void Dispose()
        {
            if (File.Exists(path + filename))
            {
                File.Delete(path + filename);
            }
        }


        [Fact]
        public void CreateLeaderboardTest()
        {
            //Arrange
            List<LeaderboardEntry> expectedLeaderboard = [];

            //Act
            var actualLeaderboard = Leaderboard.GetLeaderBoard(path, filename);

            //Assert
            Assert.Equal(expectedLeaderboard, actualLeaderboard);
        }

        [Fact]
        public void SaveRecordTest()
        {
            //Arrange
            LeaderboardEntry expectedLeaderboardEntry = new(1, "test", 1, 1);

            //Act
            Leaderboard.SaveRecord(expectedLeaderboardEntry, path, filename);
            var actualLeaderboardEntry = Leaderboard.GetLeaderBoard(path, filename)[0];

            //Assert
            Assert.Equal(expectedLeaderboardEntry.ID, actualLeaderboardEntry.ID);
            Assert.Equal(expectedLeaderboardEntry.Name, actualLeaderboardEntry.Name);
            Assert.Equal(expectedLeaderboardEntry.Floor, actualLeaderboardEntry.Floor);
            Assert.Equal(expectedLeaderboardEntry.Kills, actualLeaderboardEntry.Kills);
        }
    }
}
