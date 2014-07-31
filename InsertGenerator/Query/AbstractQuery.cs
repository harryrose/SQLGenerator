using InsertGenerator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertGenerator.Query
{
    public abstract class AbstractQuery
    {
        public List<Row> Rows { get { return rows; } }
        public string Table { get; set; }
        
        public virtual bool KeysRequired { get { return false; } }
        public virtual List<string> Keys { get { throw new Exception("Attempted to access keys of a non-selective query."); } }

        private List<Row> rows = new List<Row>();
    }
}
