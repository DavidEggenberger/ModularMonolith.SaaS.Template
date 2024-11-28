namespace Shared.Kernel.Errors.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityType, string id) : base($"{entityType} with {id} was not found")
        {

        }

        public NotFoundException(string entityType, Guid id) : base($"{entityType} with {id} was not found")
        {

        }
    }
}
