using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Circle.Sword.Infrastructure.DapperExtensions;
// ReSharper disable UnusedVariable

namespace Circle.Sword.TestConsole
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=Damon-Dev;Integrated Security=False;User ID=sa;Password=sasa;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                IDbConnection connection = new SqlConnection(connectionString);

                Teacher teacher = new Teacher
                {
                    Id = 1,
                    TeacherId = "003",
                    Name = "李老师",
                    Enabled = false,
                    CreateTime = DateTimeOffset.Now,
                    Amount = 98832542
                };

                Teacher teacher2 = new Teacher
                {
                    Id = 1,
                    TeacherId = "004",
                    Name = "袁老师",
                    Enabled = false,
                    CreateTime = DateTimeOffset.Now,
                    Amount = 12324
                };

                IEnumerable<Teacher> teachers = new List<Teacher> { teacher, teacher2 };

                await connection.InsertAsync(teacher);
                //IEnumerable<Teacher> result = await connection.QueryAsync<Teacher>("Name='李老师'","Id DESC");
                //await connection.InsertAsync(teachers);

                IDictionary<string, object> updatedDic = new Dictionary<string, object>
                {
                    {"Name","锅老师" }
                };
                await connection.UpdateAsync<Teacher>(updatedDic, "Id>5");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
