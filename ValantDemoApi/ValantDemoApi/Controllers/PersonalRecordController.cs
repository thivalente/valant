using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using ValantDemoApi.Application;
using ValantDemoApi.Mappers;
using ValantDemoApi.Models.Requests;
using ValantDemoApi.Models.Responses;

namespace ValantDemoApi.Controllers
{
    [ApiController]
    [Route("personal-records")]
    public class PersonalRecordController : ControllerBase
    {
        private readonly IPersonalRecordService _personalRecordService;
        private readonly ILogger<PersonalRecordController> _logger;

        public PersonalRecordController(IPersonalRecordService personalRecordService, ILogger<PersonalRecordController> logger)
        {
            this._personalRecordService = personalRecordService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add(PersonaRecordRequestModel personalRecord)
        {
            var result = this._personalRecordService.Add(personalRecord.TotalMistakes, personalRecord.TotalSeconds);
            var response = result.ToResponseModel();

            return Ok(ApiResponse<PersonaRecordResponseModel>.SuccessResult(HttpStatusCode.Created, response));
        }

        [HttpDelete]
        public IActionResult ClearRecords()
        {
            this._personalRecordService.RemoveAll();

            return Ok(ApiResponse<bool>.SuccessResult(HttpStatusCode.NoContent, true));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = this._personalRecordService.GetAll();
            var response = result.ConvertAll(pr => pr.ToResponseModel());

            return Ok(ApiResponse<List<PersonaRecordResponseModel>>.SuccessResult(HttpStatusCode.OK, response));
        }

        [HttpGet("top5")]
        public IActionResult GetTop5()
        {
            var result = this._personalRecordService.GetTop5();
            var response = result.ConvertAll(pr => pr.ToResponseModel());

            return Ok(ApiResponse<List<PersonaRecordResponseModel>>.SuccessResult(HttpStatusCode.OK, response));
        }
    }
}
