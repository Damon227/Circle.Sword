using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;

namespace Circle.Sword.Infrastructure.DapperExtensions
{
    public static class DapperExtensions
    {
        /// <summary>
        ///     插入
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="entity">实体</param>
        public static async Task InsertAsync<TEntity>(this IDbConnection connection, TEntity entity) 
            where TEntity : class, new()
        {
            string sql = BuildInsertSql<TEntity>();

            connection.Open();

            await connection.ExecuteAsync(sql, entity);

            connection.Close();
        }

        /// <summary>
        ///     批量插入
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="entities">实体</param>
        public static async Task InsertAsync<TEntity>(this IDbConnection connection, IEnumerable<TEntity> entities)
            where TEntity : class, new()
        {
            string sql = BuildInsertSql<TEntity>();

            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();

            await connection.ExecuteAsync(sql, entities, transaction);
            transaction.Commit();

            connection.Close();
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="id">TEntity对应的逻辑主键</param>
        public static async Task DeleteAsync<TEntity>(this IDbConnection connection, string id)
            where TEntity : class, new()
        {
            string sql = BuildDeleteSql<TEntity>();

            connection.Open();
            await connection.ExecuteAsync(sql, id);
            connection.Close();
        }

        /// <summary>
        ///     更新
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="entity">实体</param>
        public static async Task UpdateAsync<TEntity>(this IDbConnection connection, TEntity entity) 
            where TEntity : class, new() 
        {
            string sql = BuildUpdateSql(entity);

            connection.Open();

            await connection.ExecuteAsync(sql);

            connection.Close();
        }

        /// <summary>
        ///     更新
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="updates">更新的字段</param>
        /// <param name="where">更新条件</param>
        /// <param name="transaction">事务</param>
        public static async Task UpdateAsync<TEntity>(this IDbConnection connection, IDictionary<string, object> updates, string where, IDbTransaction transaction = null)
            where TEntity : class, new()
        {
            string sql = BuildUpdateSql<TEntity>(updates, where);

            connection.Open();

            await connection.ExecuteAsync(sql, null, transaction);
            transaction?.Commit();

            connection.Close();
        }

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="sql">更新SQL语句</param>
        /// <param name="transaction">事务</param>
        public static async Task UpdateAsync(this IDbConnection connection, string sql, IDbTransaction transaction = null)
        {
            connection.Open();

            await connection.ExecuteAsync(sql, null, transaction);
            transaction?.Commit();

            connection.Close();
        }

        /// <summary>
        ///     查询
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序条件</param>
        public static async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(this IDbConnection connection, string where = null, string orderby = null)
            where TEntity : class, new()
        {
            string sql = BuildQuerySql<TEntity>(where, orderby);

            connection.Open();

            IEnumerable<TEntity> entities = await SqlMapper.QueryAsync<TEntity>(connection, sql);

            connection.Close();

            return entities;
        }

        /// <summary>
        ///     查询单条
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序条件</param>
        public static async Task<TEntity> QueryFirstOrDefaultAsync<TEntity>(this IDbConnection connection, string where = null, string orderby = null)
            where TEntity : class, new()
        {
            string sql = BuildQuerySql<TEntity>(where, orderby);

            connection.Open();

            TEntity entity = await SqlMapper.QueryFirstOrDefaultAsync<TEntity>(connection, sql);

            connection.Close();

            return entity;
        }

        /// <summary>
        ///     构造插入SQL语句
        /// </summary>
        private static string BuildInsertSql<TEntity>()
            where TEntity : class, new()
        {
            string tableName = GetTableName<TEntity>();

            (string Columns, string Values) data = GetInsertColumnsAndValues<TEntity>();

            string sql = $"INSERT INTO {tableName}({data.Columns}) VALUES({data.Values})";

            return sql;
        }

        private static string BuildDeleteSql<TEntity>()
            where TEntity : class, new()
        {
            string tableName = GetTableName<TEntity>();
            string keyColumnName = GetKeyColumnName<TEntity>();

            string sql = $"DELETE FROM {tableName} WHERE {keyColumnName}=@{keyColumnName}";

            return sql;
        }

        /// <summary>
        ///     构造更新SQL语句
        /// </summary>
        private static string BuildUpdateSql<TEntity>(TEntity entity)
            where TEntity : class, new()
        {
            string tableName = GetTableName<TEntity>();

            string values = GetUpdateValues(entity);

            string sql = $"UPDATE {tableName} SET {values}";

            return sql;
        }

        /// <summary>
        ///     构造更新SQL语句
        /// </summary>
        private static string BuildUpdateSql<TEntity>(IDictionary<string, object> updates, string where)
            where TEntity : class, new()
        {
            string tableName = GetTableName<TEntity>();

            string setString = null;
            foreach (KeyValuePair<string, object> kv in updates)
            {
                if (setString == null)
                {
                    setString = $"{kv.Key}='{kv.Value}'";
                }
                else
                {
                    setString += $",{kv.Key}='{kv.Value}'";
                }
            }

            string sql = $"UPDATE {tableName} SET {setString}";
            if (!string.IsNullOrWhiteSpace(where))
            {
                sql += $" WHERE {where}";
            }

            return sql;
        }

        /// <summary>
        ///     构造查询SQL语句
        /// </summary>
        private static string BuildQuerySql<TEntity>(string where = null, string orderby = null)
            where TEntity : class, new()
        {
            string tableName = GetTableName<TEntity>();

            string sql = $"SELECT * FROM {tableName}";
            if (!string.IsNullOrWhiteSpace(where))
            {
                sql += $" WHERE {where}";
            }

            if (!string.IsNullOrWhiteSpace(orderby))
            {
                sql += $" ORDER BY {orderby}";
            }

            return sql;
        }

        /// <summary>
        ///     通过 TableAttribute 获取表名
        /// </summary>
        private static string GetTableName<TEntity>() 
            where TEntity : class, new()
        {
            string tableName = null;

            Attribute[] attrs = Attribute.GetCustomAttributes(typeof(TEntity));

            foreach (Attribute attr in attrs)
            {
                if (attr is TableAttribute tableAttr)
                {
                    tableName = $"{tableAttr.Name}";
                    if (!string.IsNullOrWhiteSpace(tableAttr.Schema))
                    {
                        tableName = $"{tableAttr.Schema}.[{tableName}]";
                    }
                }
            }

            if (string.IsNullOrEmpty(tableName))
            {
                throw new Exception($"The enity type of {typeof(TEntity)} is not exist TableAttribute.");
            }

            return tableName;
        }

        private static (string Columns, string Values) GetInsertColumnsAndValues<TEntity>()
            where TEntity : class, new()
        {
            string columns = null;
            string values = null;

            PropertyInfo[] props = typeof(TEntity).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                // 过滤数据库自增主键
                bool jumpOver = false;

                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    if (attr is DbGeneratedAttribute)
                    {
                        jumpOver = true;
                        break;
                    }
                }

                if (jumpOver)
                {
                    continue;
                }

                if (columns == null)
                {
                    columns = prop.Name;
                }
                else
                {
                    columns += $",{prop.Name}";
                }

                if (values == null)
                {
                    values = $"@{prop.Name}";
                }
                else
                {
                    values += $",@{prop.Name}";
                }
            }

