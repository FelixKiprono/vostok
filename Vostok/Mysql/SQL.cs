using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            MySqlConnection con = new MySqlConnection(this.CONNECTION);
            con.Open();
            this.con = con;


        }

        public static MySqlConnection NewConnection(String path)
        {
            Console.WriteLine(path);
            MySqlConnection con = new MySqlConnection(path);
            con.Open();

            return con;
        }

        public Boolean ExecuteQuery(String sql)
        {
            if (this.con.State == System.Data.ConnectionState.Open)
            {
                MessageBox.Show("Opened");
            }
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

        //return list
        public List<String> ExecuteQuery(String sql, int index)
        {
            List<String> data = new List<string>();

            MySqlCommand cmd = new MySqlCommand(sql, this.con);
            MySqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    data.Add(read.GetString(index));
                }
            }

            con.Close();

            return data;
        }


    }
}
