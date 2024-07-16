using System;

namespace ValantDemoApi.Helpers
{
    public static class MazeHelper
    {
        public const int MazeSize = 10;

        public static string ToInitial(this MazeStatusEnum status)
        {
            return status switch
            {
                MazeStatusEnum.Start => "S",
                MazeStatusEnum.End => "E",
                MazeStatusEnum.Go => "G",
                MazeStatusEnum.NotGo => "N",
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }

    public enum MazeStatusEnum
    {
        Start,
        End,
        Go,
        NotGo
    }
}
