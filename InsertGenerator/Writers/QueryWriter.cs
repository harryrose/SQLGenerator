using InsertGenerator.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertGenerator.Writers
{
    public class QueryWriter
    {
        public void Write<T>(T query, TextWriter writer) where T : AbstractQuery
        {
            dynamic tmp = query as InsertQuery ?? query as DeleteQuery ?? query as UpdateQuery ?? (AbstractQuery)query;

            DoWrite(tmp, writer);
        }

        protected virtual void DoWrite(InsertQuery query, TextWriter writer)
        {
            throw new NotImplementedException();
        }

        protected virtual void DoWrite(UpdateQuery query, TextWriter writer)
        {
            throw new NotImplementedException();
        }

        protected virtual void DoWrite(DeleteQuery query, TextWriter writer)
        {
            throw new NotImplementedException();
        }

        protected void DoWrite(AbstractQuery query, TextWriter writer)
        {
            throw new Exception(string.Format("Cannot write invalid query type '{0}'.", query.GetType().FullName));
        }
    }
}
