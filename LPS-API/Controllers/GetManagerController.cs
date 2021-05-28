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
    public class GetManagerController : ControllerBase
    {
        private readonly ILogger<GetManagerController> _logger;

        public GetManagerController(ILogger<GetManagerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public GetManager Get(string account_email, string team_name, string picture_url)
        {
            var getManager = new GetManager();
            getManager.managers = new List<Dictionary<string, object>>();

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM manager";

                var args = new Dictionary<string, object>()
                {
                    { "account_email", account_email },
                    { "team_name", team_name },
                    { "picture_url", picture_url },
                };
                cmd.AddWhereClause(args);

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
