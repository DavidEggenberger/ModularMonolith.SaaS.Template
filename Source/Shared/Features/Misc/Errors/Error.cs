using Shared.Features.Misc.Errors.Exceptions;

namespace Shared.Features.Misc.Errors
{
    public static class Error
    {
        public static NotFoundException NotFound(string entityName, Guid id) => new NotFoundException(entityName, id);
        public static NotFoundException NotFound(string entityName, string id) => new NotFoundException(entityName, id);
        public static DomainException DomainException(string message, int statusCode) => new DomainException(message, statusCode);
        public static UnAuthorizedException UnAuthorized = new UnAuthorizedException();
    }
}
