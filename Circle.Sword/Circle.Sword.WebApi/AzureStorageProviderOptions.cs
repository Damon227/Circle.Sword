// ***********************************************************************
// Solution         : Damon.Core
// Project          : WebApi
// File             : AzureStorageProviderOptions.cs
// Updated          : 2018-08-30 15:17
// ***********************************************************************
// <copyright>
//     Copyright © 2017 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using Microsoft.Extensions.Options;

namespace WebApi
{
    public class AzureStorageProviderOptions : IOptions<AzureStorageProviderOptions>
    {
        public string StorageConnectionString { get; set; }

        public string BlobContainerName { get; set; }

        #region IOptions<AzureStorageProviderOptions> Members

        AzureStorageProviderOptions IOptions<AzureStorageProviderOptions>.Value => this;

        #endregion
    }
}