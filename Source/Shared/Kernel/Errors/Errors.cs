using Shared.Kernel.Errors.Exceptions;

namespace Shared.Kernel.Errors
{
    public static class Errors
    {
        public static NotFoundException NotFound(string entityName, Guid id) => new NotFoundException(entityName, id);
        public static NotFoundException NotFound(string entityName, string id) => new NotFoundException(entityName, id);
        public static UnAuthorizedException UnAuthorized = new UnAuthorizedException();
    }
}
