using MediatR;
using Medicare.Application.Features.Queries.Location;
using Medicare.Application.Interfaces.ILocations;
using Medicare.Application.Models.Location;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, List<CityModel>>
    {
        private readonly ILocationRepository _locationRepository;

        public GetCitiesQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<List<CityModel>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            return await _locationRepository.GetCitiesByStateAsync(request.StateId);
        }
    }
}
