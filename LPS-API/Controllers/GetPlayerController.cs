using LPS_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LpsApi.Controllers
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
        public GetPlayer Get(string id, string team_id, string name, string birthdate, string picture_url, string position, string jersey_number, string height, string weight, string is_captain, string is_subcaptain)
        {
            var GetPlayer = new GetPlayer();
            GetPlayer.players = new List<Dictionary<string, object>>();

            try
            {
                var args = new Dictionary<string, object>();
                
                args.Add("id", id);
                args.Add("team_id", team_id);
                args.Add("name", name);
                args.Add("birthdate", birthdate);
                args.Add("picture_url", picture_url);
                args.Add("position", position);
                args.Add("jersey_number", jersey_number);
                args.Add("height", height);
                args.Add("weight", weight);
                args.Add("is_captain", is_captain);
                args.Add("is_subcaptain", is_subcaptain);

                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM player";
                cmd.AddWhereClause(args);

                Console.WriteLine(cmd.CommandText);

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
