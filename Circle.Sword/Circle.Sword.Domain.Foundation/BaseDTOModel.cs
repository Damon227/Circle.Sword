// ***********************************************************************
// Solution         : Damon.Core
// Project          : Damon.Domain.Foundation
// File             : BaseDTOModel.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;

namespace Damon.Domain.Foundation
{
    public class BaseDTOModel
    {
        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTimeOffset CreateTime { get; set; }

        /// <summary>
        ///     更新时间
        /// </summary>
        public DateTimeOffset UpdateTime { get; set; }
    }
}