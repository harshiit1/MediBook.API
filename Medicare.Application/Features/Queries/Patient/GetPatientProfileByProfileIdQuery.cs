using MediatR;
using Medicare.Application.Models.Patient;

namespace Medicare.Application.Features.Queries.Patient
{
    public record GetPatientProfileByProfileIdQuery(int profileId) : IRequest<PatientProfileModel>;
}
