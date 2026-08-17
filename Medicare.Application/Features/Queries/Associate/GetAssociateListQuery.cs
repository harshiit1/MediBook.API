using MediatR;
using Medicare.Application.Models.Associate;

namespace Medicare.Application.Features.Queries.Associate
{
    public record GetAssociateListQuery() : IRequest<List<AssociateListModel>>;
}
