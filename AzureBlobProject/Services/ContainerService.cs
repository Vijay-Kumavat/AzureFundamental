using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobProject.Services
{
    public class ContainerService : IContainerService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public ContainerService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task CreateContainer(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.CreateIfNotExistsAsync();
        }

        public async Task DeleteContainer(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllContainer()
        {
            List<string> containerNameList = new List<string>();
            await foreach (BlobContainerItem blobContainerItem in _blobServiceClient.GetBlobContainersAsync())
            {
                containerNameList.Add(blobContainerItem.Name);
            }
            return containerNameList;
        }

        public Task<List<string>> GetAllContainerAndBlobs()
        {
            throw new NotImplementedException();
        }
    }
}
