using MediatR;
using Medicare.Application.Features.Queries.Location;
using Medicare.Application.Interfaces.ILocations;
using Medicare.Application.Models.Location;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, List<CountryModel>>
    {
        private readonly ILocationRepository _locationRepository;

        public GetCountriesQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<List<CountryModel>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            return await _locationRepository.GetCountriesAsync();
        }
    }
}
