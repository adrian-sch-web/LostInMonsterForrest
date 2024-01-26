using DungeonGame.Core;
using Xunit;

namespace DungeonGame.Tests
{
    public class DirectionCheckTests
    {
        [Fact]
        public void DirectionTestLeftSuccess()
        {
            //Arrange
            Map map = new();
            
            //Act
            bool actualResult = map.DirectionCheck(Direction.Left, new Position(1, 0));

            //Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void DirectionTestLeftFail() 
        {
            //Arrange
            Map map = new();

            //Act
            bool actualResult = map.DirectionCheck(Direction.Left, new Position(0, 1));

            //Assert
            Assert.False(actualResult);
        }

        [Fact]
        public void DirectionTestRightSuccess()
        {
            //Arrange
            Map map = new();

            //Act
            bool actualResult = map.DirectionCheck(Direction.Right, new Position(map.Size.X - 2, 10));

            //Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void DirectionTestRightFail()
        {
            //Arrange
            Map map = new();

            //Act
            bool actualResult = map.DirectionCheck(Direction.Right, new Position(map.Size.X - 1, 10));

            //Assert
            Assert.False(actualResult);
        }

        [Fact]
        public void DirectionTestUpSuccess()
        {
            //Arrange
            Map map = new();

            //Act
            bool actualResult = map.DirectionCheck(Direction.Up, new Position(0, 1));

            //Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void DirectionTestUpFail()
        {
            //Arrange
            Map map = new();

            //Act
            bool actualResult = map.DirectionCheck(Direction.Up, new Position(1, 0));

            //Assert
            Assert.False(actualResult);
        }

        [Fact]
        public void DirectionTestDownSuccess()
        {
            //Arrange
            Map map = new();

            //Act
            bool actualResult = map.DirectionCheck(Direction.Down, new Position(0, map.Size.Y - 2));

            //Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void DirectionTestDownFail()
        {
            //Arrange
            Map map = new();

            //Act
            bool actualResult = map.DirectionCheck(Direction.Down, new Position(0, map.Size.Y - 1));

            Assert.False(actualResult);
        }
    }
}
