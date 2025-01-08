namespace Shared.Features.Misc.Errors.Exceptions
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
