// ***********************************************************************
// Solution         : Damon.Core
// Project          : Damon.Domain.Foundation
// File             : DapperExtensions.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Damon.Domain.Foundation
{
    public class DapperExtensions
    {
        public static IDbConnection GetConnection()
        {
            return new SqlConnection();
        }
    }
}