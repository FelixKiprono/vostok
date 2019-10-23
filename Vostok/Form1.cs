using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Vostok
{
    public partial class Form1 : Form
    {
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




        public Form1()
        {
            InitializeComponent();
        }
        void FetchDBA(String DB)
        {
            DATABASE_A = DB;
            TABLES_A = new List<string>();
            //connect to database
            MySqlConnection con = new MySqlConnection("server=localhost;username=root;password=;database=" + DATABASE_A + ";");
            con.Open();
            CON_A = con;
            MySqlCommand cmd = new MySqlCommand("SHOW TABLES", con);
            MySqlDataReader read = cmd.ExecuteReader();
            int j = 0;
            while (read.Read())
            {

                TABLES_A.Add(read.GetString(0));
                j++;
            }
            //con.Close();
            //
            //loop through ables generting columns 
            COLUMNS_A = new List<string>();
            foreach (String table in TABLES_A)
            {
                foreach (String n in columns(table, DATABASE_A))
                {
                    COLUMNS_A.Add(n);
                }
            }

        }
        void FetchDBB(String DB)
        {
            DATABASE_B = DB;
            TABLES_B = new List<string>();
            //connect to database
            MySqlConnection con = new MySqlConnection("server=localhost;username=root;password=;database=" + DATABASE_B + ";");
            con.Open();
            CON_B = con;
            MySqlCommand cmd = new MySqlCommand("SHOW TABLES", con);
            MySqlDataReader read = cmd.ExecuteReader();
            int j = 0;
            while (read.Read())
            {

                TABLES_B.Add(read.GetString(0));
                j++;
            }
            con.Close();
            //
            //loop through ables generting columns 
            COLUMNS_B = new List<string>();
            foreach (String table in TABLES_B)
            {
                foreach (String n in columns(table, DATABASE_B))
                {
                    COLUMNS_B.Add(n);
                }
            }

        }
        //public List<String> CompareList(List<String> ls1,List<String> ls2)
        //{
        //    var firstNotSecond = ls1.Except(ls2).ToList();
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            TABLES_A.Sort();
            TABLES_B.Sort();

            foreach (String n in TABLES_A)
            {
                listBox1.Items.Add(n);
            }
            foreach (String n in TABLES_B)
            {
                listBox2.Items.Add(n);
            }


            if (TABLES_A.Count > TABLES_B.Count)
            {
                var firstNotSecond = TABLES_A.Except(TABLES_B).ToList();
                foreach (String n in firstNotSecond)
                {
                    listBox3.Items.Add(n);
                }
            }
            //add columns 
            foreach (String n in COLUMNS_A)
            {
                 textBox1.Text+=Environment.NewLine+ n;
            }
            foreach (String n in COLUMNS_B)
            {
                textBox2.Text += Environment.NewLine + n;
            }
           
        }
        List<String> columns(String tbl, String DB)
        {
            List<String> cols = new List<string>();
            //connect to database
            MySqlConnection con = new MySqlConnection("server=localhost;username=root;password=;database=" + DB + ";");
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SHOW COLUMNS FROM `" + tbl + "`;", con);
            MySqlDataReader read = cmd.ExecuteReader();
            int j = 0;
            while (read.Read())
            {
                cols.Add(read.GetString(0));
            }
            con.Close();
            return cols;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FetchDBA("hospital");
            FetchDBB("hospitalb");
        }

        private void listBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            //get columns of the table
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
             MySqlCommand cmd = new MySqlCommand("SHOW COLUMNS FROM `" + listBox1.SelectedItem.ToString() + "`;", CON_A);
            MySqlDataReader read = cmd.ExecuteReader();
            int j = 0;
            while (read.Read())
            {
                textBox1.Text+=Environment.NewLine+ read.GetString(0);
            }
        }
    }
}
