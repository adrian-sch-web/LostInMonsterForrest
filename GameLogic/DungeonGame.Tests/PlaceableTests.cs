﻿using DungeonGame.Core;
using Xunit;

namespace DungeonGame.Tests
{
    public class PlaceableTests
    {
        [Fact]
        public void MoveLeft()
        {
            //Arrange
            Placeable testPlaceable = new(new Position(10, 10));
            Position expectedPosition = new(9, 10);

            //Act
            testPlaceable.Move(Direction.Left);

            //Assert
            Assert.Equal(expectedPosition.X, testPlaceable.Position.X);
            Assert.Equal(expectedPosition.Y, testPlaceable.Position.Y);


        }

        [Fact]
        public void MoveRight()
        {
            //Arrange
            Placeable testPlaceable = new(new Position(10, 10));
            Position expectedPosition = new(11, 10);

            //Act
            testPlaceable.Move(Direction.Right);

            //Assert
            Assert.Equal(expectedPosition.X, testPlaceable.Position.X);
            Assert.Equal(expectedPosition.Y, testPlaceable.Position.Y);
        }

        [Fact]
        public void MoveUp()
        {
            //Arrange
            Placeable testPlaceable = new(new Position(10, 10));
            Position expectedPosition = new(10, 9);

            //Act
            testPlaceable.Move(Direction.Up);

            //Assert
            Assert.Equal(expectedPosition.X, testPlaceable.Position.X);
            Assert.Equal(expectedPosition.Y, testPlaceable.Position.Y);
        }

        [Fact]
        public void MoveDown()
        {
            //Arrange
            Placeable testPlaceable = new(new Position(10, 10));
            Position expectedPosition = new Position(10, 11);

            //Act
            testPlaceable.Move(Direction.Down);

            //Assert
            Assert.Equal(expectedPosition.X, testPlaceable.Position.X);
            Assert.Equal(expectedPosition.Y, testPlaceable.Position.Y);
        }

        [Fact]
        public void SameSpotCheckSuccess()
        {
            //Arrange
            Placeable testPlaceable = new(new Position(3, 6));

            //Act
            bool actualResult = testPlaceable.OnSameSpot(new Position(3, 6));

            //Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void SameSpotCheckFailFull()
        {
            //Arrange
            Placeable testPlaceable = new(new Position(2, 7));

            //Act
            bool actualResult = testPlaceable.OnSameSpot(new Position(3, 6));

            //Assert
            Assert.False(actualResult);
        }

        [Fact]
        public void SameSpotCheckFailHorizontal()
        {
            //Arrange
            Placeable testPlaceable = new(new Position(2, 6));

            //Act
            bool actualResult = testPlaceable.OnSameSpot(new Position(3, 6));

            //Assert
            Assert.False(actualResult);
        }

        [Fact]
        public void SameSpotCheckFailVertical()
        {
            //Arrange
            Placeable testPlaceable = new(new Position(3, 7));

            //Act
            bool actualResult = testPlaceable.OnSameSpot(new Position(3, 6));

            //Assert
            Assert.False(actualResult);
        }

        [Fact]
        public void DistanceCheckSameSpot()
        {
            //Arrange
            Placeable testPlaceable = new(new Position(1,1));
            int expectedDistance = 0;

            //Act
            int actualDistance = testPlaceable.Distance(new Position(1, 1));

            //Assert
            Assert.Equal(expectedDistance, actualDistance);
        }

        [Fact]
        public void DistanceCheckHorizontal()
        {
            //Arrange
            Placeable testPlaceable = new(new Position());
            int expectedDistance = 2;

            //Act
            int actualDistance = testPlaceable.Distance(new Position(2, 0));

            //Assert
            Assert.Equal(expectedDistance, actualDistance);
        }

        [Fact]
        public void DistanceCheckVertical()
        {
            //Arrange
            Placeable testPlaceable = new(new Position(0,2));
            int expectedDistance = 2;

            //Act
            int actualDistance = testPlaceable.Distance(new Position());

            //Assert
            Assert.Equal(expectedDistance, actualDistance);
        }

        [Fact]
        public void DistanceCheckDiagonal()
        {
            //Arrange
            Placeable testPlaceable = new(new Position());
            int expectedDistance = 4;

            //Act
            int actualDistance = testPlaceable.Distance(new Position(2, 2));

            //Assert
            Assert.Equal(expectedDistance, actualDistance);
        }
    }
}
