using System.Collections.Generic;
using System;
using ValantDemoApi.Helpers;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ValantDemoApi.Domain
{
    public class Maze
    {
        private readonly int _mazeSize = MazeHelper.MazeDefaultSize;
        private Random _random = new Random();

        public Guid Id { get; init; }
        public string Name { get; init; }
        public List<List<string>> Path { get; private set; } = new();

        private Maze(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public static Maze CreateMaze(string name, List<List<string>> path)
        {
            var maze = new Maze(name);
            maze.Path = path;

            return maze;
        }

        public static Maze CreateRandomMaze()
        {
            return CreateRandomMaze(1);
        }

        private static Maze CreateRandomMaze(int times)
        {
            if (times > 5)
                return null;

            var randomName = $"Maze_{DateTime.Now.ToString("yyyyMMddHHmmsss")}";
            var maze = new Maze(randomName);

            maze.InitializeMaze()
                .GenerateMaze(0, 0)
                .PlaceGoal();

            if (!maze.IsValidMaze().valid)
                return CreateRandomMaze(++times);

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

                if (IsInBounds(nx, ny) && Path[ny][nx] == MazeStatusEnum.NotGo.ToInitial())
                {
                    Path[y + dy][x + dx] = MazeStatusEnum.Go.ToInitial();
                    Path[ny][nx] = MazeStatusEnum.Go.ToInitial();
                    GenerateMaze(nx, ny);
                }
            }

            return this;
        }

        public bool HasValidPath()
        {
            bool[,] visited = new bool[Path.Count, Path[0].Count];
            Stack<(int, int)> stack = new Stack<(int, int)>();
            stack.Push((0, 0));

            bool hasValidPath = false;

            while (stack.Count > 0)
            {
                var (x, y) = stack.Pop();

                if (x < 0 || y < 0 || x >= Path.Count || y >= Path[0].Count || visited[x, y] || Path[x][y] == MazeStatusEnum.NotGo.ToInitial())
                    continue;

                if (Path[x][y] == MazeStatusEnum.End.ToInitial())
                {
                    hasValidPath = true;
                    break;
                }

                visited[x, y] = true;

                // Push adjacent cells to the stack
                stack.Push((x + 1, y));
                stack.Push((x - 1, y));
                stack.Push((x, y + 1));
                stack.Push((x, y - 1));
            }

            return hasValidPath;
        }

        private Maze InitializeMaze()
        {
            Path = new List<List<string>>();

            for (int i = 0; i < _mazeSize; i++)
            {
                Path.Add(new List<string>());

                for (int j = 0; j < _mazeSize; j++)
                {
                    Path[i].Add(MazeStatusEnum.NotGo.ToInitial()); // Initialize all positions as blocked (N)
                }
            }

            Path[0][0] = MazeStatusEnum.Start.ToInitial();

            return this;
        }

        private bool IsInBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _mazeSize && y < _mazeSize;
        }

        public (bool valid, List<string> errors) IsValidMaze()
        {
            var errors = new List<string>();

            // Check if the maze structure is valid
            if (String.IsNullOrEmpty(Name))
                errors.Add("The maze must have a name");

            if (Path is null || Path.Count < MazeHelper.MazeMinSize || Path.Count > MazeHelper.MazeMaxSize)
            {
                errors.Add($"The maze must have between {MazeHelper.MazeMinSize} and {MazeHelper.MazeMaxSize} rows");
                return (false, errors);
            }

            int rows = Path.Count;

            foreach (var row in Path)
            {
                if (row.Count != rows)
                {
                    errors.Add("The maze must have the same number of rows and columns");
                    return (false, errors);
                }
            }

            // Check if the first position is Start
            if (!Path[0][0].Equals(MazeStatusEnum.Start.ToInitial(), StringComparison.InvariantCultureIgnoreCase))
                errors.Add("The first position must be 'S'tart");

            // Check if there is an end position
            if (!Path.Any(cel => cel.Any(item => item.Equals(MazeStatusEnum.End.ToInitial(), StringComparison.InvariantCultureIgnoreCase))))
                errors.Add("The maze must have an 'E'nd position");

            // Check if the end position is not adjacent to start position
            if (MazeHelper.IsEndAdjascentToStart(Path))
                errors.Add("The maze 'E'nd position must not be adjacent to 'S'tart position");

            // Check if the maze contains blocked positions ('X')
            if (!Path.Any(cel => cel.Any(item => item.Equals(MazeStatusEnum.NotGo.ToInitial(), StringComparison.InvariantCultureIgnoreCase))))
                errors.Add("The maze must contain blocked positions ('X')");

            // Check if the maze contains a valid path (from 'S'tart to the 'E'nd)
            var maze = Maze.CreateMaze("AnyName", Path);

            if (maze == null)
                errors.Add("It was not possible to create the maze");
            else
            {
                if (!maze.HasValidPath())
                    errors.Add("The maze must contain a valid path (from 'S'tart to the 'E'nd)");
            }

            return (!errors.Any(), errors);
        }

        private Maze PlaceGoal()
        {
            int gx, gy;

            do
            {
                gx = _random.Next(_mazeSize);
                gy = _random.Next(_mazeSize);
            } while ((gx == 0 && gy == 0) || (Math.Abs(gx - 0) <= 1 && Math.Abs(gy - 0) <= 1));

            Path[gy][gx] = MazeStatusEnum.End.ToInitial();

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
