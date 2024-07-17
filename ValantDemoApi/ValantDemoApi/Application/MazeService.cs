using System;
using System.Collections.Generic;
using ValantDemoApi.Domain;
using ValantDemoApi.Helpers;
using ValantDemoApi.Infrastructure;

namespace ValantDemoApi.Application
{
    public interface IMazeService
    {
        Maze Add(string name, List<List<string>> path);
        List<Maze> GetAll();
        Maze GetById(Guid id);
        Maze GetRandom();
        (bool valid, List<string> errors) IsValidMaze(string name, List<List<string>> maze);
        bool IsValidStep(int row, int column);
    }

    internal class MazeService : IMazeService
    {
        private readonly IMazeRepository _mazeRepository;

        public MazeService(IMazeRepository mazeRepository)
        {
            this._mazeRepository = mazeRepository;
        }

        public Maze Add(string name, List<List<string>> path)
        {
            return this._mazeRepository.Add(name, path);
        }

        public List<Maze> GetAll()
        {
            return this._mazeRepository.GetAll();
        }

        public Maze GetById(Guid id)
        {
            return this._mazeRepository.GetById(id);
        }

        public Maze GetRandom()
        {
            var maze = Maze.CreateRandomMaze();

            return maze;
        }

        public (bool valid, List<string> errors) IsValidMaze(string name, List<List<string>> path)
        {
            var newMaze = Maze.CreateMaze(name, path);

            return newMaze.IsValidMaze();
        }

        public bool IsValidStep(int row, int column)
        {
            var maze = new List<List<string>>();
            var validSteps = new List<string>() { MazeStatusEnum.Go.ToInitial(), MazeStatusEnum.End.ToInitial() };

            return row < 0 || row >= maze.Count || column < 0 || column >= maze[row].Count || (!validSteps.Contains(maze[row][column]));
        }
    }
}
