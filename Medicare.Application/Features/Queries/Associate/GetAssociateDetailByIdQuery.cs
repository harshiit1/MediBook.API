using MediatR;
using Medicare.Application.Models.Associate;

namespace Medicare.Application.Features.Queries.Associate
{
    public record GetAssociateDetailByIdQuery(int associateId) : IRequest<AssociateDetailModel>;
}
