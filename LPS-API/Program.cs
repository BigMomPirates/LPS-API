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
            var connArgs = new Dictionary<string, string>();

            try
            {
                connArgs.Add("Server", args[0]);
                connArgs.Add("Port", args[1]);
                connArgs.Add("Database", args[2]);
                connArgs.Add("Uid", args[3]);
                connArgs.Add("Pwd", args[4]);
            }
            catch
            {
                Console.WriteLine(
                    "���α׷� ���� �� ���� �Ű������� �ʿ��մϴ�:\n" +
                    "[MySQL ���� �ּ�] [MySQL ��Ʈ ��ȣ] [MySQL DB �̸�] [MySQL ���� id] [MySQL ��й�ȣ]"
                    );
                return;
            }

            connArgs.Add("CharSet", "utf8");
            
            foreach(KeyValuePair<string, string> connArg in connArgs)
            {
                ConnString += string.Format("{0}={1};", connArg.Key, connArg.Value);
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
