using Azure.Storage.Blobs;


public class BlobStorageService
{
    private readonly string _connectionString;
    private readonly string _containerName;

    public BlobStorageService(string connectionString, string containerName)
    {
        _connectionString = connectionString;
        _containerName = containerName;
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var blobServiceClient = new BlobServiceClient(_connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

        // Ensure the container exists
        await containerClient.CreateIfNotExistsAsync();

        // Generate a unique file name
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

        var blobClient = containerClient.GetBlobClient(fileName);

        // Upload the file
        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream);
        }

        return blobClient.Uri.ToString(); // Return the file URL
    }

    public string GetBlobUrl(string blobName)
    {
        var blobUri = new Uri($"https://{_connectionString.Split(';')[1].Split('=')[1]}.blob.core.windows.net/{_containerName}/{blobName}");
        return blobUri.ToString();
    }
}
