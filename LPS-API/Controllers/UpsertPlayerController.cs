using LPS_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;

namespace LpsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpsertPlayerController : ControllerBase
    {
        private readonly ILogger<UpsertPlayerController> _logger;

        public UpsertPlayerController(ILogger<UpsertPlayerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public UpsertPlayer Get(string team_id, string name, byte age, string birthdate, string position, string jersey_number, string height, string weight, string picture_url, string is_captain, string is_subcaptain)
        {
            var upsertPlayer = new UpsertPlayer();

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText =
                    "REPLACE INTO team (name, age, birthdate, position, jersey_number, height, weight, picture_url, is_captain, is_subcaptain, team_id) " +
                    "VALUES(@name, @age, @birthdate, @position, @jersey_number, @height, @weight, @picture_url, @is_captain, @is_subcaptain, @team_id)";

                cmd.Parameters.AddWithValue("@team_id", team_id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@birthdate", birthdate);
                cmd.Parameters.AddWithValue("@position", position);
                cmd.Parameters.AddWithValue("@jersey_number", jersey_number);
                cmd.Parameters.AddWithValue("@height", height);
                cmd.Parameters.AddWithValue("@weight", weight);
                cmd.Parameters.AddWithValue("@picture_url", picture_url);
                cmd.Parameters.AddWithValue("@is_captain", is_captain);
                cmd.Parameters.AddWithValue("@is_subcaptain", is_subcaptain);

                upsertPlayer.rows_affected = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                upsertPlayer.result = e.Message;
            }

            return upsertPlayer;
        }
    }
}
