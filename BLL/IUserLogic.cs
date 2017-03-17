using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IUserLogic
    {
        bool CheckUsername(string tempUsername);
        List<UserSM> GetUsers();
        UserSM GetUserById(int tempId);
        bool Login(UserSM tempUser);
        void CreateUser(UserSM user);
        void EditUserById(UserSM user);
        void RemoveUserById(int id);
        UserSM GetUser(UserSM user);
        void UpdatePassword(UserSM person);
    }
}
