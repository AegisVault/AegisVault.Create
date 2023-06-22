using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisVault.Create.Helpers
{
    public class BlobStorageHelper
    {
        public async Task<string> UploadFile(IFormFile blob)
        {
            var connstr = Environment.GetEnvironmentVariable("BlobConnectionString");

            var blobServiceClient = new BlobServiceClient(connstr);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("uploadedfiles");
            string directory = $"{Guid.NewGuid()}/{blob.FileName}";
            BlobClient blobClient = containerClient.GetBlobClient(directory);
            await blobClient.UploadAsync(blob.OpenReadStream());
            return directory;
        }
    }
}
