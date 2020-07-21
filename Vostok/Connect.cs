using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vostok.Models;

namespace Vostok
{
    public partial class Connect : Form
    {

        public DatabaseControllers dbcontroller;

        public Dictionary<String, List<TableModel>> SCHEMA_DETAILS_A;
        public Dictionary<String, List<TableModel>> SCHEMA_DETAILS_B;

        public String CONSTRSOURCE;

        public String TARGECONSTRSOURCE;

        public MySqlConnection CON_A;
        public String DATABASE_A = "";
        public List<String> TABLES_A;
        public List<String> COLUMNS_A;
        public List<String> DATA_TYPES_A;
        public List<String> LENGTH_A;

        public MySqlConnection CON_B;
        public String DATABASE_B = "";
        public List<String> TABLES_B;
        public List<String> COLUMNS_B;
        public List<String> DATA_TYPES_B;
        public List<String> LENGTH_B;

        public Connect()
        {
            InitializeComponent();
        }


        Dictionary<String, String> columns(String tbl, String DB)
        {
            Dictionary<String, String> cols = new Dictionary<string, string>();
            //connect to database    
            MySqlConnection con = new MySqlConnection(CONSTRSOURCE);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SHOW COLUMNS FROM `" + tbl + "`;", con);
            MySqlDataReader read = cmd.ExecuteReader();
            int j = 0;
            while (read.Read())
            {
                cols.Add(read.GetString(0), read.GetString(1));
            }
            con.Close();
            return cols;
        }
        void ConnectSource(String server, String username, String Password)
        {
            CONSTRSOURCE = "server=" + server + ";username=" + username + ";password=" + Password + ";";
            cbodatabase.Items.Clear();
            TABLES_A = new List<string>();
            //connect to database
            MySqlConnection con = new MySqlConnection(CONSTRSOURCE);
            con.Open();
            CON_A = con;
            //load databases
            MySqlCommand cmd = new MySqlCommand("SHOW DATABASES", con);
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                cbodatabase.Items.Add(read.GetString(0));

            }
            con.Close();

        }
        void ConnectTarget(String server, String username, String Password)
        {
            TARGECONSTRSOURCE = "server=" + server + ";username=" + username + ";password=" + Password + ";";

            TABLES_B = new List<string>();
            //connect to database
            MySqlConnection con = new MySqlConnection(CONSTRSOURCE);
            con.Open();
            CON_B = con;
            MySqlCommand cmd = new MySqlCommand("SHOW DATABASES;", con);
            MySqlDataReader read = cmd.ExecuteReader();
            int j = 0;
            cbodb2.Items.Clear();
            while (read.Read())
            {
                cbodb2.Items.Add(read.GetString(0));
                j++;
            }
            con.Close();
        }
        private void cbodatabase_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbodatabase_Click(object sender, EventArgs e)
        {
            ConnectSource(txtserver.Text, txtusername.Text, txtpass.Text);
        }

        private void cbodatabase_SelectionChangeCommitted(object sender, EventArgs e)
        {

            String database = cbodatabase.SelectedItem.ToString();
            DATABASE_A = database;
            CONSTRSOURCE = CONSTRSOURCE + "database=" + cbodatabase.SelectedItem.ToString() + ";";
            //open new connection

            /*MySqlConnection con = new MySqlConnection(CONSTRSOURCE + "database=" + database + ";");
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SHOW TABLES;", con);
            MySqlDataReader read = cmd.ExecuteReader();
            int j = 0;
            SCHEMA_DETAILS_A = new Dictionary<string, List<TableModel>>();
            List<TableModel> tbls = new List<TableModel>();
            while (read.Read())
            {
                String tbl = read.GetString(0);
                //SCHEMA_DETAILS_A.Add(tbl, FetchColumns(database, tbl));
                TABLES_A.Add(tbl);
                j++;
            }
            con.Close();*/

        }
        List<TableModel> FetchColumns(String db, String tbl)
        {

            List<TableModel> datacolumns = new List<TableModel>();

            foreach (KeyValuePair<String, String> n in columns(tbl, db))
            {

                datacolumns.Add(new TableModel() { COLUMN = n.Key, TYPE = n.Value });

            }
            return datacolumns;
        }
        private void bunifuDropdown1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {



        }

