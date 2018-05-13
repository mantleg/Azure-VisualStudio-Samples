using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSamples.Libs
{
    public static class AzureStorageBlob
    {
        public static void CreateBlobContainer(string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));

            var blogClient = storageAccount.CreateCloudBlobClient();
            var container = blogClient.GetContainerReference(containerName);
            container.CreateIfNotExists(Microsoft.WindowsAzure.Storage.Blob.BlobContainerPublicAccessType.Blob);
            Console.ReadKey();
        }

        public static void UploadBlobToContainer(string containerName, string resourcePath, string ResourceName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnection"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

            //upload a blob to the container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(ResourceName);

            using (var fileStream = System.IO.File.OpenRead(resourcePath))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }

        public static void DownloadBlobFromContainer(string containerName, string resourcePath, string ResourceName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnection"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

            //upload a blob
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(ResourceName);

            using (var fileStream = System.IO.File.OpenRead(resourcePath))
            {
                blockBlob.DownloadToStream(fileStream);
            }
        }

        public static List<string> ListContainerBlobUris(string containerName)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnection"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            var blobs = container.ListBlobs();

            List<string> blobUris = new List<string>();

            foreach (var blob in blobs)
            {
                blobUris.Add(blob.Uri.ToString());
            }

            //  download blob
            //  CloudBlockBlob blockBlob2 = container.GetBlockBlobReference("img2.png")
    
            return blobUris;
        }

        public static class AzureStorageTable
        {

        }
    }
}
