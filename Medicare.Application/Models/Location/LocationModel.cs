namespace Medicare.Application.Models.Location
{
    public class CityModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
    }

    public class StateModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
    }

    public class CountryModel
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string PhoneCode { get; set; }
    }
}
