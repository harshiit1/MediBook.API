using Microsoft.AspNetCore.SignalR;

namespace Medicare.DAL.Helper.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst("RefId")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                Context.Abort();
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, $"user-{userId}");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            var userId = Context.User?.FindFirst("RefId")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"userI-{userId}");
            }

            await base.OnDisconnectedAsync(ex);
        }
    }
}
