using DungeonGame.Core;
using Xunit;

namespace DungeonGame.Tests
{
    public class ItemTests
    {
        [Fact]
        public void DamageUpItemText()
        {
            //Arrange
            Item testItem = new(1, ItemType.Damage, new Position());
            string expectedText = "Damage Up";

            //Act
            string actualText = testItem.Fullname();

            //Assert
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void CritChanceUpItemText()
        {
            //Arrange
            Item testItem = new(1, ItemType.Crit, new Position());
            string expectedText = "Critical Strike Chance Up";

            //Act
            string actualText = testItem.Fullname();

            //Assert
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void HealItemText()
        {
            //Arrange
            Item testItem = new(1, ItemType.Heal, new Position());
            string expectedText = "Heal";

            //Act
            string actualText = testItem.Fullname();

            //Assert
            Assert.Equal(expectedText, actualText);
        }
    }
}
