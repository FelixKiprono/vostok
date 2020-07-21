using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vostok.Models
{
    public class DatabaseModel
    {
        public String DatabaseName { get; set; }

        public String StoredProcdures { get; set; }


        public String Functions { get; set; }

        public String Triggers { get; set; }

        public List<DatabaseTableModel> Tables { get; set; }



    }
}
