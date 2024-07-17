using System;
using System.Collections.Generic;
using System.Linq;
using ValantDemoApi.Domain;
using ValantDemoApi.Helpers;

namespace ValantDemoApi.Infrastructure
{
    public interface IMazeRepository
    {
        Maze Add(string name, List<List<string>> newMaze);
        List<Maze> GetAll();
        Maze GetById(Guid id);
    }

    internal class MazeRepository : IMazeRepository
    {
        private static List<Maze> _mazes = new();

        private static List<List<string>> _defaultMaze = new List<List<string>>
        {
            new List<string> { MazeStatusEnum.Start.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial() },
            new List<string> { MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial() },
            new List<string> { MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial() },
            new List<string> { MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial() },
            new List<string> { MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial() },
            new List<string> { MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.NotGo.ToInitial() },
            new List<string> { MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.NotGo.ToInitial() },
            new List<string> { MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.NotGo.ToInitial() },
            new List<string> { MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.Go.ToInitial() },
            new List<string> { MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.NotGo.ToInitial(), MazeStatusEnum.End.ToInitial() }
        };

        public Maze Add(string name, List<List<string>> path)
        {
            var newMaze = Maze.CreateMaze(name, path);

            if (_mazes is null)
                _mazes = new();

            _mazes.Add(newMaze);

            return newMaze;
        }

        public List<Maze> GetAll()
        {
            return _mazes;
        }

        public Maze GetById(Guid id)
        {
            return _mazes?.FirstOrDefault(m => m.Id == id);
        }

        public List<List<string>> GetDefault()
        {
            return _defaultMaze;
        }
    }
}
