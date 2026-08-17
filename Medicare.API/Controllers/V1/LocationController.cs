using MediatR;
using Medicare.Application.Features.Queries.Location;
using Medicare.Application.Models.Location;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LocationController : BaseApiController
    {
        private readonly IMediator _mediator;
        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetCountriesList")]
        public async Task<IActionResult> GetCountriesList()
        {
            List<CountryModel> response = new List<CountryModel>();
            response = await _mediator.Send(new GetCountriesQuery());
            return HandleListResponse(response);
        }

        [HttpGet]
        [Route("GetStatesByCountryList/{countryId}")]
        public async Task<IActionResult> GetStatesByCountryList(int countryId)
        {
            List<StateModel> response = new List<StateModel>();
            response = await _mediator.Send(new GetStatesQuery(countryId));
            return HandleListResponse(response);
        }

        [HttpGet]
        [Route("GetCitiesByStateList/{stateId}")]
        public async Task<IActionResult> GetCitiesByStateList(int stateId)
        {
            List<CityModel> response = new List<CityModel>();
            response = await _mediator.Send(new GetCitiesQuery(stateId));
            return HandleListResponse(response);
        }
    }
}
