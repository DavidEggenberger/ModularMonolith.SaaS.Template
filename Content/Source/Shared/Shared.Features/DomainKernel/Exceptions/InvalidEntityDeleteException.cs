namespace Shared.Domain.Exceptions
{
    public class InvalidEntityDeleteException : DomainException
    {
        public InvalidEntityDeleteException(string message) : base(message)
        {

        }
    }
}
