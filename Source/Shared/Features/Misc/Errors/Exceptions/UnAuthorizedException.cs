namespace Shared.Features.Misc.Errors.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException() : base("UnAuthorized to see the entity")
        {

        }
    }
}
