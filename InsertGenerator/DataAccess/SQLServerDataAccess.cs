using InsertGenerator.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertGenerator.DataAccess
{
    public class SQLServerDataAccess : IDataAccess
    {
        public List<Data.Row> FetchData(string connectionString, string sql)
        {
            List<Data.Row> result = new List<Data.Row>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Row row = new Row();
                            for (int i = 0; i < reader.VisibleFieldCount; i++)
                            {
                                row[reader.GetName(i)] = GetValue(reader, i);
                            }

                            result.Add(row);
                        }
                    }
                }
            }

            return result;
        }

        private Value GetValue(SqlDataReader reader, int column)
        {
            Value value = new Value();

            value.Item = reader.GetValue(column);

            Type type = value.Item.GetType();

            if(typeof(bool).IsAssignableFrom(type))
            {
                value.Type = ColumnType.Boolean;
            }
            else if(typeof(int).IsAssignableFrom(type))
            {
                value.Type = ColumnType.Integer;
            }
            else if (typeof(double).IsAssignableFrom(type))
            {
                value.Type = ColumnType.Double;
            }
            else if (typeof(byte[]).IsAssignableFrom(type))
            {
                value.Type = ColumnType.Blob;
            }
            else
            {
                value.Type = ColumnType.String;
            }

            return value;
        }
    }
}
