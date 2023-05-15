using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet18_Test1_Milos_Stojic.DAO
{
    public class DaoConnection
    {
        private static string connString = "Server=localhost;Database=DotNet18_Test1_Milos_Stojic;Integrated security=True;MultipleActiveResultSets=True";

        public static SqlConnection NewConnection()
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            return conn;
        }
    }
}
