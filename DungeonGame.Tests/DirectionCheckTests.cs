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
            Game game = new();
            
            //Act
            bool actualResult = game.DirectionCheck(Direction.Left, new Position(1, 0));

            //Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void DirectionTestLeftFail() 
        {
            //Arrange
            Game game = new();

            //Act
            bool actualResult = game.DirectionCheck(Direction.Left, new Position(0, 1));

            //Assert
            Assert.False(actualResult);
        }

        [Fact]
        public void DirectionTestRightSuccess()
        {
            //Arrange
            Game game = new();

            //Act
            bool actualResult = game.DirectionCheck(Direction.Right, new Position(game.BoardSize[0] - 2, 10));

            //Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void DirectionTestRightFail()
        {
            //Arrange
            Game game = new();

            //Act
            bool actualResult = game.DirectionCheck(Direction.Right, new Position(game.BoardSize[0] - 1, 10));

            //Assert
            Assert.False(actualResult);
        }

        [Fact]
        public void DirectionTestUpSuccess()
        {
            //Arrange
            Game game = new();

            //Act
            bool actualResult = game.DirectionCheck(Direction.Up, new Position(0, 1));

            //Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void DirectionTestUpFail()
        {
            //Arrange
            Game game = new();

            //Act
            bool actualResult = game.DirectionCheck(Direction.Up, new Position(1, 0));

            //Assert
            Assert.False(actualResult);
        }

        [Fact]
        public void DirectionTestDownSuccess()
        {
            //Arrange
            Game game = new();

            //Act
            bool actualResult = game.DirectionCheck(Direction.Down, new Position(0, game.BoardSize[1] - 2));

            //Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void DirectionTestDownFail()
        {
            //Arrange
            Game game = new();

            //Act
            bool actualResult = game.DirectionCheck(Direction.Down, new Position(0, game.BoardSize[1] - 1));

            Assert.False(actualResult);
        }
    }
}
