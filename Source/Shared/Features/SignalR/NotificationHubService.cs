using Microsoft.AspNetCore.SignalR;

namespace Shared.Features.SignalR
{
    public class NotificationHubService : INotificationHubService
    {
        private readonly IHubContext<NotificationHub> notificationHubContext;

        public NotificationHubService(IHubContext<NotificationHub> notificationHubContext)
        {
            this.notificationHubContext = notificationHubContext;
        }

        public async Task SendNotificationAsync(Guid userId, string triggeredMethodName)
        {
            await notificationHubContext.Clients.User(userId.ToString()).SendAsync(triggeredMethodName);
        }
    }
}
