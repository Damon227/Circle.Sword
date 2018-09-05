using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Circle.Sword.Infrastructure.DapperExtensions;

namespace Circle.Sword.TestConsole
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                string connectionString = "Data Source=kc-fengniaowu-dev.database.chinacloudapi.cn;Initial Catalog=KC.Fengniaowu.Talos-Dev-Local;Integrated Security=False;User ID=KC;Password=V245ZGxbEhn3Sakk;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                IDbConnection connection = new SqlConnection(connectionString);

                Teacher teacher = new Teacher
                {
                    Id = 1,
                    Name = "李老师",
                    Enabled = false,
                    CreateTime = DateTimeOffset.Now,
                    Amount = 98832542
                };

                await connection.InsertAsync(teacher);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
