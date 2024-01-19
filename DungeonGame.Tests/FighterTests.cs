using Xunit;

namespace DungeonGame.Tests
{
    public class FighterTests
    {
        [Fact]
        public void CreateFighterTest()
        {
            //Arrange
            int[] expectedPosition = [0, 1];
            int expectedHp = 100;
            int expectedDamage = 10;
            int expectedCritchance = 20;

            //Act
            Fighter actualFighter = new Fighter([0, 1], 100, 10, 20);

            //Assert
            Assert.Equal(expectedHp, actualFighter.Hp);
            Assert.Equal(expectedDamage, actualFighter.Damage);
            Assert.Equal(expectedCritchance, expectedCritchance);
            Assert.Equal(expectedPosition, actualFighter.Position);
        }

        [Fact]
        public void AttackNoCritTest()
        {
            //Arrange
            Fighter testFighter = new Fighter([0,0],100,10,20);
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
            Fighter testFighter = new Fighter([0, 0], 100, 10, 20);
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
            Fighter testFighter = new Fighter([0, 0], 100, 10, 20);
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
            Fighter testFighter = new Fighter([0, 0], 10, 10, 20);
            int expectedHp = 0;

            //Act
            testFighter.Defend(20);

            //Assert
            Assert.Equal(expectedHp, testFighter.Hp);
        }
    }
}