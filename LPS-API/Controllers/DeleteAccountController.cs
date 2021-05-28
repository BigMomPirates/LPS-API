using LPS_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace LPS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteAccountController : ControllerBase
    {
        private readonly ILogger<DeleteAccountController> _logger;

        public DeleteAccountController(ILogger<DeleteAccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public DeleteAccount Get(string email)
        {
            var deleteAccount = new DeleteAccount();

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM account";

                var args = new Dictionary<string, object>()
                {
                    { "email", email }
                };
                cmd.AddWhereClause(args);

                deleteAccount.rows_affected = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                deleteAccount.result = e.Message;
            }

            return deleteAccount;
        }
    }
}
