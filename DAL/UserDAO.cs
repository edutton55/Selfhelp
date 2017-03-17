using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLogger;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class UserDAO:IUserDAO
    {
        private ISQLDAO dataWriter;
        private ILoggerIO logs;
        private string connectionString = @"Server=.\SQLEXPRESS;Database = SelfHelp;Trusted_Connection=True;";

        public UserDAO(ILoggerIO log)
        {
            logs = log;
        }
        public void GetDataWriter(ISQLDAO dataWriter)
        {
            this.dataWriter = dataWriter;
        }
        public List<UserDM> Read(SqlParameter[] parameters, string statement)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    SqlDataReader data = command.ExecuteReader();
                    List<UserDM> people = new List<UserDM>();
                    while (data.Read())
                    {
                        UserDM user = new UserDM();
                        user.UserId = data["UserId"].ToString();
                        user.UserName = data["UserName"].ToString();
                        user.Password = data["Password"].ToString();
                        user.SecLev = data["SecLev"].ToString();
                        people.Add(user);
                    }
                    return people;
                }
            }
        }
        public List<UserDM> GetUsers()
        {
            return Read(null, "GetUsers");
        }
        public void CreateUser(UserDM user)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@UserName", user.UserName)
                ,new SqlParameter("@Password", user.Password)
                ,new SqlParameter("@SecLev", user.SecLev)
            };
            dataWriter.Write(parameters, "CreateUser");
            logs.LogError("Event", "An User has been added to the database", "Class:UserDAO, Method:AddUser");
        }
        public void RemoveUserById(string id)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@UserId", id)
            };
            dataWriter.Write(parameters, "RemoveUser");
            logs.LogError("Event", "An User has been removed from the database", "Class:UserDAO, Method:RemoveUserById");
        }
        public void EditUserById(UserDM user)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@UserName", user.UserName)
                , new SqlParameter("@Password", user.Password)
                ,new SqlParameter("@SecLev", user.SecLev)
                , new SqlParameter("@Id", user.UserId) 
            };
            dataWriter.Write(parameters, "UpdateUsers");
            logs.LogError("Event", "An User has been updated", "Class:UserDAO, Method: UpdateUser");
        }
        public UserDM GetUserById(string id)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@UserId", id)
            };
            return Read(parameters, "GetUserById").SingleOrDefault();
        }
        public UserDM GetUser(UserDM veriUser)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@UserName", veriUser.UserName)
               ,new SqlParameter("@Password", veriUser.Password)
            };
            return Read(parameters, "GetUser").SingleOrDefault();
        }
    }
}