            return (columns, values);
        }

        private static string GetUpdateValues<TEntity>(TEntity entity)
            where TEntity : class, new()
        {
            string values = null;
            string where = null;

            PropertyInfo[] props = typeof(TEntity).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                string value = prop.GetValue(entity).ToString();

                // 处理特殊的类型
                if (prop.PropertyType == typeof(bool))
                {
                    value = value == "True" ? "1" : "0";
                }

                // 过滤数据库自增主键
                bool jumpOver = false;

                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    if (attr is DbGeneratedAttribute)
                    {
                        jumpOver = true;
                        break;
                    }

                    if (attr is KeyAttribute)
                    {
                        where = $"WHERE {prop.Name}={value}";
                    }
                }

                if (jumpOver)
                {
                    continue;
                }

                if (values == null)
                {
                    values = $"{prop.Name}='{value}'";
                }
                else
                {
                    values += $",{prop.Name}='{value}'";
                }
            }

            return $"{values} {where}";
        }

        /// <summary>
        ///     获取有KeyAttribute的属性名称
        /// </summary>
        private static string GetKeyColumnName<TEntity>()
            where TEntity : class, new()
        {
            PropertyInfo[] props = typeof(TEntity).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                // 查询逻辑主键

                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    if (attr is KeyAttribute)
                    {
                        return prop.Name;
                    }
                }
            }

            throw new Exception($"The type of {typeof(TEntity)} not exist Key Attribute.");
        }
    }
}
