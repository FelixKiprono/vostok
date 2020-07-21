using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Vostok.Models;
using Vostok.Mysql;
using Vostok.Properties;

namespace Vostok
{
    public partial class Analysis : Form
    {

        public String TARGETCONNECTIONSTRING;
        public String SOURCECONNECTIONSTRING;

        public Dictionary<String, List<TableModel>> SCHEMA_DETAILS_A;
        public Dictionary<String, List<TableModel>> SCHEMA_DETAILS_B;
        public String DATABASE_A;
        public String DATABASE_B;
        public String SERVER_A;
        public String SERVER_B;
        public Analysis(JObject infor, String dba, String dbb, Dictionary<String, List<TableModel>> SCHEMA_DETAILS_A, Dictionary<String, List<TableModel>> SCHEMA_DETAILS_B)
        {
            InitializeComponent();
            this.SCHEMA_DETAILS_A = SCHEMA_DETAILS_A;
            this.SCHEMA_DETAILS_B = SCHEMA_DETAILS_B;
            this.DATABASE_A = dba;
            this.DATABASE_B = dbb;


            this.SERVER_A = infor["SERVER_A"].ToString();
            this.SERVER_B = infor["SERVER_B"].ToString();

            this.TARGETCONNECTIONSTRING = infor["TARGETCONNECTION"].ToString();
            this.SOURCECONNECTIONSTRING = infor["SOURCECONNECTION"].ToString();

            Init();
            Populate();
            loadSchemas();
        }
        void Init()
        {
            database1.Text = DATABASE_A;
            database2.Text = DATABASE_B;

            server1.Text = SERVER_A;
            server2.Text = SERVER_B;
        }
        void Populate()
        {
            tablesa.Text = "[" + SCHEMA_DETAILS_A.Keys.Count.ToString() + "] Total Tables";
            tablesb.Text = "[" + SCHEMA_DETAILS_B.Keys.Count.ToString() + "] Total Tables";
        }
        int CountTableColumns(String CONN, String DB, String table)
        {
            int total = 0;
            SQL sql = new SQL(CONN);
            total = sql.Execute("SELECT count(*) FROM information_schema.columns WHERE table_schema='" + DB + "' AND table_name = '" + table + "'");
            return total;
        }
        void loadSchemas()
        {
            List<String> TABLES_A = new List<string>();
            List<String> TABLES_B = new List<string>();
            //load schema A

            foreach (String key in SCHEMA_DETAILS_A.Keys)
            {
                TABLES_A.Add(key);
            }

            foreach (String key in SCHEMA_DETAILS_B.Keys)
            {
                TABLES_B.Add(key);
            }


            TABLES_A.Sort();
            TABLES_B.Sort();
            //find the common tables
            var result = TABLES_A.Intersect(TABLES_B);
            var sourceonly = TABLES_A.Except(TABLES_B);
            var targetonly = TABLES_B.Except(TABLES_A);
            foreach (String table in result)
            {
                schema_a.Rows.Add(true, DATABASE_A, table, CountTableColumns(this.SOURCECONNECTIONSTRING, this.DATABASE_A, table));
                schema_b.Rows.Add(true, DATABASE_B, table, CountTableColumns(this.TARGETCONNECTIONSTRING, this.DATABASE_B, table));
            }

            foreach (String table in sourceonly)
            {
                sourcetables.Rows.Add(true, table, CountTableColumns(this.SOURCECONNECTIONSTRING, this.DATABASE_A, table));
            }
            foreach (String table in targetonly)
            {
                targettables.Rows.Add(true, table, CountTableColumns(this.TARGETCONNECTIONSTRING, this.DATABASE_B, table));
            }

            List<String> tblanotb = new List<string>();
            List<String> tblbnota = new List<string>();
            tblanotb = TABLES_A.Except(TABLES_B).ToList();
            tblbnota = TABLES_B.Except(TABLES_A).ToList();

        }
        int getColumnCount(string key, Dictionary<string, List<TableModel>> SCHEMA)
        {
            int COUNT = 0;
            foreach (KeyValuePair<String, List<TableModel>> n in SCHEMA)
            {
                COUNT = n.Value.Count();
            }
            return COUNT;

        }
        List<String> GetColumns(string key, Dictionary<string, List<TableModel>> SCHEMA)
        {
            List<String> columns = new List<string>();
            foreach (KeyValuePair<String, List<TableModel>> n in SCHEMA)
            {
                foreach (TableModel tbl in n.Value)
                {
                    columns.Add(tbl.COLUMN);
                }

            }

            return columns;
        }


