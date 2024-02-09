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
            Map map = new();
            map.Monsters.Add(Monster.CreateMonster(1, 0, new Position(1, 1), new Position(0, 0)));

            //Act
            int actualOverlap = map.OnMonster(new Position(1, 1));

            //Assert
            Assert.Equal(1, actualOverlap);
        }

        [Fact]
        public void MonsterOverlapTestFail()
        {
            //Arrange
            Map map = new();
            map.Monsters.Add(Monster.CreateMonster(1, 0, new Position(0, 1), new Position(0, 0)));
            map.Monsters.Add(Monster.CreateMonster(2, 0, new Position(1, 0), new Position(0, 0)));
            map.Monsters.Add(Monster.CreateMonster(3, 0, new Position(), new Position(0, 0)));

            //Act
            int actualOverlap = map.OnMonster(new Position(1, 1));

            //Assert
            Assert.Equal(-1, actualOverlap);
        }
    }
}
