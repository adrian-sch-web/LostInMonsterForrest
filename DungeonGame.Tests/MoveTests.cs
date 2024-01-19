using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DungeonGame.Tests
{
    public class MoveTests
    {
        [Fact]
        public void MoveLeft()
        {
            //Arrange
            Placeable testPlaceable = new Placeable([10,10]);
            int[] expectedPosition = [9, 10];

            //Act
            testPlaceable.Move(Direction.Left);

            //Assert
            Assert.Equal(expectedPosition, testPlaceable.Position);
        }

        [Fact]
        public void MoveRight()
        {
            //Arrange
            Placeable testPlaceable = new Placeable([10, 10]);
            int[] expectedPosition = [11, 10];

            //Act
            testPlaceable.Move(Direction.Right);

            //Assert
            Assert.Equal(expectedPosition, testPlaceable.Position);
        }

        [Fact]
        public void MoveUp()
        {
            //Arrange
            Placeable testPlaceable = new Placeable([10, 10]);
            int[] expectedPosition = [10, 9];

            //Act
            testPlaceable.Move(Direction.Up);

            //Assert
            Assert.Equal(expectedPosition, testPlaceable.Position);
        }

        [Fact]
        public void MoveDown()
        {
            //Arrange
            Placeable testPlaceable = new Placeable([10, 10]);
            int[] expectedPosition = [10, 11];

            //Act
            testPlaceable.Move(Direction.Down);

            //Assert
            Assert.Equal(expectedPosition, testPlaceable.Position);
        }

    }
}
