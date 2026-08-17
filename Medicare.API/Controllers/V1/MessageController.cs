using MediatR;
using Medicare.Application.Features.Commands.Message;
using Medicare.Application.Features.Queries.Notification;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : BaseApiController
    {
        private readonly IMediator _mediator;
        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("MessageListById/{id}")]
        public async Task<IActionResult> GetMessageListById(int id)
        {
            List<MessageResponseModel> response = new List<MessageResponseModel>();
            response = await _mediator.Send(new GetMessageListByIdQuery(id));
            return HandleListResponse(response);
        }
        [Authorize]
        [HttpGet("UpdateMessageToRead")]
        public async Task<IActionResult> UpdateMessageToRead(UpdateMessageRequestModel model)
        {
            ResponseModel response = new ResponseModel();
            response = await _mediator.Send(new UpdateMessageToReadCommand(model));
            return HandleResponse(response);
        }
    }
}
