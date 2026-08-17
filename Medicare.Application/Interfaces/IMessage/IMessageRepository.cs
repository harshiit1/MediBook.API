using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Message;

namespace Medicare.Application.Interfaces.IMessage
{
    public interface IMessageRepository
    {
        Task<List<MessageResponseModel>> GetNotificationListById(int id);
        Task<ResponseModel> UpdateMessageToReadAsync(UpdateMessageRequestModel model);
    }
}
