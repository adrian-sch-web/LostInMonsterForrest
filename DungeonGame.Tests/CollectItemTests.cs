using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DungeonGame.Tests
{
    public class CollectItemTests
    {

        [Fact]
        public void CollectDamageUp()
        {
            //Arrange
            Player testPlayer = new([0, 0], 10, 10, 10);
            Item testItem = new([0, 0], "D");
            int expectedHp = 10;
            int expectedDamage = 15;
            int expectedCritChance = 10;

            //Act
            testPlayer.Collect(testItem);

            //Assert
            Assert.Equal(expectedHp,testPlayer.Hp);
            Assert.Equal(expectedDamage, testPlayer.Damage);
            Assert.Equal(expectedCritChance, testPlayer.CritChance);
        }

        [Fact]
        public void CollectCritChanceUp()
        {
            //Arrange
            Player testPlayer = new([0, 0], 10, 10, 10);
            Item testItem = new([0, 0], "C");
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
            Player testPlayer = new([0, 0], 10, 10, 10);
            Item testItem = new([0, 0], "H");
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

        [Fact]
        public void CollectEmptyItem()
        {
            //Arrange
            Player testPlayer = new([0, 0], 10, 10, 10);
            Item testItem = new([0, 0], "");
            int expectedHp = 10;
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
