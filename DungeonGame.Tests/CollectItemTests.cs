using DungeonGame.Core;
using Xunit;

namespace DungeonGame.Tests
{
    public class CollectItemTests
    {

        [Fact]
        public void CollectDamageUp()
        {
            //Arrange
            Player testPlayer = new(new Position(), 100, 10, 10, 10);
            Item testItem = new(1, ItemType.Damage, new Position());
            int expectedHp = 10;
            int expectedDamage = 15;
            int expectedCritChance = 10;

            //Act
            testPlayer.Collect(testItem);

            //Assert
            Assert.Equal(expectedHp, testPlayer.Hp);
            Assert.Equal(expectedDamage, testPlayer.Damage);
            Assert.Equal(expectedCritChance, testPlayer.CritChance);
        }

        [Fact]
        public void CollectCritChanceUp()
        {
            //Arrange
            Player testPlayer = new(new Position(), 100, 10, 10, 10);
            Item testItem = new(1, ItemType.Crit, new Position());
            int expectedHp = 10;
            int expectedDamage = 10;
            int expectedCritChance = 15;

            //Act
            testPlayer.Collect(testItem);

            //Assert
            Assert.Equal(expectedHp, testPlayer.Hp);
            Assert.Equal(expectedDamage, testPlayer.Damage);
            Assert.Equal(expectedCritChance, testPlayer.CritChance);
        }

        [Fact]
        public void CollectHeal()
        {
            //Arrange
            Player testPlayer = new(new Position(), 100, 10, 10, 10);
            Item testItem = new(1, ItemType.Heal, new Position());
            int expectedHp = 15;
            int expectedDamage = 10;
            int expectedCritChance = 10;

            //Act
            testPlayer.Collect(testItem);

            //Assert
            Assert.Equal(expectedHp, testPlayer.Hp);
            Assert.Equal(expectedDamage, testPlayer.Damage);
            Assert.Equal(expectedCritChance, testPlayer.CritChance);
        }
    }
}
