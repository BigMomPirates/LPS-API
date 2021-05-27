using LPS_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;

namespace LpsApi.Controllers
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
        public DeletePlayer Get(string id)
        {
            var deletePlayer = new DeletePlayer();

            try
            {
                using MySqlConnection conn = Program.GetMySqlConnection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM player WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);

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
