using MediatR;
using Medicare.Application.Features.Commands.RxOrder;
using Medicare.Application.Features.Queries.RxOrder;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Orders;
using Medicare.Application.Models.RxOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class RxOrderController : BaseApiController
    {
        private readonly IMediator _mediator;
        public RxOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CreateRxOrder")]
        public async Task<IActionResult> CreateRxOrder(CreateRxOrderRequestModel model)
        {
            ResponseModel response = new ResponseModel();
            response = await _mediator.Send(new CreateRxOrderCommand(model));
            return HandleResponse(response);
        }

        [HttpPut]
        [Route("UpdateRxOrder")]
        public async Task<IActionResult> UpdateRxOrder(UpdateRxOrderRequestModel model)
        {
            ResponseModel response = new ResponseModel();
            response = await _mediator.Send(new UpdateRxOrderCommand(model));
            return HandleResponse(response);
        }

        [HttpDelete]
        [Route("CancelRxOrder")]
        public async Task<IActionResult> CancelRxOrder(CancelRxOrderRequestModel model)
        {
            ApiResponse<ResponseModel> ApiResponse = new ApiResponse<ResponseModel>();
            ResponseModel response = new ResponseModel();
            response = await _mediator.Send(new CancelRxOrderCommand(model));
            return HandleResponse(response);
        }

        [HttpGet]
        [Route("GetRxOrderByOrderId/{orderId}")]
        public async Task<IActionResult> GetRxOrderByOrderId(int orderId)
        {
            RxOrderDetailModel response = new RxOrderDetailModel();
            response = await _mediator.Send(new GetRxOrderByOrderIdQuery(orderId));
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("GetRxOrderByPatientProfileId")]
        public async Task<IActionResult> GetRxOrderByPatientProfileId(GetRxOrderRequestModel model)
        {
            List<RxOrderDetailModel> response = new List<RxOrderDetailModel>();
            response = await _mediator.Send(new GetRxOrderByPatientProfileIdQuery(model));
            return HandleListResponse(response);
        }
    }
}
