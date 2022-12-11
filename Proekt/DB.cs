using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proekt
{
    class DB
    {
        SqlConnection connection=new SqlConnection("server=DESKTOP-Q05O5DB; initial catalog=magazin; userid=user1; password=sa");
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}
