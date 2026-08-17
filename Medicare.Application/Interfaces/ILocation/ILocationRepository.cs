using Medicare.Application.Models.Location;

namespace Medicare.Application.Interfaces.ILocations
{
    public interface ILocationRepository
    {
        Task<List<CountryModel>> GetCountriesAsync();
        Task<List<StateModel>> GetStatesByCountryAsync(int countryId);
        Task<List<CityModel>> GetCitiesByStateAsync(int stateId);
    }
}
