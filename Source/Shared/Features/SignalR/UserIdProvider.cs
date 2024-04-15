using Microsoft.AspNetCore.SignalR;
using Shared.Kernel.Extensions.ClaimsPrincipal;

namespace Shared.Features.SignalR
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.Identity.IsAuthenticated 
                ? connection.User?.GetUserId<string>()
                : string.Empty;
        }
    }
}
