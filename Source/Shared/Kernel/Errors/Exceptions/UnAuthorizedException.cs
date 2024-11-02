namespace Shared.Kernel.Errors.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException() : base("UnAuthorized to see the entity")
        {
            
        }
    }
}
