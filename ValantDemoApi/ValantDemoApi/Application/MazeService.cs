using System;
using System.Collections.Generic;
using ValantDemoApi.Helpers;
using ValantDemoApi.Infrastructure;

namespace ValantDemoApi.Application
{
    public interface IMazeService
    {
        List<List<string>> Get();
        List<List<string>> GetRandom();
    }

    internal class MazeService : IMazeService
    {
        private static Random _random = new Random();
        private readonly IMazeRepository _mazeRepository;

        public MazeService(IMazeRepository mazeRepository)
        {
            this._mazeRepository = mazeRepository;
        }

        public List<List<string>> Get()
        {
            return this._mazeRepository.Get();
        }

        public List<List<string>> GetRandom()
        {
            var maze = new List<List<string>>();

            for (int i = 0; i < MazeHelper.MazeSize; i++)
            {
                maze.Add(new List<string>(new string[MazeHelper.MazeSize]));
            }

            // Initialize maze with closed paths
            for (int i = 0; i < MazeHelper.MazeSize; i++)
            {
                for (int j = 0; j < MazeHelper.MazeSize; j++)
                {
                    maze[i][j] = MazeStatusEnum.NotGo.ToInitial();
                }
            }

            // Set the start point
            maze[0][0] = MazeStatusEnum.Start.ToInitial();

            // Randomly select the end point at the right or bottom edge
            const int sizeMinusOne = MazeHelper.MazeSize - 1;
            int endRow = _random.Next(2) == 0 ? sizeMinusOne : _random.Next(MazeHelper.MazeSize);
            int endCol = endRow == sizeMinusOne ? _random.Next(MazeHelper.MazeSize) : sizeMinusOne;
            maze[endRow][endCol] = MazeStatusEnum.End.ToInitial();

            // Generate a path from start to end
            GeneratePath(maze, 0, 0, endRow, endCol);

            // Fill the rest with random open or closed paths
            for (int i = 0; i < MazeHelper.MazeSize; i++)
            {
                for (int j = 0; j < MazeHelper.MazeSize; j++)
                {
                    if (maze[i][j] == MazeStatusEnum.NotGo.ToInitial() && _random.Next(2) == 0)
                    {
                        maze[i][j] = MazeStatusEnum.Go.ToInitial();
                    }
                }
            }

            return maze;
        }

        private static bool GeneratePath(List<List<string>> maze, int currentRow, int currentCol, int endRow, int endCol)
        {
            if (currentRow == endRow && currentCol == endCol)
                return true;

            // Possible directions to move: right, down, left, up
            List<(int, int)> directions = new List<(int, int)> { (0, 1), (1, 0), (0, -1), (-1, 0) };

            // Shuffle directions to create a random path
            Shuffle(directions);

            foreach ((int rowOffset, int colOffset) in directions)
            {
                int newRow = currentRow + rowOffset;
                int newCol = currentCol + colOffset;

                if (IsValidMove(maze, newRow, newCol))
                {
                    maze[newRow][newCol] = MazeStatusEnum.Go.ToInitial();

                    if (GeneratePath(maze, newRow, newCol, endRow, endCol))
                        return true;

                    maze[newRow][newCol] = MazeStatusEnum.NotGo.ToInitial(); // Backtrack
                }
            }

            return false;
        }

        private static bool IsValidMove(List<List<string>> maze, int row, int col)
        {
            return row >= 0 && row < MazeHelper.MazeSize && col >= 0 && col < MazeHelper.MazeSize && maze[row][col] == MazeStatusEnum.NotGo.ToInitial();
        }

        private static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
