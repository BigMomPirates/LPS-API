using LPS_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;

namespace LpsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsertManagerController : ControllerBase
    {
        private readonly ILogger<InsertManagerController> _logger;

        public InsertManagerController(ILogger<InsertManagerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public InsertManager Get(string name, string birthdate, string picture_url, string team_id)
        {
            string result = "OK";

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText =
                    "INSERT INTO manager (name, birthdate, picture_url, team_id) " +
                    "VALUES(@name, @birthdate, @picture_url, @team_id)";

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@birthdate", birthdate);
                cmd.Parameters.AddWithValue("@picture_url", picture_url);
                cmd.Parameters.AddWithValue("@team_id", team_id);

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return new InsertManager()
            {
                result = result
            };
        }
    }
}
