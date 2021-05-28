using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LPS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetPlayerController : ControllerBase
    {
        private readonly ILogger<GetPlayerController> _logger;

        public GetPlayerController(ILogger<GetPlayerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public GetPlayer Get(string account_email, string team_name, string position, string jersey_number, string height, string weight, string is_captain, string picture_url)
        {
            var GetPlayer = new GetPlayer();
            GetPlayer.players = new List<Dictionary<string, object>>();

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM player";

                var args = new Dictionary<string, object>()
                {
                    { "account_email", account_email },
                    { "team_name", team_name },
                    { "position", position },
                    { "jersey_number", jersey_number },
                    { "height", height },
                    { "weight", weight },
                    { "is_captain", is_captain },
                    { "picture_url", picture_url },
                };
                cmd.AddWhereClause(args);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Dictionary<string, object> player = Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue);
                    GetPlayer.players.Add(player);
                }

                GetPlayer.rows = GetPlayer.players.Count();
            }
            catch (Exception e)
            {
                GetPlayer.result = e.Message;
            }

            return GetPlayer;
        }
    }
}
