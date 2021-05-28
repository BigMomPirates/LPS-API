using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace LPS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteTeamController : ControllerBase
    {
        private readonly ILogger<DeleteTeamController> _logger;

        public DeleteTeamController(ILogger<DeleteTeamController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public DeleteTeam Get(string name)
        {
            var deleteTeam = new DeleteTeam();

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM team";

                var args = new Dictionary<string, object>()
                {
                    { "name", name }
                };
                cmd.AddWhereClause(args);

                deleteTeam.rows_affected = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                deleteTeam.result = e.Message;
            }

            return deleteTeam;
        }
    }
}
