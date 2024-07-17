using System.Collections.Generic;
using ValantDemoApi.Domain;

namespace ValantDemoApi.Application
{
    public interface IMazeService
    {
        List<List<string>> Get();
    }

    internal class MazeService : IMazeService
    {
        public List<List<string>> Get()
        {
            var maze = Maze.CreateMaze();

            return maze.Path;
        }
    }
}
