using MediatR;
using Medicare.Application.Features.Queries.Appointments;
using Medicare.Application.Interfaces.IAppointment;
using Medicare.Application.Interfaces.JwtToken;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class UpdateAppointmentStatusQueryHandler : IRequestHandler<UpdateAppointmentStatusQuery, ResponseModel>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IJwtTokenRepository _tokenRepository;
        public UpdateAppointmentStatusQueryHandler(IAppointmentRepository appointmentRepository, IJwtTokenRepository tokenRepository)
        {
            _appointmentRepository = appointmentRepository;
            _tokenRepository = tokenRepository;
        }
        public async Task<ResponseModel> Handle(UpdateAppointmentStatusQuery request, CancellationToken cancellationToken)
        {
            var appointmentId = _tokenRepository.ValidateAppointmentConfirmationToken(request.token);

            if(appointmentId == null)
            {
                return new ResponseModel
                {
                    Status = 0,
                    IsSuccess = 0,
                    ResponseId = 0,
                    ResponseMessage = "Invalid or Expired Confirmation Link."
                };
            }

            return await _appointmentRepository.ConfirmAppointmentStatusAsync(appointmentId.Value);
        }
    }
}
