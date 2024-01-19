using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DungeonGame.Tests
{
    public class ItemTests
    {
        [Fact]
        public void DamageUpItemText()
        {
            //Arrange
            Item testItem = new Item([0, 0], "D");
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
            Item testItem = new Item([0, 0], "C");
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
            Item testItem = new Item([0, 0], "H");
            string expectedText = "Heal";

            //Act
            string actualText = testItem.Fullname();

            //Assert
            Assert.Equal(expectedText, actualText);
        }
        
        [Fact]
        public void ItemGoneText()
        {
            //Arrange
            Item testItem = new Item([0, 0], "");
            string expectedText = "";

            //Act
            string actualText = testItem.Fullname();

            //Assert
            Assert.Equal(expectedText, actualText);
        }
    }
}
