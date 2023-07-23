using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.BlobStorageProvider
{
    public interface IBlobStorageClient
    {
        Task<string> CreateBlobAsync(string blobName);
    }
}
