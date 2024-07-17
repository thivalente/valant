using System.Collections.Generic;
using System;
using ValantDemoApi.Helpers;

namespace ValantDemoApi.Domain
{
    public class Maze
    {
        private readonly int _mazeSize = MazeHelper.MazeSize;
        private Random _random = new Random();

        public List<List<string>> Path { get; private set; } = new();

        private Maze()
        { }

        public static Maze CreateMaze()
        {
            var maze = new Maze();

            maze.InitializeMaze()
                .GenerateMaze(0, 0)
                .PlaceGoal();

            return maze;
        }

        private Maze GenerateMaze(int x, int y)
        {
            var directions = new List<(int, int)> { (0, 1), (1, 0), (0, -1), (-1, 0) };

            Shuffle(directions);

            foreach (var (dx, dy) in directions)
            {
                int nx = x + dx * 2;
                int ny = y + dy * 2;

                if (IsInBounds(nx, ny) && Path[ny][nx] == "N")
                {
                    Path[y + dy][x + dx] = "G";
                    Path[ny][nx] = "G";
                    GenerateMaze(nx, ny);
                }
            }

            return this;
        }

        private Maze InitializeMaze()
        {
            Path = new List<List<string>>();

            for (int i = 0; i < _mazeSize; i++)
            {
                Path.Add(new List<string>());

                for (int j = 0; j < _mazeSize; j++)
                {
                    Path[i].Add("N"); // Initialize all positions as blocked (N)
                }
            }

            Path[0][0] = "S";

            return this;
        }

        private bool IsInBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _mazeSize && y < _mazeSize;
        }

        private Maze PlaceGoal()
        {
            int gx, gy;

            do
            {
                gx = _random.Next(_mazeSize);
                gy = _random.Next(_mazeSize);
            } while ((gx == 0 && gy == 0) || (Math.Abs(gx - 0) <= 1 && Math.Abs(gy - 0) <= 1));

            Path[gy][gx] = "E";

            return this;
        }

        private void Shuffle<T>(IList<T> list)
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
