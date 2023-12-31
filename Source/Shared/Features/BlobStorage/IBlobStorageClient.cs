namespace Shared.Features.BlobStorageProvider
{
    public interface IBlobStorageClient
    {
        Task<string> CreateBlobAsync(string blobName);
    }
}
