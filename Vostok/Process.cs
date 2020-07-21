using Newtonsoft.Json.Linq;
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
    public partial class Process : Form
    {
        public DatabaseModel DatabaseOne;
        public DatabaseModel DatabaseTwo;

        public Process(DatabaseModel dba, DatabaseModel dbb)
        {
            InitializeComponent();

            this.DatabaseOne = dba;
            this.DatabaseTwo = dbb;


        }
        async Task CompareDatabase()
        {



        }
        private void Process_Load(object sender, EventArgs e)
        {
            processtask.RunWorkerAsync();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
           
        }

        private async void processtask_DoWork(object sender, DoWorkEventArgs e)
        {

            //compare source db vs target
            List<DatabaseTableModel> TABLES_A = this.DatabaseOne.Tables;
            List<DatabaseTableModel> TABLES_B = this.DatabaseTwo.Tables;

            //
            List<DatabaseTableModel> sourceTables = new List<DatabaseTableModel>();
            List<DatabaseTableModel> targetTables = new List<DatabaseTableModel>();


            TABLES_A.Sort();
            TABLES_B.Sort();
            //find the common tables
            var result = TABLES_A.Intersect(TABLES_B);
            var sourceonly = TABLES_A.Except(TABLES_B);
            var targetonly = TABLES_B.Except(TABLES_A);


            int i = 0;
            this.Invoke((Action)delegate
            {
                progress.MaxValue = sourceonly.Count();
                foreach (var table in sourceonly)
                {
                    i = i + 1;
                    progress.Value = i;
                    lbltask.Text = "Fetching Source tables only";
                    sourceTables.Add(table);
                }

            });

            int j = 0;
            this.Invoke((Action)delegate
            {
                progress.MaxValue = targetonly.Count();
                foreach (var table in targetonly)
                {
                    j = j + 1;
                    progress.Value = j;
                    lbltask.Text = "Fetching target tables only";
                    targetTables.Add(table);
                }
            });

        }
    }
}
