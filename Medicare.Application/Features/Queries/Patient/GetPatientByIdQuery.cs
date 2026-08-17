using MediatR;
using Medicare.Application.Models.Patient;

namespace Medicare.Application.Features.Queries.Patient
{
    public record GetPatientByIdQuery(int Id) : IRequest<PatientDetailModel>; 
}
