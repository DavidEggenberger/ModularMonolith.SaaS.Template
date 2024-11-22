namespace Shared.Features.Messaging.Query
{
    public class Query<IResponse>
    {
        public Guid ExecutingUserId { get; set; }
    }
}
