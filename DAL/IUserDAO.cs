using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUserDAO
    {
        void GetDataWriter(ISQLDAO dataWriter);
        List<UserDM> Read(SqlParameter[] parameters, string statement);
        List<UserDM> GetUsers();
        void CreateUser(UserDM user);
        void RemoveUserById(string userId);
        void EditUserById(UserDM user);
        UserDM GetUserById(string userId);
        UserDM GetUser(UserDM veriUser);
    }
}
