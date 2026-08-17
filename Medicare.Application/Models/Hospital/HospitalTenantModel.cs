namespace Medicare.Application.Models.Hospital
{
    public class HospitalTenantModel
    {
        public int HospitalId { get; set; }
        public Guid TenantId { get; set; }
    }
}
