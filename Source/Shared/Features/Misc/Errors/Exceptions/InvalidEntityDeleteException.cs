using Microsoft.AspNetCore.Http;

namespace Shared.Features.Misc.Errors.Exceptions
{
    public class InvalidEntityDeleteException : DomainException
    {
        public InvalidEntityDeleteException(string message) : base(message, StatusCodes.Status409Conflict)
        {

        }
    }
}
