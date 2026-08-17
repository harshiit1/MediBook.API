using MediatR;
using Medicare.Application.Models.Patient;

namespace Medicare.Application.Features.Queries.Patient
{
    public record GetRxOrderByPatientProfileIdQuery(int profileId) : IRequest<PatientProfileModel>;
}
