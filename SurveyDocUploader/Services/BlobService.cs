using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using SurveyDocUploader.Models.Dtos;

namespace SurveyDocUploader.Services
{
    public class BlobService : IBlobService
    {
        private const string StorageAccountName = "stdocreader";

        private readonly KeyVaultOptions _options;

        public BlobService(
            IOptions<KeyVaultOptions> options)
        {
            _options = options.Value;
        }

        public async Task<ContentDto> GetBlobFile(string name)
        {
            var container = GetBlobContainerClient("templates");
            var blob = container.GetBlobClient(name);

            if (await blob.ExistsAsync())
            {
                var a = await blob.DownloadAsync();
                var contentDto = new ContentDto()
                {
                    Content = a.Value.Content,
                    ContentType = a.Value.ContentType,
                    Name = name
                };

                return contentDto;
            }

            return null!;
        }

        public async Task UploadBlobFile(IBrowserFile file)
        {
            try
            {
                var container = GetBlobContainerClient("uploads");

                //Append Time Signature
                var currentTime = DateTime.UtcNow;
                var fileName = Path.GetFileNameWithoutExtension(file.Name);
                var fileExtension = Path.GetExtension(file.Name);
                var newFileName = fileName + "_" + currentTime.Ticks + fileExtension;

                // Create a new Blob client.
                var blob = container.GetBlobClient(newFileName);

                // If a blob with the same name exists, then we delete the Blob and its snapshots.
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

                // Create a file stream and use the UploadSync method to upload the Blob.
                using (var fileStream = file.OpenReadStream())
                {
                    await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = file.ContentType });
                }
            }
            catch (Exception ex)
            {
            }
        }

        private BlobContainerClient GetBlobContainerClient(string containerName)
        {
            StorageSharedKeyCredential sharedKeyCredential = new(StorageAccountName, _options.StorageKey.ToString());

            string blobUri = $"https://{StorageAccountName}.blob.core.windows.net/{containerName}";

            var blobContainerClient = new BlobContainerClient(new Uri(blobUri), sharedKeyCredential);

            return blobContainerClient;
        }
    }
}
