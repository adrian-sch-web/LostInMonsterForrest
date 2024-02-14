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
            Fighter actualFighter = new(new Position(0, 1), 100, 100, 10, 20);

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
            Fighter testFighter = new(new Position(), 100, 100, 10, 20);
            Attack attack = new(new Monster(1, MonsterType.Normalo, new Position(1, 1), 100, 100, 10, 10, 1, new Position(0, 0)), true);
            int expectedDamage = 10;

            //Act
            Attack actualAttack = testFighter.Attack(50, attack);

            //Assert
            Assert.Equal(expectedDamage, actualAttack.Damage);
            Assert.False(attack.CriticalStrike);

        }

        [Fact]
        public void AttackCritTest()
        {
            //Arrange
            Fighter testFighter = new(new Position(), 100, 100, 10, 20);
            Attack attack = new(new Monster(1, MonsterType.Normalo, new Position(1, 1), 100, 100, 10, 10, 1, new Position(0, 0)), true);
            int expectedDamage = 20;

            //Act
            Attack actualAttack = testFighter.Attack(19, attack);

            //Assert
            Assert.Equal(expectedDamage, actualAttack.Damage);
            Assert.True(attack.CriticalStrike);

        }

        [Fact]
        public void RiskyAttackTestMiss()
        {
            //Arrange
            Fighter testFighter = new(new Position(), 100, 100, 10, 10);
            Attack attack = new(new Monster(1, MonsterType.Normalo, new Position(1, 1), 100, 100, 10, 10, 1, new Position(0, 0)), true);
            int expectedDamage = 0;

            //Act
            Attack actualAttack = testFighter.RiskyAttack(0, 9, attack);

            //Assert
            Assert.Equal(expectedDamage, actualAttack.Damage);
        }

        [Fact]
        public void RiskyAttackTestHit()
        {
            //Arrange
            Fighter testFighter = new(new Position(), 100, 100, 10, 10);
            Attack attack = new(new Monster(1, MonsterType.Normalo, new Position(1, 1), 100, 100, 10, 10, 1, new Position(0, 0)), true);
            int expectedDamage = 10;

            //Act
            Attack actualAttack = testFighter.RiskyAttack(20, 11, attack);

            //Assert
            Assert.Equal(expectedDamage, actualAttack.Damage);
        }

        [Fact]
        public void DefendTestAlive()
        {
            //Arrange
            Fighter testFighter = new(new Position(), 100, 100, 10, 20);
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
            Fighter testFighter = new(new Position(), 100, 10, 10, 20);
            int expectedHp = 0;

            //Act
            testFighter.Defend(20);

            //Assert
            Assert.Equal(expectedHp, testFighter.Hp);
        }

        [Fact]
        public void WeakDefendTestAlive()
        {
            //Arrange
            Fighter testFighter = new(new Position(), 100, 100, 10, 20);
            int expectedHp = 70;

            //Act
            testFighter.WeakDefend(20);

            //Assert
            Assert.Equal(expectedHp, testFighter.Hp);

        }
    }
}