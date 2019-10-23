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
        private String TARGETCONNECTIONSTRING;
        private String SOURCECONNECTIONSTRING;

        private Dictionary<String, List<TableModel>> SCHEMA_DETAILS_A;
        private Dictionary<String, List<TableModel>> SCHEMA_DETAILS_B;
        private String DATABASE_A;
        private String DATABASE_B;
        private String SERVER_A;
        private String SERVER_B;

        public Process(JObject infor, String dba, String dbb, Dictionary<String, List<TableModel>> SCHEMA_DETAILS_A, Dictionary<String, List<TableModel>> SCHEMA_DETAILS_B)
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


        }
        void StartCompare()
        {


        }




        private void Process_Load(object sender, EventArgs e)
        {

        }
    }
}
