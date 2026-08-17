using MediatR;
using Medicare.Application.Features.Commands.Appointment;
using Medicare.Application.Interfaces.IAppointment;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class UpdateAppointmentScheduleCommandHandler : IRequestHandler<UpdateAppointmentScheduleCommand, ResponseModel>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public UpdateAppointmentScheduleCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ResponseModel> Handle(UpdateAppointmentScheduleCommand request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.UpdateAppointmentScheduleAsync(request.model);
        }
    }
}
