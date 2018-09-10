// ***********************************************************************
// Solution         : Damon.Core
// Project          : Damon.Domain.User
// File             : UserEntity.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Circle.Sword.Infrastructure.DapperExtensions;
using Damon.Domain.Foundation;

namespace Circle.Sword.Domain.Identity
{
    /// <summary>
    ///     用户
    /// </summary>
    [Table("Circle.Sword.Users", Schema = "dbo")]
    public class UserEntity : BaseEntity
    {
        /// <summary>
        ///     数据库自增Id
        /// </summary>
        [DbGenerated]
        public int Id { get; set; }

        /// <summary>
        ///     唯一标识
        /// </summary>
        [Key]
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

        /// <summary>
        ///     角色
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        ///     权限
        /// </summary>
        public string Permissions { get; set; }
    }
}