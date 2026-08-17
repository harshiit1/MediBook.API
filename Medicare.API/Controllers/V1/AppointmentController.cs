using MediatR;
using Medicare.Application.Features.Commands.Appointment;
using Medicare.Application.Features.Queries.Appointments;
using Medicare.Application.Models.Appointment;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Patient;
using Medicare.DAL.Services.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly AppointmentReminderJobService _reminder;

        public AppointmentController(IMediator mediator, AppointmentReminderJobService reminder)
        {
            _mediator = mediator;
            _reminder = reminder;
        }

        [HttpPost]
        [Route("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment(AppointmentMasterModel model)
        {
            ResponseModel response = new ResponseModel();
            response = await _mediator.Send(new CreateAppointmentCommand(model));
            return HandleResponse(response);
        }

        [HttpPut]
        [Route("UpdateAppointmentSchedule")]
        public async Task<IActionResult> UpdateAppointmentSchedule(UpdateAppointmentScheduleRequestModel model)
        {
            ResponseModel response = new ResponseModel();
            response = await _mediator.Send(new UpdateAppointmentScheduleCommand(model));
            return HandleResponse(response);
        }

        [HttpDelete]
        [Route("CancelAppointmentById")]
        public async Task<IActionResult> CancelAppointmentById(CancelAppointmentScheduleRequestModel model)
        {
            ResponseModel response = new ResponseModel();
            response = await _mediator.Send(new DeleteAppointmentCommand(model));
            return HandleResponse(response);
        }

        [HttpGet]
        [Route("GetAvailableAppointments")]
        public async Task<IActionResult> GetAvailableAppointments([FromQuery] int associateId)
        {
            List<AvailableAppointmentModel> response = new List<AvailableAppointmentModel>();
            response = await _mediator.Send(new GetAvailableAppointmentsQuery(associateId));
            return HandleListResponse(response);
        }
        
        [HttpGet]
        [Route("GetAppointmentById/{appointmentId}")]
        public async Task<IActionResult> GetAppointmentById([FromRoute] int appointmentId)
        {
            AppointmentDetailModel response = new AppointmentDetailModel();
            response = await _mediator.Send(new GetAppointmentByIdQuery(appointmentId));
            return HandleResponse(response);
        }
        
        [HttpGet]
        [Route("Patient/GetMyAppointmentList/{patientId}")]
        public async Task<IActionResult> GetMyAppointmentListByPatientId(int patientId)
        {
            List<PatientAppointmentModel> response = new List<PatientAppointmentModel>();
            response = await _mediator.Send(new GetMyAppointmentListByPatientIdQuery(patientId));
            return HandleListResponse(response);
        }

        [HttpGet]
        [Route("Doctor/GetMyAppointmentList/{associateId}")]
        public async Task<IActionResult> GetMyAppointmentListByAssociateId(int associateId)
        {
            List<PatientProfileModel> response = new List<PatientProfileModel>();
            response = await _mediator.Send(new GetMyAppointmentListByAssociateIdQuery(associateId));
            return HandleListResponse(response);
        }

        [HttpPut]
        [Route("UpdateAppointmentStatus/{appointmentId}")]
        public async Task<IActionResult> UpdateAppointmentStatus(int associateId)
        {
            List<PatientProfileModel> response = new List<PatientProfileModel>();
            response = await _mediator.Send(new GetMyAppointmentListByAssociateIdQuery(associateId));
            return HandleListResponse(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmAppointmentStatus")]
        public async Task<IActionResult> ConfirmAppointmentStatus([FromQuery] string token)
        {
            ResponseModel response = new ResponseModel();
            response = await _mediator.Send(new UpdateAppointmentStatusQuery(token));
            return HandleResponse(response);
        }
    }
}
