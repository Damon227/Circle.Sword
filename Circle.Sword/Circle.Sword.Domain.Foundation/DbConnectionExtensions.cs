// ***********************************************************************
// Solution         : Damon.Core
// Project          : Damon.Domain.Foundation
// File             : DapperExtensions.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Damon.Domain.Foundation
{
    public static class DbConnectionExtensions
    {
        public static IDbConnection GetConnection(DataOptions dataOptions)
        {
            return new SqlConnection(dataOptions.SqlServerConnectionString);
        }
    }
}