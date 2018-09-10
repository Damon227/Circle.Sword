// ***********************************************************************
// Solution         : Circle.Sword
// Project          : Circle.Sword.Domain.Foundation
// File             : DataOptions.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using Microsoft.Extensions.Options;

namespace Damon.Domain.Foundation
{
    public class DataOptions : IOptions<DataOptions>
    {
        /// <summary>
        ///     Sql server 数据库连接字符串
        /// </summary>
        public string SqlServerConnectionString { get; set; }

        DataOptions IOptions<DataOptions>.Value => this;
    }
}