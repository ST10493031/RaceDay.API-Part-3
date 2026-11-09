using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace RaceDay.API.Services
{
    public class BlobService
    {
        private readonly string _connectionString;

        public BlobService(IConfiguration config)
        {
            // UseDevelopmentStorage=true means it uses the local Azurite emulator
            _connectionString = config["AzureBlobStorage"] ?? "UseDevelopmentStorage=true";
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient("event-banners");

            // Create container if it doesn't exist and set public access for viewing
            await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, true);

            return blobClient.Uri.ToString();
        }
    }
}