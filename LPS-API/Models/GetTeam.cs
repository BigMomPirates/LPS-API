using System.Collections.Generic;

namespace LPS_API
{
    public class GetTeam
    {
        public string result { get; set; } = "OK";
        public int rows { get; set; }
        public List<Dictionary<string, object>> teams { get; set; }
    }
}
