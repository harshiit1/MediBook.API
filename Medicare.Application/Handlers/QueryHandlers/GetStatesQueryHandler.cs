using MediatR;
using Medicare.Application.Features.Queries.Location;
using Medicare.Application.Interfaces.ILocations;
using Medicare.Application.Models.Location;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetStatesQueryHandler : IRequestHandler<GetStatesQuery, List<StateModel>>
    {
        private readonly ILocationRepository _locationRepository;

        public GetStatesQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<List<StateModel>> Handle(GetStatesQuery request, CancellationToken cancellationToken)
        {
            return await _locationRepository.GetStatesByCountryAsync(request.CountryId);
        }
    }
}
