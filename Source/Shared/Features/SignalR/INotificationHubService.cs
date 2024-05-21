namespace Shared.Features.SignalR
{
    public interface INotificationHubService
    {
        Task SendNotificationAsync(Guid userId, string triggeredMethodName);
    }
}
