using System;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;

namespace AzureStorageExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connStr = "CONSTR";

            BlobServiceClient blobService = new BlobServiceClient(connStr);
            // string containerName = "cloudwork";
            // BlobContainerClient container = blobService.GetBlobContainerClient(containerName);
            // await foreach(var item in container.GetBlobsAsync())
            // {
            //     Console.WriteLine($"{item.Name}");
            //     BlobClient blob = container.GetBlobClient(item.Name);
            //     Console.WriteLine($"{blob.Uri.AbsoluteUri}");
            // }
            string containerName2 = "home";
            string path = "Data";
            string filename = "logo.png";
            string fullPath = Path.Combine(path, filename);
            BlobContainerClient container2 = new BlobContainerClient(connStr, containerName2);
            await container2.CreateIfNotExistsAsync(PublicAccessType.Blob);
            BlobClient blob1 = container2.GetBlobClient(filename);
            var res = await blob1.UploadAsync(fullPath);
            System.Console.WriteLine(res.GetRawResponse().Status);
            System.Console.WriteLine(res.GetRawResponse().ReasonPhrase);
        }
    }
}
