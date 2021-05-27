using System.Collections.Generic;

namespace LpsApi
{
    public class GetPlayer
    {
        public string result { get; set; } = "OK";
        public int rows { get; set; }
        public List<Dictionary<string, object>> players { get; set; }
    }
}
