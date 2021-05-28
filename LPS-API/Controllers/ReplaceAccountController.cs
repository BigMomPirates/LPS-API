using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;

namespace LPS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReplaceAccountController : ControllerBase
    {
        private readonly ILogger<ReplaceAccountController> _logger;

        public ReplaceAccountController(ILogger<ReplaceAccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ReplaceAccount Get(string email, string name, string password, string birthdate)
        {
            var replaceAccount = new ReplaceAccount();

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText =
                    "REPLACE INTO account (email, name, password, birthdate) " +
                    "VALUES(@email, @name, md5(@password), @birthdate)";

                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@birthdate", birthdate);

                replaceAccount.rows_affected = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                replaceAccount.result = e.Message;
            }

            return replaceAccount;
        }
    }
}
