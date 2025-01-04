namespace Shared.Features.Errors.Exceptions
{
    public class DomainException : Exception
    {
        public int StatusCode { get; private set; }

        public DomainException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
