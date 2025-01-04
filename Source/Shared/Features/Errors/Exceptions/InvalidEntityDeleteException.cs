using Microsoft.AspNetCore.Http;

namespace Shared.Features.Errors.Exceptions
{
    public class InvalidEntityDeleteException : DomainException
    {
        public InvalidEntityDeleteException(string message) : base(message, StatusCodes.Status409Conflict)
        {

        }
    }
}
