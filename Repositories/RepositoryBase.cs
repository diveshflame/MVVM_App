using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Npgsql;

namespace MVVM_App.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string con;
        public RepositoryBase()
        {

            con = @"Server=localhost;Port=5432;User Id=postgres;Password=Ashritha@2000;Database=WeCareDB;";
        }
      protected NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(con);  
        }
    }
}
