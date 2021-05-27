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
    public class GetTeamController : ControllerBase
    {
        private readonly ILogger<GetTeamController> _logger;

        public GetTeamController(ILogger<GetTeamController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public GetTeam Get(string id, string name, string location, string title, string description, string color, string home, string logo_url, string grouppicture_url, string homepicture_url)
        {
            var getTeam = new GetTeam();
            getTeam.teams = new List<Dictionary<string, object>>();

            try
            {
                var args = new Dictionary<string, object>();
                
                args.Add("id", id);
                args.Add("name", name);
                args.Add("location", location);
                args.Add("title", title);
                args.Add("description", description);
                args.Add("color", color);
                args.Add("home", home);
                args.Add("logo_url", logo_url);
                args.Add("grouppicture_url", grouppicture_url);
                args.Add("homepicture_url", homepicture_url);

                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM team";
                cmd.AddWhereClause(args);

                MySqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    Dictionary<string, object> team = Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue);
                    getTeam.teams.Add(team);
                }

                getTeam.rows = getTeam.teams.Count();
            }
            catch (Exception e)
            {
                getTeam.result = e.Message;
            }

            return getTeam;
        }
    }
}
