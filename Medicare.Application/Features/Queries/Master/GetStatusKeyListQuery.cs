using MediatR;
using Medicare.Application.Models.Master;

namespace Medicare.Application.Features.Queries.Master
{
    public record GetStatusKeyListQuery() : IRequest<List<StatusCategoryModel>>;
}
