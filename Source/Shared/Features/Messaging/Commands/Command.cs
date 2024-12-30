namespace Shared.Features.Messaging.Commands
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
