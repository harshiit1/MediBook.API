using MediatR;
using Medicare.Application.Features.Commands.Appointment;
using Medicare.Application.Interfaces.IAppointment;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, ResponseModel>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public DeleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<ResponseModel> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.CancelAppointmentByIdAsync(request.model);
        }
    }
}
