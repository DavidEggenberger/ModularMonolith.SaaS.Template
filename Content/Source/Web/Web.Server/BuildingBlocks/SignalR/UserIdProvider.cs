using Microsoft.AspNetCore.SignalR;
using System;

namespace Web.Server.BuildingBlocks.SignalR
{
    public class UserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            throw new NotImplementedException();
            //return connection.User.GetUserId<string>();
        }
    }
}
