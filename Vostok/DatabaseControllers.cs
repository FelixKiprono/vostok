using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vostok.Models;
using Vostok.Mysql;

namespace Vostok
{
    public class DatabaseControllers
    {
        public String ConnectionString = null;
        public DatabaseControllers(String Connection)
        {
            this.ConnectionString = Connection;
        }

        public List<DatabaseTableModel> FetchTables(String Database)
        {

            SQL db = new SQL(this.ConnectionString);
            List<String> tables = db.ExecuteQuery("USE " + Database + ";SHOW TABLES;", 0);

            List<ColumnModel> columnmodel = new List<ColumnModel>();
            DatabaseTableModel tableModel = new DatabaseTableModel();
            List<DatabaseTableModel> tablesdata = new List<DatabaseTableModel>();

            foreach (var tbl in tables)
            {

                tableModel.TableName = tbl;
                //fetch columns based on the table
                MySqlCommand cmd = new MySqlCommand("USE " + Database + ";SHOW COLUMNS FROM `" + tbl + "`;", SQL.NewConnection(this.ConnectionString));
                MySqlDataReader read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        columnmodel.Add(new ColumnModel()
                        {
                            COLUMN = read.GetString("Field"),
                            KEY = read.GetString("Key"),
                            TYPE = read.GetString("Type"),
                            EXTRA = read.GetString("Extra")

                        });

                    }
                    tableModel.Columns = columnmodel;
                    tablesdata.Add(tableModel);
                }
            }

            return tablesdata;
        }


    }
}
