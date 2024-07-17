using System;
using System.Collections.Generic;

namespace ValantDemoApi.Models.Requests
{
    public record MazeResponseModel(Guid Id, string Name, List<List<string>> Path);
    public record MazeWithoutPathResponseModel(Guid Id, string Name);
}
