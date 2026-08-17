using MediatR;
using Medicare.Application.Models.Patient;

namespace Medicare.Application.Features.Queries.Patient
{
    public record GetPatientProfileListByIdQuery(int patientId) : IRequest<List<PatientProfileModel>>;
}
