using InsertGenerator.Data;
using InsertGenerator.Query;
using InsertGenerator.Writers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InsertGenerator
{


    public class Job
    {
        public string ConnectionString { get; private set; }
        public string Sql { get; private set; }
        public string DestinationTable { get; private set; }
        public string DestinationFilename { get; private set; }

        public Job(string connectionString, string dstTable, string dstFile, string sql,
            Func<QueryWriter> createWriter,
            Func<AbstractQuery> createQuery,
            Func<Job,IEnumerable<string>,IEnumerable<string>> OnKeysRequired,
            Action<Job,Exception> OnException,
            Action<Job> OnComplete = null)
        {
            this.ConnectionString = connectionString;
            this.DestinationTable = dstTable;
            this.Sql = sql;
            this.DestinationFilename = dstFile;

            this.createQueryWriter = createWriter;

            this.onComplete = OnComplete;
            this.onException = OnException;
            this.onKeysRequired = OnKeysRequired;
            this.createQuery = createQuery;
        }

        public void Process()
        {
            thread = new Thread(Run);
            thread.Start();
        }

        private void Run()
        {
            try
            {
                AbstractQuery q = createQuery();
                HandleQuery(q);
                WriteToFile(q);
                NotifyComplete();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void HandleQuery(AbstractQuery query)
        {
            query.Table = DestinationTable;
            query.Rows.AddRange(DataAccess.DataAccess.Instance.FetchData(ConnectionString, Sql));

            if (query.KeysRequired)
            {
                FetchKeys(query);
            }
        }


        private void FetchKeys(AbstractQuery query)
        {
            if (onKeysRequired != null)
            {
                List<string> columns = new List<string>();
                if (query.Rows.Count > 0)
                {
                    foreach (string column in query.Rows[0].Keys)
                    {
                        columns.Add(column);
                    }
                }
                query.Keys.AddRange(onKeysRequired(this,columns));
            }
            else
            {
                throw new Exception("Keys required but no handler exists.");
            }
        }


        private void WriteToFile(AbstractQuery query)
        {
            if (createQueryWriter == null)
            {
                throw new Exception("No query writer was specified.");
            }

            using (StreamWriter writer = new StreamWriter(DestinationFilename))
            {
                QueryWriter queryWriter = createQueryWriter();
                queryWriter.Write(query, writer);
                writer.Close();
            }
        }

        private void HandleException(Exception ex)
        {
            if (onException != null)
            {
                onException(this, ex);
            }
            else
            {
                throw new Exception("Exception in job was unhandled. ", ex);
            }
        }

        private void NotifyComplete()
        {
            if (onComplete != null)
            {
                onComplete(this);
            }
        }

        private Thread thread;
        private Func<Job,IEnumerable<string>,IEnumerable<string>> onKeysRequired;
        private Action<Job, Exception> onException;
        private Action<Job> onComplete;
        private Func<AbstractQuery> createQuery;
        private Func<QueryWriter> createQueryWriter;
    }
}
