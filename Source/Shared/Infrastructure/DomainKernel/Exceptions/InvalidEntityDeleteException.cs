namespace Shared.Infrastructure.DomainKernel.Exceptions
{
    public class InvalidEntityDeleteException : DomainException
    {
        public InvalidEntityDeleteException(string message) : base(message)
        {

        }
    }
}
