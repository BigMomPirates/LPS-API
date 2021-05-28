using System.Collections.Generic;

namespace LPS_API
{
    public class GetAccount
    {
        public string result { get; set; } = "OK";
        public int rows { get; set; }
        public List<Dictionary<string, object>> accounts { get; set; }
    }
}
