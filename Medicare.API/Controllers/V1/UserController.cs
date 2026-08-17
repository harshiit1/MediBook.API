using MediatR;
using Medicare.API.Controllers.V1;
using Medicare.Application.Features.Queries.User;
using Medicare.Application.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Medicare_API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            UserInfoDataModel response = new UserInfoDataModel();
            response = await _mediator.Send(new GetUserByIdQuery(id));
            return HandleResponse(response);
        }
    }
}
