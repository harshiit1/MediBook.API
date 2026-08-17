using MediatR;
using Medicare.Application.Models.Location;

namespace Medicare.Application.Features.Queries.Location
{
    public record GetCitiesQuery(int StateId) : IRequest<List<CityModel>>;
}
