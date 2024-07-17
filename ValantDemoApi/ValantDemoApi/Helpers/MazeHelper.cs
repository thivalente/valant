using System;
using System.Collections.Generic;

namespace ValantDemoApi.Helpers
{
    public static class MazeHelper
    {
        public const int MazeDefaultSize = 6;
        public const int MazeMinSize = 3;
        public const int MazeMaxSize = 10;

        public static bool IsEndAdjascentToStart(List<List<string>> path)
        {
            bool isAdjacent = false;

            for (int i = 0; i < path.Count; i++)
            {
                for (int j = 0; j < path[i].Count; j++)
                {
                    if (path[i][j] == MazeStatusEnum.End.ToInitial())
                    {
                        if (Math.Abs(i - 0) <= 1 && Math.Abs(j - 0) <= 1)
                        {
                            isAdjacent = true;
                        }
                    }
                }
            }

            return isAdjacent;
        }

        public static string ToInitial(this MazeStatusEnum status)
        {
            return status switch
            {
                MazeStatusEnum.Start => "S",
                MazeStatusEnum.End => "E",
                MazeStatusEnum.Go => "O",
                MazeStatusEnum.NotGo => "X",
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
