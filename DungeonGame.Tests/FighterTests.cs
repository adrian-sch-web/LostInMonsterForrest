using DungeonGame.Core;
using Xunit;

namespace DungeonGame.Tests
{
    public class FighterTests
    {
        [Fact]
        public void CreateFighterTest()
        {
            //Arrange
            Position expectedPosition = new Position(0, 1);
            int expectedHp = 100;
            int expectedDamage = 10;
            int expectedCritchance = 20;

            //Act
            Fighter actualFighter = new(new Position(0, 1), 100, 10, 20);

            //Assert
            Assert.Equal(expectedHp, actualFighter.Hp);
            Assert.Equal(expectedDamage, actualFighter.Damage);
            Assert.Equal(expectedCritchance, expectedCritchance);
            Assert.Equal(expectedPosition.X, actualFighter.Position.X);
            Assert.Equal(expectedPosition.Y, actualFighter.Position.Y);

        }

        [Fact]
        public void AttackNoCritTest()
        {
            //Arrange
            Fighter testFighter = new(new Position(),100,10,20);
            int expectedDamage = 10;

            //Act
            int actualDamage = testFighter.Attack(50);

            //Assert
            Assert.Equal(expectedDamage, actualDamage);

        }

        [Fact]
        public void AttackCritTest()
        {
            //Arrange
            Fighter testFighter = new(new Position(), 100, 10, 20);
            int expectedDamage = 20;

            //Act
            int actualDamage = testFighter.Attack(81);

            //Assert
            Assert.Equal(expectedDamage, actualDamage);
        }

        [Fact]
        public void DefendTestAlive()
        {
            //Arrange
            Fighter testFighter = new(new Position(), 100, 10, 20);
            int expectedHp = 80;

            //Act
            testFighter.Defend(20);

            //Assert
            Assert.Equal(expectedHp, testFighter.Hp);   
        }

        [Fact]
        public void DefendTestDead()
        {
            //Arrange
            Fighter testFighter = new(new Position(), 10, 10, 20);
            int expectedHp = 0;

            //Act
            testFighter.Defend(20);

            //Assert
            Assert.Equal(expectedHp, testFighter.Hp);
        }
    }
}