using MediatR;
using Medicare.Application.Features.Queries.Doctor;
using Medicare.Application.Models.Doctor;
using Medicare.Application.Models.Speciality;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorController : BaseApiController
    {
        private readonly IMediator _mediator;
        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetDoctorList")]
        public async Task<IActionResult> GetDoctorList()
        {
            List<DoctorCategoryModel> response = new List<DoctorCategoryModel>();
            response = await _mediator.Send(new GetDoctorListQuery());
            return HandleListResponse(response);
        }

        [HttpGet]
        [Route("GetDoctorSpecialityList")]
        public async Task<IActionResult> GetDoctorSpecialityList(
            [FromQuery] string? doctorName,
            [FromQuery] string? departmentName)
        {
            List<DoctorSpecialityDataModel> response = new List<DoctorSpecialityDataModel>();
            response = await _mediator.Send(new GetDoctorSpecialityListQuery(doctorName, departmentName));
            return HandleListResponse(response);
        }


        [HttpPost]
        [Route("GetDoctorTimeSlotById")]
        public async Task<IActionResult> GetDoctorTimeSlot(DoctorTimeSlotRequestModel model)
        {
            List<DoctorAvailabilityModel> response = new List<DoctorAvailabilityModel>();
            response = await _mediator.Send(new GetDoctorTimeSlotQuery(model));
            return HandleListResponse(response);
        }
    }
}
