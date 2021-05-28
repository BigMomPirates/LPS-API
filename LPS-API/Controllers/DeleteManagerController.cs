using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace LPS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteManagerController : ControllerBase
    {
        private readonly ILogger<DeleteManagerController> _logger;

        public DeleteManagerController(ILogger<DeleteManagerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public DeleteManager Get(string account_email)
        {
            var deleteManager = new DeleteManager();

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM manager";

                var args = new Dictionary<string, object>()
                {
                    { "account_email", account_email }
                };
                cmd.AddWhereClause(args);

                deleteManager.rows_affected = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                deleteManager.result = e.Message;
            }

            return deleteManager;
        }
    }
}
