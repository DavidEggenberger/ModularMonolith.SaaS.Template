using System.Runtime.Serialization;

namespace Shared.Features.EFCore.MultiTenancy.Exceptions
{
    [Serializable]
    internal class CrossTenantUpdateException : Exception
    {
        private List<Guid> ids;

        public CrossTenantUpdateException()
        {
        }

        public CrossTenantUpdateException(List<Guid> ids)
        {
            this.ids = ids;
        }

        public CrossTenantUpdateException(string message) : base(message)
        {
        }

        public CrossTenantUpdateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CrossTenantUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}