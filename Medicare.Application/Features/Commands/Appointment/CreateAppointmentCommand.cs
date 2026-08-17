using MediatR;
using Medicare.Application.Models.Appointment;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Features.Commands.Appointment
{
    public record CreateAppointmentCommand(AppointmentMasterModel model) : IRequest<ResponseModel>;
}
