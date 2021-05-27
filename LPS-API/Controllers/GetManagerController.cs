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
    public class GetManagerController : ControllerBase
    {
        private readonly ILogger<GetManagerController> _logger;

        public GetManagerController(ILogger<GetManagerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public GetManager Get(string id, string team_id, string name, string birthdate, string picture_url)
        {
            var getManager = new GetManager();
            getManager.managers = new List<Dictionary<string, object>>();

            try
            {
                var args = new Dictionary<string, object>();
                
                args.Add("id", id);
                args.Add("team_id", team_id);
                args.Add("name", name);
                args.Add("birthdate", birthdate);
                args.Add("picture_url", picture_url);

                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM manager";
                cmd.AddWhereClause(args);

                Console.WriteLine(cmd.CommandText);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Dictionary<string, object> manager = Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue);
                    getManager.managers.Add(manager);
                }

                getManager.rows = getManager.managers.Count();
            }
            catch (Exception e)
            {
                getManager.result = e.Message;
            }

            return getManager;
        }
    }
}
