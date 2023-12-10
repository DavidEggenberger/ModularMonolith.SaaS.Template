namespace Shared.Infrastructure.BlobStorageProvider
{
    public interface IBlobStorageClient
    {
        Task<string> CreateBlobAsync(string blobName);
    }
}
