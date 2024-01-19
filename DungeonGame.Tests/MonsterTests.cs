using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            int expectedType = 0;

            //Act
            Monster testMonster = Monster.CreateMonster(1, 0, [0, 0]);

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
            int expectedType = 1;

            //Act
            Monster testMonster = Monster.CreateMonster(1, 1, [0, 0]);

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
            int expectedType = 2;

            //Act
            Monster testMonster = Monster.CreateMonster(1, 2, [0, 0]);

            //Assert
            Assert.Equal(expectedHp, testMonster.Hp);
            Assert.Equal(expectedDamage, testMonster.Damage);
            Assert.Equal(expectedCritChance, testMonster.CritChance);
            Assert.Equal(expectedType, testMonster.Type);
        }
    }
}
