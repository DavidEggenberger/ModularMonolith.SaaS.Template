using System;

namespace Shared.Features.BuildingBlocks.Interfaces
{
    public interface IAuditable
    {
        Guid CreatedByUserId { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset LastUpdated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
