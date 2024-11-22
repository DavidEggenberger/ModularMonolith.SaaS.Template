namespace Shared.Features.Messaging.Command
{
    public class Command
    {
        public Guid ExecutingUserId { get; set; }
    }

    public class Command<IResponse>
    {
        public Guid ExecutingUserId { get; set; }
    }
}
