using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace LPS_API
{
    public static class Extension
    {
        public static void AddWhereClause(this MySqlCommand cmd, Dictionary<string, object> args)
        {
            foreach (KeyValuePair<string, object> arg in args)
            {
                if (arg.Value == null)
                {
                    args.Remove(arg.Key);
                }
            }

            if (args.Count > 0)
            {
                cmd.CommandText += " WHERE ";
                int index = 0;

                foreach (KeyValuePair<string, object> arg in args)
                {
                    index++;

                    cmd.CommandText += string.Format("{0} = @{0}", arg.Key);
                    cmd.Parameters.AddWithValue("@" + arg.Key, arg.Value);

                    if (index < args.Count)
                    {
                        cmd.CommandText += " AND ";
                    }
                }
            }
        }
    }
}
