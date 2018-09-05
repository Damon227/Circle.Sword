// ***********************************************************************
// Solution         : Damon.Core
// Project          : Damon.Domain.User.Interface
// File             : User.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using Damon.Domain.Foundation;

namespace Circle.Sword.Domain.Identity.Interface
{
    /// <summary>
    ///     用户
    /// </summary>
    public class User : BaseDTOModel
    {
        /// <summary>
        ///     唯一标识
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///     用户名，可用于登录
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     手机号，可用于登录
        /// </summary>
        public string Cellphone { get; set; }

        /// <summary>
        ///     邮箱，可用于登录
        /// </summary>
        public string Email { get; set; }
    }
}