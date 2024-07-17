using System;
using System.Collections.Generic;
using NUnit.Framework;
using ValantDemoApi.Domain;

namespace ValantDemoApi.Tests.Domain
{
    [TestFixture]
    public class MazeTests
    {
        [Test]
        public void MazeInitialization_ShouldSetStartPosition()
        {
            var maze = Maze.CreateMaze();

            Assert.AreEqual("S", maze.Path[0][0]);
        }

        [Test]
        public void MazeInitialization_ShouldHaveOneEndPosition()
        {
            var maze = Maze.CreateMaze();
            int endCount = 0;

            for (int i = 0; i < maze.Path.Count; i++)
            {
                for (int j = 0; j < maze.Path[i].Count; j++)
                {
                    if (maze.Path[i][j] == "E")
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
            var maze = Maze.CreateMaze();
            bool isAdjacent = false;

            for (int i = 0; i < maze.Path.Count; i++)
            {
                for (int j = 0; j < maze.Path[i].Count; j++)
                {
                    if (maze.Path[i][j] == "E")
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
            var maze = Maze.CreateMaze();
            bool hasBlocked = false;

            for (int i = 0; i < maze.Path.Count; i++)
            {
                for (int j = 0; j < maze.Path[i].Count; j++)
                {
                    if (maze.Path[i][j] == "N")
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
            var maze = Maze.CreateMaze();
            bool[,] visited = new bool[maze.Path.Count, maze.Path[0].Count];
            Stack<(int, int)> stack = new Stack<(int, int)>();
            stack.Push((0, 0));

            bool pathExists = false;

            while (stack.Count > 0)
            {
                var (x, y) = stack.Pop();

                if (x < 0 || y < 0 || x >= maze.Path.Count || y >= maze.Path[0].Count || visited[x, y] || maze.Path[x][y] == "N")
                    continue;

                if (maze.Path[x][y] == "E")
                {
                    pathExists = true;
                    break;
                }

                visited[x, y] = true;

                // Push adjacent cells to the stack
                stack.Push((x + 1, y));
                stack.Push((x - 1, y));
                stack.Push((x, y + 1));
                stack.Push((x, y - 1));
            }

            Assert.True(pathExists);
        }
    }
}
