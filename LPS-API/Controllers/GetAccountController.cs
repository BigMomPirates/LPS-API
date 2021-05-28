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
    public class GetAccountController : ControllerBase
    {
        private readonly ILogger<GetAccountController> _logger;

        public GetAccountController(ILogger<GetAccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public GetAccount Get(string email, string name, string password, string birthdate)
        {
            var getAccount = new GetAccount();
            getAccount.accounts = new List<Dictionary<string, object>>();

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM account";

                var args = new Dictionary<string, object>()
                {
                    { "email", email },
                    { "name", name },
                    { "password", password },
                    { "birthdate", birthdate },
                };
                cmd.AddWhereClause(args);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Dictionary<string, object> account = Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue);
                    getAccount.accounts.Add(account);
                }

                getAccount.rows = getAccount.accounts.Count();
            }
            catch (Exception e)
            {
                getAccount.result = e.Message;
            }

            return getAccount;
        }
    }
}
