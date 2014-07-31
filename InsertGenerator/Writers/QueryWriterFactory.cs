using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertGenerator.Writers
{
    public class QueryWriterFactory : GenericFactory<QueryWriterFactory,string,QueryWriter>
    {
        public static Singleton<QueryWriterFactory> Factory = new Singleton<QueryWriterFactory>();

        public IEnumerable<string> Extensions
        {
            get { return extensions.Keys; }
        }

        public QueryWriterFactory()
        {
            Add<TSQLQueryWriter>("TSQL",".sql");
        }

        public override void Add<T>(string name)
        {
            throw new Exception("Must specify an extension parameter.");
        }

        public virtual void Add<T>(string name, string extension) where T : QueryWriter, new()
        {
            base.Add<T>(name);
            extensions[name] = extension;
        }

        public virtual string GetExtension(string name)
        {
            if (!extensions.ContainsKey(name))
            {
                throw new Exception(string.Format("No such extension - {0}", name));
            }

            return extensions[name];
        }

        Dictionary<string, string> extensions = new Dictionary<string,string>();
    }
}
