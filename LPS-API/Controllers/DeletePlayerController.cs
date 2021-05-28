using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace LPS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeletePlayerController : ControllerBase
    {
        private readonly ILogger<DeletePlayerController> _logger;

        public DeletePlayerController(ILogger<DeletePlayerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public DeletePlayer Get(string account_email)
        {
            var deletePlayer = new DeletePlayer();

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM player";
                
                var args = new Dictionary<string, object>()
                {
                    { "account_email", account_email }
                };
                cmd.AddWhereClause(args);

                deletePlayer.rows_affected = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                deletePlayer.result = e.Message;
            }

            return deletePlayer;
        }
    }
}
