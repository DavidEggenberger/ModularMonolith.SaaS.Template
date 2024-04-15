using Microsoft.AspNetCore.SignalR;
using Shared.Kernel.Constants.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Features.SignalR
{
    public class NotificationHubService
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