        private void Analysis_Load(object sender, EventArgs e)
        {

        }
        void LoadColumns(DataGridViewCellEventArgs e)
        {
            StringBuilder STATEMENT = new StringBuilder();
            if (e.ColumnIndex == 2)
            {
                //clear
                acolumns.Rows.Clear();
                String tbl = schema_a.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                STATEMENT.AppendLine("CREATE TABLE IF NOT EXISTS  `" + tbl + "`");
                STATEMENT.AppendLine("(");
                int i = 0;
                foreach (KeyValuePair<String, List<TableModel>> Tables in SCHEMA_DETAILS_A)
                {
                    if (tbl == Tables.Key)
                    {
                        foreach (TableModel tbls in Tables.Value)
                        {
                            acolumns.Rows.Add(tbls.COLUMN);
                            STATEMENT.Append("`" + tbls.COLUMN + "` " + tbls.TYPE + "");
                            i++;
                            if (i != Tables.Value.Count)
                            {
                                STATEMENT.AppendLine(",");
                            }
                        }
                    }
                }
                //generate sql statements


                STATEMENT.AppendLine("); ");

                sourcesql.Text = STATEMENT.ToString();
            }

        }
        private void schema_a_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadColumns(e);
        }

        private void schema_b_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            StringBuilder STATEMENT = new StringBuilder();
            if (e.ColumnIndex == 2)
            {
                bcolumns.Rows.Clear();
                String tbl = schema_b.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                STATEMENT.AppendLine("CREATE TABLE IF NOT EXISTS  `" + tbl + "`");
                STATEMENT.AppendLine("(");
                int i = 0;
                foreach (KeyValuePair<String, List<TableModel>> Tables in SCHEMA_DETAILS_A)
                {
                    if (tbl == Tables.Key)
                    {
                        foreach (TableModel tbls in Tables.Value)
                        {
                            bcolumns.Rows.Add(tbls.COLUMN);

                            STATEMENT.Append("`" + tbls.COLUMN + "` " + tbls.TYPE + "");
                            i++;
                            if (i != Tables.Value.Count)
                            {
                                STATEMENT.AppendLine(",");
                            }
                        }
                    }
                }
                //generate sql statements


                STATEMENT.AppendLine("); ");

                targetsql.Text = STATEMENT.ToString();
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (panel1.Height == 319)
            {
                bunifuImageButton1.Image = Resources.Plus_32px;
                panel1.Height = 39;

            }
            else
            {
                bunifuImageButton1.Image = Resources.Minus_32px;
                panel1.Height = 319;


            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (sourcepanel.Height == 319)
            {
                bunifuImageButton2.Image = Resources.Plus_32px;
                sourcepanel.Height = 39;

            }
            else
            {
                bunifuImageButton2.Image = Resources.Minus_32px;
                sourcepanel.Height = 319;

            }
        }
        void LoadSourceColumns(DataGridViewCellEventArgs e)
        {
            StringBuilder STATEMENT = new StringBuilder();

            if (e.ColumnIndex == 2)
            {
                sourcecolumns.Rows.Clear();
                String tbl = sourcetables.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                STATEMENT.AppendLine("CREATE TABLE IF NOT EXISTS  `" + tbl + "`");
                STATEMENT.AppendLine("(");
                int i = 0;

                foreach (KeyValuePair<String, List<TableModel>> Tables in SCHEMA_DETAILS_A)
                {
                    if (tbl == Tables.Key)
                    {
                        foreach (TableModel tbls in Tables.Value)
                        {
                            sourcecolumns.Rows.Add(tbls.COLUMN);

                            STATEMENT.Append("`" + tbls.COLUMN + "` " + tbls.TYPE + "");
                            i++;
                            if (i != Tables.Value.Count)
                            {
                                STATEMENT.AppendLine(",");
                            }
                        }
                    }
                }
                STATEMENT.AppendLine("); ");

                sourcesql.Text = STATEMENT.ToString();
            }
        }
        private void sourcetables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadSourceColumns(e);
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            if (targetpnl.Height == 319)
            {
                bunifuImageButton3.Image = Resources.Plus_32px;
                targetpnl.Height = 39;

            }
            else
            {
                bunifuImageButton3.Image = Resources.Minus_32px;
                targetpnl.Height = 319;

            }
        }

        private void targettables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                targetcolumns.Rows.Clear();
                String tbl = targettables.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                foreach (KeyValuePair<String, List<TableModel>> Tables in SCHEMA_DETAILS_B)
                {
                    if (tbl == Tables.Key)
                    {
                        foreach (TableModel tbls in Tables.Value)
                        {
                            targetcolumns.Rows.Add(tbls.COLUMN);
                        }
                    }
                }
            }
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void title_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            if (pnlsql.Height == 373)
            {
                bunifuImageButton5.Image = Resources.Plus_32px;
                pnlsql.Height = 39;

            }
            else
            {
                bunifuImageButton5.Image = Resources.Minus_32px;
                pnlsql.Height = 373;

            }
        }

        private void btnmerge_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.TARGETCONNECTIONSTRING))
            {
                MessageBox.Show("PLEASE CHECK YOU DATABASE CONNECTION FROM CONNECTION WINDOW?");
                return;
            }
            SQL sql = new SQL(this.TARGETCONNECTIONSTRING);
            sql.ExecuteQuery(sourcesql.Text);

            MessageBox.Show("Successfully Merged the changes to Database : " + DATABASE_B);


        }

        private void schema_a_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadColumns(e);
        }

        private void sourcetables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadSourceColumns(e);
        }
    }
}
