using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vostok.Models
{
    public class DatabaseTableModel
    {
        public String TableName { get; set; }
        public List<ColumnModel> Columns { get; set; }

    }
}
