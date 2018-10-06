using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Vostok.Mysql
{
    public class SQL
    {

        public String CONNECTION = null;
        public MySqlConnection con;
        public SQL(string connectionpath)
        {
            this.CONNECTION = connectionpath;
            con = new MySqlConnection(this.CONNECTION);
            try
            {
                con.Open();
            }
            catch (MySqlException)
            {

            }

        }

        public Boolean ExecuteQuery(String sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, this.con);
            int res = cmd.ExecuteNonQuery();
            con.Close();
            if (res != 1)
            {
                return false;
            }

            return false;

        }
        public int Execute(String sql)
        {

            MySqlCommand cmd = new MySqlCommand(sql, this.con);
            MySqlDataReader read = cmd.ExecuteReader();
            int t = 0;
            if (read.Read())
            {
                t = int.Parse(read.GetString(0));
            }
            else
            {
                t = 0;
            }
            con.Close();
            return t;
        }


    }
}
