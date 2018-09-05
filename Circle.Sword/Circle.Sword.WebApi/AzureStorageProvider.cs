// ***********************************************************************
// Solution         : Damon.Core
// Project          : WebApi
// File             : AzureStorageProvider.cs
// Updated          : 2018-08-30 15:14
// ***********************************************************************
// <copyright>
//     Copyright © 2017 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;

namespace WebApi
{
    public class AzureStorageProvider
    {
        private readonly CloudBlobContainer _blobContainer;

        public AzureStorageProvider(IOptions<AzureStorageProviderOptions> options)
        {
            AzureStorageProviderOptions providerOptions = options.Value;

            CloudStorageAccount account = CloudStorageAccount.Parse(providerOptions.StorageConnectionString);
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            blobClient.DefaultRequestOptions.RetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(3), 5);
            _blobContainer = blobClient.GetContainerReference(providerOptions.BlobContainerName);
            _blobContainer.CreateIfNotExistsAsync().Wait();
        }

        public async Task<Stream> GetResourceStreamAsync(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            path = path.Replace("/" + _blobContainer.Name + "/", "");

            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(path);
            if (!await blob.ExistsAsync())
            {
                return null;
            }

            MemoryStream stream = new MemoryStream();
            await blob.DownloadToStreamAsync(stream);
            return stream;
        }
    }
}