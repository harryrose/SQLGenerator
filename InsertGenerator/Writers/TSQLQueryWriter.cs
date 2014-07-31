using InsertGenerator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertGenerator.Writers
{
    public class TSQLQueryWriter : QueryWriter
    {
        protected override void DoWrite(Query.InsertQuery query, System.IO.TextWriter writer)
        {
            foreach (Row row in query.Rows)
            {
                WriteInsert(query,row, writer);
            }
        }

        protected override void DoWrite(Query.UpdateQuery query, System.IO.TextWriter writer)
        {
            foreach (Row row in query.Rows)
            {
                WriteUpdate(query, row, writer);
            }
        }

        protected override void DoWrite(Query.DeleteQuery query, System.IO.TextWriter writer)
        {
            foreach (Row row in query.Rows)
            {
                WriteDelete(query, row, writer);
            }
        }

        private void WriteInsert(Query.InsertQuery query, Row row, System.IO.TextWriter writer)
        {
            List<string> columns = row.Keys.ToList();
            List<string> values = columns.Select(x => HandleValue(row[x])).ToList();

            writer.WriteLine("INSERT INTO {0} ({1}) VALUES ({2});", query.Table, string.Join(",",columns), string.Join(",",values));
        }

        private void WriteUpdate(Query.UpdateQuery query, Row row, System.IO.TextWriter writer)
        {
            
            List<string> setList = row.Keys.Where(x => !query.Keys.Contains(x)).Select(x =>
                string.Format("{0} = {1}", x, HandleValue(row[x]))).ToList();
            

            writer.WriteLine("UPDATE {0} SET {1} WHERE {2};",query.Table, string.Join(",",setList), GenerateWhereList(query,row));
        }

        private void WriteDelete(Query.DeleteQuery query, Row row, System.IO.TextWriter writer)
        {
            writer.WriteLine("DELETE FROM {0} WHERE {1};", query.Table, GenerateWhereList(query, row));
        }

        private string GenerateWhereList(Query.SelectiveQuery query, Row row)
        {
            List<string> whereList = query.Keys.Select(x => string.Format("{0} = {1}", x, HandleValue(row[x]))).ToList();
            return string.Join(" AND ", whereList);
        }

        private string HandleValue(Value value)
        {
            string result = "";
            switch (value.Type)
            {
                case ColumnType.Boolean:
                    result = (((bool) value.Item) ? 1 : 0).ToString();
                    break;

                case ColumnType.Double:
                case ColumnType.Integer:
                    result = value.Item.ToString();
                    break;

                case ColumnType.String:
                    result = string.Format("'{0}'",value.Item.ToString().Replace("'","''"));
                    break;

                case ColumnType.Blob:
                    result = BinaryToString((byte[]) value.Item);
                    break;

                default:
                    throw new Exception(string.Format("Unknown data type: {0}", value.Type.ToString()));
            }

            return result;
        }

        private string BinaryToString(byte[] binaryObject)
        {
            string result = "";

            if (binaryObject.Length > 0)
            {
                result += "0x";
                foreach (byte b in binaryObject)
                {
                    result += string.Format("{0:x2}", b);
                }
            }
            return result;
        }
    }
}
