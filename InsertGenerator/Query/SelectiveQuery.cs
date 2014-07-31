using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertGenerator.Query
{
    public abstract class SelectiveQuery : AbstractQuery
    {
        public override List<string> Keys
        {
            get
            {
                return keyColumns;
            }
        }

        public override bool KeysRequired
        {
            get
            {
                return true;
            }
        }

        private List<string> keyColumns = new List<string>();
    }
}
