using MediatR;
using Medicare.Application.Features.Commands.ErrorLog;
using Medicare.Application.Features.Queries.ErrorLog;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Medicare.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ErrorLogController : Controller
    {
        private readonly IMediator _mediator;
        public ErrorLogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> LogError([FromBody] ErrorLogModel model)
        {
            ApiResponse<ErrorLogModel> ApiResponse = new ApiResponse<ErrorLogModel>();
            ErrorLogModel response = new ErrorLogModel();
            response = await _mediator.Send(new ErrorLogCommand(model));
            ApiResponse = new ApiResponse<ErrorLogModel>()
            {
                Data = response,
                StatusMessage = "Error Logged Successfully",
                StatusCode = HttpStatusCode.OK,
                Result = 1
            };
            return Ok(ApiResponse);
        }

        [Authorize]
        [HttpGet]
        [Route("GetErrorLog")]
        public async Task<IActionResult> GetErrorLog()
        {
            ApiResponse<List<ErrorLogModel>> ApiResponse = new ApiResponse<List<ErrorLogModel>>();
            List<ErrorLogModel> response = new List<ErrorLogModel>();
            response = await _mediator.Send(new GetErrorLogQuery());
            ApiResponse = new ApiResponse<List<ErrorLogModel>>()
            {
                Data = response,
                StatusMessage = "Error Log Fetched Successfully",
                StatusCode = HttpStatusCode.OK,
                Result = 1
            };
            return Ok(ApiResponse);
        }
    }
}
