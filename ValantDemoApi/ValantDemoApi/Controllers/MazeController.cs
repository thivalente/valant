using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using ValantDemoApi.Application;
using ValantDemoApi.Mappers;
using ValantDemoApi.Models.Requests;
using ValantDemoApi.Models.Responses;

namespace ValantDemoApi.Controllers
{
    [ApiController]
    [Route("mazes")]
    public class MazeController : ControllerBase
    {
        private readonly IMazeService _mazeService;
        private readonly ILogger<MazeController> _logger;

        public MazeController(IMazeService mazeService, ILogger<MazeController> logger)
        {
            this._mazeService = mazeService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add(MazeRequestModel request)
        {
            var isValid = this._mazeService.IsValidMaze(request.Name, request.Path);

            if (!isValid.valid)
                return Ok(ApiResponse<List<string>>.BadRequestResult(isValid.errors));

            var result = this._mazeService.Add(request.Name, request.Path);
            var response = result.ToResponseModel();

            return Ok(ApiResponse<MazeResponseModel>.SuccessResult(HttpStatusCode.Created, response));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = this._mazeService.GetAll();
            var response = result.ConvertAll(m => m.ToResponseWithoutPathModel());

            if (response == null)
                return NotFound();

            return Ok(ApiResponse<List<MazeWithoutPathResponseModel>>.SuccessResult(HttpStatusCode.OK, response));
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var result = this._mazeService.GetById(id);
            var response = result?.ToResponseModel();

            if (response == null)
                return NotFound();

            return Ok(ApiResponse<MazeResponseModel>.SuccessResult(HttpStatusCode.OK, response));
        }

        [HttpGet("random")]
        public IActionResult GetRandom()
        {
            var result = this._mazeService.GetRandom();
            var response = result?.ToResponseModel();

            if (response == null)
                return NotFound();

            return Ok(ApiResponse<MazeResponseModel>.SuccessResult(HttpStatusCode.OK, response));
        }

        [HttpGet("is-valid/{row:int}/{column:int}/")]
        public IActionResult IsValidStep(int row, int column)
        {
            var isValid = this._mazeService.IsValidStep(row, column);

            return Ok(ApiResponse<bool>.SuccessResult(HttpStatusCode.OK, isValid));
        }
    }
}
