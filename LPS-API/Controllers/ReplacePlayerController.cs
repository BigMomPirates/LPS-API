using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;

namespace LPS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReplacePlayerController : ControllerBase
    {
        private readonly ILogger<ReplacePlayerController> _logger;

        public ReplacePlayerController(ILogger<ReplacePlayerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ReplacePlayer Get(string account_email, string team_name, string position, string jersey_number, string height, string weight, string is_captain, string picture_url)
        {
            var replacePlayer = new ReplacePlayer();

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText =
                    "REPLACE INTO team (account_email, team_name, position, jersey_number, height, weight, is_captain, picture_url) " +
                    "VALUES(@account_email, @team_name, @position, @jersey_number, @height, @weight, @is_captain, @picture_url)";

                cmd.Parameters.AddWithValue("@account_email", account_email);
                cmd.Parameters.AddWithValue("@team_name", team_name);
                cmd.Parameters.AddWithValue("@position", position);
                cmd.Parameters.AddWithValue("@jersey_number", jersey_number);
                cmd.Parameters.AddWithValue("@height", height);
                cmd.Parameters.AddWithValue("@weight", weight);
                cmd.Parameters.AddWithValue("@is_captain", is_captain);
                cmd.Parameters.AddWithValue("@picture_url", picture_url);

                replacePlayer.rows_affected = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                replacePlayer.result = e.Message;
            }

            return replacePlayer;
        }
    }
}
