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
            game.Monsters.Add(Monster.CreateMonster(1, 0, new Position(1, 1)));

            //Act
            int actualOverlap = game.OverlappingMonster(new Position(1, 1));

            //Assert
            Assert.Equal(1, actualOverlap);
        }

        [Fact]
        public void MonsterOverlapTestFail()
        {
            //Arrange
            Game game = new();
            game.Monsters.Add(Monster.CreateMonster(1, 0, new Position(0, 1)));
            game.Monsters.Add(Monster.CreateMonster(2, 0, new Position(1, 0)));
            game.Monsters.Add(Monster.CreateMonster(3, 0, new Position()));

            //Act
            int actualOverlap = game.OverlappingMonster(new Position(1, 1));

            //Assert
            Assert.Equal(-1, actualOverlap);
        }
    }
}
