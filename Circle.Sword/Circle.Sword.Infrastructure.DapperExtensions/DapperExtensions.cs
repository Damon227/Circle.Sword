using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;

namespace Circle.Sword.Infrastructure.DapperExtensions
{
    public static class DapperExtensions
    {
        public static async Task InsertAsync<TEntity>(this IDbConnection connection, TEntity entity) 
            where TEntity : class, new()
        {
            string sql = BuildInsertSql(entity);

            await connection.ExecuteAsync(sql, entity);
        }

        //public static async Task InsertAsync<TEntity>(this IDbConnection connection, IEnumerable<TEntity> entities)
        //    where TEntity : class, new()
        //{
        //    string sql = BuildInsertSql(entities);

        //    await connection.ExecuteAsync(sql);
        //}

        public static async Task UpdateAsync<TEntity>(this IDbConnection connection, TEntity entity) 
            where TEntity : class, new() 
        {
            string sql = BuildUpdateSql(entity);

            await connection.ExecuteAsync(sql);
        }

        /// <summary>
        ///     构造插入SQL语句
        /// </summary>
        private static string BuildInsertSql<TEntity>(TEntity entity)
            where TEntity : class, new()
        {
            string tableName = GetTableName<TEntity>();

            (string Columns, string Values) data = GetInsertColumnsAndValues(entity);

            string sql = $"INSERT INTO {tableName}({data.Columns}) VALUES({data.Values})";

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

        private static (string Columns, string Values) GetInsertColumnsAndValues<TEntity>(TEntity entity)
            where TEntity : class, new()
        {
            string columns = null;
            string values = null;

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
                    values = $"'{value}'";
                }
                else
                {
                    values += $",'{value}'";
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
    }
}
