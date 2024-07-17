using NUnit.Framework;
using System;
using ValantDemoApi.Domain;
using ValantDemoApi.Helpers;

namespace ValantDemoApi.Tests.Domain
{
    [TestFixture]
    public class MazeTests
    {
        [Test]
        public void MazeInitialization_ShouldHaveBetween3And10Size()
        {
            var maze = Maze.CreateRandomMaze();

            Assert.IsNotNull(maze);
            Assert.GreaterOrEqual(maze.Path.Count, MazeHelper.MazeMinSize);
            Assert.LessOrEqual(maze.Path.Count, MazeHelper.MazeMaxSize);

            var totalRows = maze.Path.Count;

            foreach (var row in maze.Path)
            {
                Assert.IsTrue(row.Count == totalRows);
            }
        }

        [Test]
        public void MazeInitialization_ShouldSetStartPosition()
        {
            var maze = Maze.CreateRandomMaze();

            Assert.AreEqual(MazeStatusEnum.Start.ToInitial(), maze.Path[0][0]);
        }

        [Test]
        public void MazeInitialization_ShouldHaveOneEndPosition()
        {
            var maze = Maze.CreateRandomMaze();
            int endCount = 0;

            for (int i = 0; i < maze.Path.Count; i++)
            {
                for (int j = 0; j < maze.Path[i].Count; j++)
                {
                    if (maze.Path[i][j] == MazeStatusEnum.End.ToInitial())
                    {
                        endCount++;
                    }
                }
            }

            Assert.AreEqual(1, endCount);
        }

        [Test]
        public void MazeInitialization_EndPosition_ShouldNotBeAdjacentToStart()
        {
            var maze = Maze.CreateRandomMaze();
            bool isAdjacent = false;

            for (int i = 0; i < maze.Path.Count; i++)
            {
                for (int j = 0; j < maze.Path[i].Count; j++)
                {
                    if (maze.Path[i][j] == MazeStatusEnum.End.ToInitial())
                    {
                        if (Math.Abs(i - 0) <= 1 && Math.Abs(j - 0) <= 1)
                        {
                            isAdjacent = true;
                        }
                    }
                }
            }

            Assert.False(isAdjacent);
        }

        [Test]
        public void MazeInitialization_ShouldContainBlockedPositions()
        {
            var maze = Maze.CreateRandomMaze();
            bool hasBlocked = false;

            for (int i = 0; i < maze.Path.Count; i++)
            {
                for (int j = 0; j < maze.Path[i].Count; j++)
                {
                    if (maze.Path[i][j] == MazeStatusEnum.NotGo.ToInitial())
                    {
                        hasBlocked = true;
                    }
                }
            }

            Assert.True(hasBlocked);
        }

        [Test]
        public void Maze_ShouldHavePathFromStartToEnd()
        {
            var maze = Maze.CreateRandomMaze();
            var hasValidPath = maze.HasValidPath();

            Assert.True(hasValidPath);
        }
    }
}
