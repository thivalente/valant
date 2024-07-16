using System.Collections.Generic;
using ValantDemoApi.Helpers;

namespace ValantDemoApi.Infrastructure
{
    public interface IMazeRepository
    {
        List<List<string>> Get();
    }

    internal class MazeRepository : IMazeRepository
    {
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

        public List<List<string>> Get()
        {
            return _defaultMaze;
        }
    }
}
