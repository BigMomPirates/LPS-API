using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace LPS_API
{
    public class Program
    {
        private static string ConnString = "";

        public static void Main(string[] args)
        {
            var DBConn = new Dictionary<string, string>();

            try
            {
                DBConn.Add("Server", args[0]);
                DBConn.Add("Port", args[1]);
                DBConn.Add("Database", args[2]);
                DBConn.Add("Uid", args[3]);
                DBConn.Add("Pwd", args[4]);
            }
            catch
            {
                Console.WriteLine(
                    "���α׷� ���� �� ���� �Ű������� �ʿ��մϴ�:\n" +
                    "[MySQL ���� �ּ�] [MySQL ��Ʈ ��ȣ] [MySQL DB �̸�] [MySQL ���� id] [MySQL ��й�ȣ]"
                    );
                return;
            }

            DBConn.Add("CharSet", "utf8");
            
            foreach(KeyValuePair<string, string> DBConnParameter in DBConn)
            {
                ConnString += string.Format("{0}={1};", DBConnParameter.Key, DBConnParameter.Value);
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static MySqlConnection GetMySqlConnection()
        {
            return new MySqlConnection(ConnString);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
