using LPS_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;

namespace LpsApi.Controllers
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
        public DeleteManager Get(string id)
        {
            var deleteManager = new DeleteManager();

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM manager WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);

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
