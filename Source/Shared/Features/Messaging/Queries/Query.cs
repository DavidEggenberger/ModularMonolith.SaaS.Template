namespace Shared.Features.Messaging.Queries
{
    public class Query<IResponse>
    {
        public Guid ExecutingUserId { get; set; }
    }
}
