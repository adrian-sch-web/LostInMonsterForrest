using DungeonGame.Core;
using Xunit;

namespace DungeonGame.Tests
{
    public class MonsterOverlapTests
    {
        [Fact]
        public void MonsterOverlapTestSuccess()
        {
            //Arrange
            Game game = new();
            game.Monsters.Add(Monster.CreateMonster(1, 0, [1, 1]));

            //Act
            int actualOverlap = game.OverlappingMonster([1, 1]);

            //Assert
            Assert.Equal(1, actualOverlap);
        }

        [Fact]
        public void MonsterOverlapTestFail()
        {
            //Arrange
            Game game = new();
            game.Monsters.Add(Monster.CreateMonster(1, 0, [0, 1]));
            game.Monsters.Add(Monster.CreateMonster(2, 0, [1, 0]));
            game.Monsters.Add(Monster.CreateMonster(3, 0, [0, 0]));

            //Act
            int actualOverlap = game.OverlappingMonster([1, 1]);

            //Assert
            Assert.Equal(-1, actualOverlap);
        }
    }
}
