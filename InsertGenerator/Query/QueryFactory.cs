using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertGenerator.Query
{
    public class QueryFactory : GenericFactory<QueryFactory,string,AbstractQuery>
    {
        public QueryFactory()
        {
            Add<DeleteQuery>("DELETE");
            Add<InsertQuery>("INSERT");
            Add<UpdateQuery>("UPDATE");
        }
    }
}
