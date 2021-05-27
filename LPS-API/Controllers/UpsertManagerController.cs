using LPS_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;

namespace LpsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpsertManagerController : ControllerBase
    {
        private readonly ILogger<UpsertManagerController> _logger;

        public UpsertManagerController(ILogger<UpsertManagerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public UpsertManager Get(string team_id, string name, string birthdate, string picture_url)
        {
            var upsertManager = new UpsertManager();

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText =
                    "REPLACE INTO manager (name, birthdate, picture_url, team_id) " +
                    "VALUES(@name, @birthdate, @picture_url, @team_id)";

                cmd.Parameters.AddWithValue("@team_id", team_id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@birthdate", birthdate);
                cmd.Parameters.AddWithValue("@picture_url", picture_url);

                upsertManager.rows_affected = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                upsertManager.result = e.Message;
            }

            return upsertManager;
        }
    }
}
