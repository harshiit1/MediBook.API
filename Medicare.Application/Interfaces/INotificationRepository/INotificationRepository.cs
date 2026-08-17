using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Notification;

namespace Medicare.Application.Interfaces.INotificationRepository
{
    public interface INotificationRepository
    {
        Task<ResponseModel> CreateAsync(SaveNotificationModel model);
    }
}
