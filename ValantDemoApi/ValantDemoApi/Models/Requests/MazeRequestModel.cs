using System.Collections.Generic;

namespace ValantDemoApi.Models.Requests
{
    public record MazeRequestModel(string Name, List<List<string>> Path);
}
