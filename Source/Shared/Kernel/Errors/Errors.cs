using Shared.Kernel.Errors.Exceptions;

namespace Shared.Kernel.Errors
{
    public static class Errors
    {
        public static NotFoundException NotFound = new NotFoundException();
        public static UnAuthorizedException UnAuthorized = new UnAuthorizedException();
    }
}