        private void cbodb2_Click(object sender, EventArgs e)
        {
            ConnectTarget(server2.Text, username2.Text, password2.Text);
        }

        private void cbodb2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DATABASE_B = cbodb2.SelectedItem.ToString();
            TARGECONSTRSOURCE = TARGECONSTRSOURCE + "database=" + cbodb2.SelectedItem.ToString() + ";";
            //open new connection
            /* MySqlConnection con = new MySqlConnection(CONSTRSOURCE + "database=" + cbodb2.SelectedItem.ToString() + ";");
             con.Open();
             MySqlCommand cmd = new MySqlCommand("SHOW TABLES", con);
             MySqlDataReader read = cmd.ExecuteReader();
             int j = 0;
             SCHEMA_DETAILS_B = new Dictionary<string, List<TableModel>>();
             while (read.Read())
             {
                 String tbl = read.GetString(0);
                 SCHEMA_DETAILS_B.Add(tbl, FetchColumns(DATABASE_B, tbl));
                 TABLES_B.Add(tbl);
                 j++;
             }
             con.Close();*/
        }
        public async Task<JObject> StartComp()
        {
            JObject data = null;
            await Task.Run(() =>
             {
                 this.TARGECONSTRSOURCE = "server=" + server2.Text + ";username=" + username2.Text + ";password=" + password2.Text + ";database=" + cbodb2.SelectedItem.ToString() + ";";
                 data = new JObject();
                 data.Add("SERVER_A", txtserver.DefaultText);
                 data.Add("SERVER_B", server2.DefaultText);
                 data.Add("TARGETCONNECTION", this.TARGECONSTRSOURCE);
                 data.Add("SOURCECONNECTION", this.CONSTRSOURCE);
             });
            return data;
        }
        private async void bunifuButton1_Click_1(object sender, EventArgs e)
        {



            if (String.IsNullOrEmpty(cbodatabase.Text) && String.IsNullOrEmpty(cbodb2.Text))
            {
                MessageBox.Show("Please Selecte Source Database and Target Database ", "Vostok", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (this.CONSTRSOURCE == null || this.CONSTRSOURCE == String.Empty)
            {
                MessageBox.Show("Connection Settings Not Set for Source?");
                return;
            }
            //fetch database items for database A
            DatabaseControllers dbcontrollera = new DatabaseControllers(this.CONSTRSOURCE);
            List<DatabaseTableModel> tablesdba = dbcontrollera.FetchTables(DATABASE_A);
            DatabaseModel DBA = new DatabaseModel();
            DBA.DatabaseName = DATABASE_A;
            DBA.Tables = tablesdba;
            var dt = DBA.Tables;

            if (this.TARGECONSTRSOURCE == null || this.TARGECONSTRSOURCE == String.Empty)
            {
                MessageBox.Show("Connection Settings Not Set for Target?");
                return;
            }

            //fetch database items for database A
            DatabaseControllers dbcontrollerb = new DatabaseControllers(this.TARGECONSTRSOURCE);
            List<DatabaseTableModel> tablesdbb = dbcontrollerb.FetchTables(DATABASE_B);
            DatabaseModel DBB = new DatabaseModel();
            DBB.DatabaseName = DATABASE_B;
            DBB.Tables = tablesdbb;


            //process window
            Process p = new Process(DBA, DBB);
            p.ShowDialog();







            //JObject data = await StartComp();
            //this.Hide();
            // new Process(data, DATABASE_A, DATABASE_B, SCHEMA_DETAILS_A, SCHEMA_DETAILS_B).ShowDialog();
            //Analysis anl = new Analysis(data, DATABASE_A, DATABASE_B, SCHEMA_DETAILS_A, SCHEMA_DETAILS_B);
            //anl.ShowDialog();

        }

        private void cbodb2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
