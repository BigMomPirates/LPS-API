using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;

namespace LPS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReplaceTeamController : ControllerBase
    {
        private readonly ILogger<ReplaceTeamController> _logger;

        public ReplaceTeamController(ILogger<ReplaceTeamController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ReplaceTeam Get(string name, string location, string title, string description, string color, string home, string logo_url, string grouppicture_url, string homepicture_url)
        {
            var replaceTeam = new ReplaceTeam();

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText =
                    "REPLACE INTO team (name, location, title, description, color, home, logo_url, grouppicture_url, homepicture_url) " +
                    "VALUES(@name, @location, @title, @description, @color, @home, @logo_url, @grouppicture_url, @homepicture_url)";

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@color", color);
                cmd.Parameters.AddWithValue("@home", home);
                cmd.Parameters.AddWithValue("@logo_url", logo_url);
                cmd.Parameters.AddWithValue("@grouppicture_url", grouppicture_url);
                cmd.Parameters.AddWithValue("@homepicture_url", homepicture_url);

                replaceTeam.rows_affected = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                replaceTeam.result = e.Message;
            }

            return replaceTeam;
        }
    }
}
