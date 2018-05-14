using Azure_VisualStudio_Samples.Entities;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
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

    }

    public static class AzureStorageTable
    {
        public static CloudTable CreateTable(string tableName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnection"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(tableName);

            table.CreateIfNotExists();
            return table;
        }

        public static void CreateCustomer(CloudTable table,CustomerUS customer)
        {
            TableOperation insert = TableOperation.Insert(customer);
            table.Execute(insert);
        }

        public static string GetCustomer(CloudTable table, string partitionKey, string rowKey)
        {
            TableOperation retrive = TableOperation.Retrieve<CustomerUS>(partitionKey, rowKey);
            var result=table.Execute(retrive);

            if (result != null)
                return ((CustomerUS)result.Result).Name;

            throw new NullReferenceException();
        }

        public static List<String> GetAllCustomers(CloudTable table)
        {
            List<string> _customerName = new List<string>();

            TableQuery<CustomerUS> query =  new TableQuery<CustomerUS>() 
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "US"));

            foreach (CustomerUS customer in table.ExecuteQuery(query))
            {
                _customerName.Add(customer.Name);
            }

            return _customerName;
           
            
        }
   }

    public static class AzureStorageQueue
    {
        public static CloudQueue CreateQueue(string queueName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnection"));

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(queueName);

            queue.CreateIfNotExists();
            return queue;
        }

        public static void AddMessageToQueue(CloudQueue queue,string MessageText)
        {
            CloudQueueMessage message = new CloudQueueMessage(MessageText);
            queue.AddMessage(message);
        }

        // Get Message
        // Peek Message
        // Delete Message
    }
}
