using ValantDemoApi.Domain;
using ValantDemoApi.Models.Requests;

namespace ValantDemoApi.Mappers
{
    public static class MazeMapper
    {
        public static MazeResponseModel ToResponseModel(this Maze maze)
        {
            if (maze is null)
                return null;

            return new MazeResponseModel(maze.Id, maze.Name, maze.Path);
        }

        public static MazeWithoutPathResponseModel ToResponseWithoutPathModel(this Maze maze)
        {
            if (maze is null)
                return null;

            return new MazeWithoutPathResponseModel(maze.Id, maze.Name);
        }
    }
}
