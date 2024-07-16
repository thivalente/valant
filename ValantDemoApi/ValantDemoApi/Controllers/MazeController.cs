using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using ValantDemoApi.Application;
using ValantDemoApi.Models.Responses;

namespace ValantDemoApi.Controllers
{
    [ApiController]
    [Route("maze")]
    public class MazeController : ControllerBase
    {
        private readonly IMazeService _mazeService;
        private readonly ILogger<MazeController> _logger;

        public MazeController(IMazeService mazeService, ILogger<MazeController> logger)
        {
            this._mazeService = mazeService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetMaze()
        {
            var maze = this._mazeService.Get();

            return Ok(ApiResponse<List<List<string>>>.SuccessResult(HttpStatusCode.OK, maze));
        }

        [HttpGet("random")]
        public IActionResult GetRandomMaze()
        {
            var maze = this._mazeService.GetRandom();

            return Ok(ApiResponse<List<List<string>>>.SuccessResult(HttpStatusCode.OK, maze));
        }
    }
}
