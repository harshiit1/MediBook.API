namespace Medicare.Application.Interfaces.IErrorHandling
{
    public interface IErrorHandling
    {
        int IsSuccess { get; set; }
        string ResponseMessage { get; set; }
    }
}
