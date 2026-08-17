using MediatR;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Features.Queries.Appointments
{
    public record UpdateAppointmentStatusQuery(string token) : IRequest<ResponseModel>;
}       