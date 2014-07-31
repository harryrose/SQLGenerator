using InsertGenerator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertGenerator.DataAccess
{
    interface IDataAccess
    {
        List<Row> FetchData(string connectionString, string sql);
    }
}
