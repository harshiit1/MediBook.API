using MediatR;
using Medicare.Application.Features.Queries.Appointments;
using Medicare.Application.Interfaces.IAppointment;
using Medicare.Application.Models.Patient;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetMyAppointmentListByAssociateIdQueryHandler : IRequestHandler<GetMyAppointmentListByAssociateIdQuery, List<PatientProfileModel>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public GetMyAppointmentListByAssociateIdQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<List<PatientProfileModel>> Handle(GetMyAppointmentListByAssociateIdQuery request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.GetMyAppointmentListByAssociateIdAsync(request.associateId);
        }
    }
}
