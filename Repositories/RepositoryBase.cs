using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WpfApp4.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string con;
        public RepositoryBase()
        {

            con = @"Data Source=.;Initial Catalog=TestMVVM;Integrated Security=True";
        }
      protected SqlConnection GetConnection()
        {
            return new SqlConnection(con);  
        }
    }
}
