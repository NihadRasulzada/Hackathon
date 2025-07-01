using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Storage.Azure
{
    public class AzureStorage
    {
        private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=blobstoragetest222;AccountKey=ruAtBhkDGaf8HOqaDa7c1hBW3o1hpHhM+g7WIuF7TAGSapwSNdQIqXdNgp4iEcIgphWMBMYAaeZk+AStx2TFhw==;EndpointSuffix=core.windows.net";

        private readonly BlobContainerClient _containerClient;


        public AzureStorage()
        {
            BlobServiceClient serviceClient = new(_connectionString);
            _containerClient = serviceClient.GetBlobContainerClient("images");

        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            string blobName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            BlobClient blob = _containerClient.GetBlobClient(blobName);


            var stream = file.OpenReadStream();

            //Blob üçün müəyyən Configuration
            BlobHttpHeaders headers = new BlobHttpHeaders()
            {
                ContentType = file.ContentType
            };

            //AzureSave
            await blob.UploadAsync(stream, headers);


            return blobName;
        }

        public string GetBlobLink(string blobName)
        {
            BlobClient blob = _containerClient.GetBlobClient(blobName);

            string blobUrl = blob.Uri.ToString();

            return blobUrl;
        }

        public async Task<string> DeleteBlobAsync(string blobName)
        {
            string message = string.Empty;
            BlobClient blob = _containerClient.GetBlobClient(blobName);
            /* var response = await blob.ExistsAsync();
             if (!response.Value)
             {
                 message = "Blob tapilmadi";
             }

             await blob.DeleteAsync();*/

            var response = await blob.DeleteIfExistsAsync();

            if (response.Value)
            {
                message = "Blob ugurla silindi.";
            }
            else
            {
                message = "Blob tapilmadi.";
            }

            return message;
        }

    }
}
