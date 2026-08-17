namespace Medicare.Application.Models.RxOrder
{
    public class GetRxOrderRequestModel
    {
        public int PatientId { get; set; }
        public int? ProfileId { get; set; }
    }
}
