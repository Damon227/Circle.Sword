using System;

namespace Damon.Domain.Foundation
{
    /// <summary>
    ///     DAO 基础类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        ///     是否有效，逻辑删除标识
        /// </summary>
        public bool Enabled { get; set; }

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
