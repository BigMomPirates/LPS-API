using LPS_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;

namespace LpsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpsertTeamController : ControllerBase
    {
        private readonly ILogger<UpsertTeamController> _logger;

        public UpsertTeamController(ILogger<UpsertTeamController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public UpsertTeam Get(string name, string location, string title, string description, string color, string home, string logo_url, string grouppicture_url, string homepicture_url)
        {
            string result = "OK";

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

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return new UpsertTeam()
            {
                result = result
            };
        }
    }
}
