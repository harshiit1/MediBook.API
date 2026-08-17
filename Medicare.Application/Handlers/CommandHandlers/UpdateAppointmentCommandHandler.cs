using MediatR;
using Medicare.Application.Features.Commands.Appointment;
using Medicare.Application.Interfaces.IAppointment;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, ResponseModel>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public UpdateAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ResponseModel> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.UpdateAppointmentDetailAsync(request.model);
        }
    }
}
