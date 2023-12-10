namespace Shared.DomainFeatures.BlobStorageProvider
{
    public interface IBlobStorageClient
    {
        Task<string> CreateBlobAsync(string blobName);
    }
}
