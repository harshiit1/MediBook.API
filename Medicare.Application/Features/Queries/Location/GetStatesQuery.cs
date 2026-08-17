using MediatR;
using Medicare.Application.Models.Location;

namespace Medicare.Application.Features.Queries.Location
{
    public record GetStatesQuery(int CountryId) : IRequest<List<StateModel>>;
}
