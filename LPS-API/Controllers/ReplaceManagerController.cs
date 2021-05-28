using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;

namespace LPS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReplaceManagerController : ControllerBase
    {
        private readonly ILogger<ReplaceManagerController> _logger;

        public ReplaceManagerController(ILogger<ReplaceManagerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ReplaceManager Get(string account_email, string team_name, string picture_url)
        {
            var replaceManager = new ReplaceManager();

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText =
                    "REPLACE INTO manager (account_email, team_name, picture_url) " +
                    "VALUES(@account_email, @team_name, @picture_url)";

                cmd.Parameters.AddWithValue("@account_email", account_email);
                cmd.Parameters.AddWithValue("@team_name", team_name);

                replaceManager.rows_affected = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                replaceManager.result = e.Message;
            }

            return replaceManager;
        }
    }
}
