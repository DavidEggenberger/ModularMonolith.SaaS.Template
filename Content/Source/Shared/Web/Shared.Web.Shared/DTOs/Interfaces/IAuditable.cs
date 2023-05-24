using System;

namespace Shared.Web.Shared.DTOs.Interfaces
{
    public interface IAuditable
    {
        Guid CreatedByUserId { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset LastUpdated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
