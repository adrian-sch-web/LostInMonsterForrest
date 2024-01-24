using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DungeonGame.Tests
{
    public class PlaceableTests
    {
        [Fact]
        public void MoveLeft()
        {
            //Arrange
            Placeable testPlaceable = new([10,10]);
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
            Placeable testPlaceable = new([10, 10]);
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
            Placeable testPlaceable = new([10, 10]);
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
            Placeable testPlaceable = new([10, 10]);
            int[] expectedPosition = [10, 11];

            //Act
            testPlaceable.Move(Direction.Down);

            //Assert
            Assert.Equal(expectedPosition, testPlaceable.Position);
        }

        [Fact]
        public void SameSpotCheckSuccess()
        {
            //Arrange
            Placeable testPlaceable = new([3, 6]);

            //Act
            bool actualResult = testPlaceable.OnSameSpot([3, 6]);

            //Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void SameSpotCheckFailFull()
        {
            //Arrange
            Placeable testPlaceable = new([2, 7]);

            //Act
            bool actualResult = testPlaceable.OnSameSpot([3, 6]);

            //Assert
            Assert.False(actualResult);
        }

        [Fact]
        public void SameSpotCheckFailHorizontal()
        {
            //Arrange
            Placeable testPlaceable = new([2, 6]);

            //Act
            bool actualResult = testPlaceable.OnSameSpot([3, 6]);

            //Assert
            Assert.False(actualResult);
        }

        [Fact]
        public void SameSpotCheckFailVertical()
        {
            //Arrange
            Placeable testPlaceable = new([3, 7]);

            //Act
            bool actualResult = testPlaceable.OnSameSpot([3, 6]);

            //Assert
            Assert.False(actualResult);
        }
    }
}
