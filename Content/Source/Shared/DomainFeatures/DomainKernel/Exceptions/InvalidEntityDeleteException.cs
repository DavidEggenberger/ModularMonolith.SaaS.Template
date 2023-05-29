namespace Shared.DomainFeatures.Exceptions
{
    public class InvalidEntityDeleteException : DomainException
    {
        public InvalidEntityDeleteException(string message) : base(message)
        {

        }
    }
}
