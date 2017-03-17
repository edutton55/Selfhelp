using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SQLDAO:ISQLDAO
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=SelfHelp;Trusted_Connection=True;";
        public int Write(SqlParameter[] parameters, string statement)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    return command.ExecuteNonQuery(); //returns the number of rows affected
                }
            }
        }
    }
}
