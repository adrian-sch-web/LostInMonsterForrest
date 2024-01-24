using DungeonGame.Core;
using Xunit;

namespace DungeonGame.Tests
{
    public class MonsterTests
    {
        [Fact]
        public void AverageMonster()
        {
            //Arrange
            int expectedHp = 50;
            int expectedDamage = 4;
            int expectedCritChance = 10;
            MonsterType expectedType = MonsterType.Normalo;

            //Act
            Monster testMonster = Monster.CreateMonster(1, MonsterType.Normalo, new Position());

            //Assert
            Assert.Equal(expectedHp, testMonster.Hp);
            Assert.Equal(expectedDamage, testMonster.Damage);
            Assert.Equal(expectedCritChance, testMonster.CritChance);
            Assert.Equal(expectedType, testMonster.Type);
        }

        [Fact]
        public void HighHealthMonster()
        {
            //Arrange
            int expectedHp = 100;
            int expectedDamage = 2;
            int expectedCritChance = 20;
            MonsterType expectedType = MonsterType.Giganto;

            //Act
            Monster testMonster = Monster.CreateMonster(1, MonsterType.Giganto, new Position());

            //Assert
            Assert.Equal(expectedHp, testMonster.Hp);
            Assert.Equal(expectedDamage, testMonster.Damage);
            Assert.Equal(expectedCritChance, testMonster.CritChance);
            Assert.Equal(expectedType, testMonster.Type);
        }

        [Fact]
        public void HighDamageMonster()
        {
            //Arrange
            int expectedHp = 30;
            int expectedDamage = 10;
            int expectedCritChance = 40;
            MonsterType expectedType = MonsterType.Attacko;

            //Act
            Monster testMonster = Monster.CreateMonster(1, MonsterType.Attacko, new Position());

            //Assert
            Assert.Equal(expectedHp, testMonster.Hp);
            Assert.Equal(expectedDamage, testMonster.Damage);
            Assert.Equal(expectedCritChance, testMonster.CritChance);
            Assert.Equal(expectedType, testMonster.Type);
        }
    }
}
