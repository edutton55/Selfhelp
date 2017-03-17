using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ApplicationLogger;

namespace BLL
{
    public class UserLogic:IUserLogic
    {
        private ILoggerIO logs;
        private IUserDAO userData;
        private IHashing hash;

        public UserLogic(IUserDAO userDAO, ISQLDAO dao, ILoggerIO log, IHashing hasher)
        {
            hash = hasher;
            logs = log;
            userData = userDAO;   //injecting dependency
            userData.GetDataWriter(dao);//dependency injector through a infrastructure
        }
        /// <summary>
        /// Creates a List of UserSM from Database
        /// </summary>
        /// <returns>List of UserSM</returns>
        public List<UserSM> GetUsers() //Creates UserList
        {
            return Map(userData.GetUsers());
        }
        /// <summary>
        /// Converts UserDM to UserSM for use on the Logic Layer
        /// </summary>
        /// <param name="human">UserDM human</param>
        /// <returns>UserSM</returns>
        private UserSM Map(UserDM human) // Converts for user in the Logic/Presentation Layer
        {
            UserSM hm = new UserSM();
            hm.UserName = human.UserName;
            hm.Password.CurrentPassword = human.Password;
            hm.SecLev = human.SecLev;
            hm.UserId = Convert.ToInt32(human.UserId);
            return hm;
        }
        /// <summary>
        /// Converts a UserSM to a UserDM
        /// </summary>
        /// <param name="human">UserSM human</param>
        /// <returns>UserDM</returns>
        private UserDM Map(UserSM human) //Converts for use in the Data Layer
        {
            UserDM hm = new UserDM();
            hm.UserName = human.UserName;
            hm.Password = human.Password.CurrentPassword;
            hm.SecLev = human.SecLev;
            hm.UserId = human.UserId.ToString();
            return hm;
        }
        /// <summary>
        /// Converts a list of UserDM to UserSM
        /// </summary>
        /// <param name="human">List UserDM users</param>
        /// <returns>List of UserSM</returns>
        private List<UserSM> Map(List<UserDM> users) //Converts for Use in the Logic/Presentation Layer
        {
            List<UserSM> people = new List<UserSM>();
            foreach (UserDM user in users)
            {
                people.Add(Map(user));
            }
            return people;
        }
        /// <summary>
        /// Converts a list of UserSM to UserDM
        /// </summary>
        /// <param name="human">List UserSM users</param>
        /// <returns>List of UserDM</returns>
        private List<UserDM> Map(List<UserSM> users) //Converts for use in the Data Layer
        {
            List<UserDM> people = new List<UserDM>();
            foreach (UserSM user in users)
            {
                people.Add(Map(user));
            }
            return people;
        }
        /// <summary>
        /// Checks User Inputed UserName Against the Database
        /// </summary>
        /// <param name="tempUsername">string tempUsername</param>
        /// <returns>bool</returns>
        public bool CheckUsername(string tempUsername) //Matches UserName Against UserList
        {
            List<UserSM> list = GetUsers();
            foreach (UserSM item in list)
            {
                if (tempUsername == item.UserName)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Gets a User from the Data
        /// </summary>
        /// <param name="tempId">int tempId</param>
        /// <returns>UserSM</returns>
        public UserSM GetUserById(int tempId) //Finds a User in the UserList by UserId
        {
            return Map(userData.GetUserById(tempId.ToString()));
        }
        /// <summary>
        /// Tests the User's Username and Password input and returns a bool if there's a match.
        /// </summary>
        /// <param name="tempUser">UserSM tempUser</param>
        /// <returns>bool</returns>
        public bool Login(UserSM tempUser)
        {
            try
            {
                if (tempUser.UserId > 0)
                {
                    UserSM user = GetUserById(tempUser.UserId);

                    if (tempUser.Password.CurrentPassword == user.Password.CurrentPassword)
                    {
                        logs.LogError("Event", "User was successfully able to Login", "Class:UserLogic, Method::Login");
                        return true;
                    }
                }
            }
            catch (Exception a)
            {
                logs.LogError("Error", "User was unable to Login", "Class:UserLogic, Method:Login");
            }
            return false;
        }
        /// <summary>
        /// Adds User to DataBase based on User input.
        /// </summary>
        /// <param name="user">UserSM user</param>
        public void CreateUser(UserSM user)
        {
            try
            {

                if (!CheckUsername(user.UserName))
                {
                    user.Password.CurrentPassword = hash.GetHash(user.Password.NewPassword);
                    user.SecLev = "User";
                    userData.CreateUser(Map(user));
                    logs.LogError("Event", "a new user has been been added to database", "Class:UserLogic,Method:NewUser");
                }
            }
            catch (Exception d)
            {
                logs.LogError("Error", "A new user has not been added to the database", "Class:UserLogic,Method:NewUser");
            }
        }
        /// <summary>
        /// Takes User input to update User in database.
        /// </summary>
        /// <param name="user">UserSM user</param>
        public void EditUserById(UserSM user)
        {
            try
            {
                userData.EditUserById(Map(user));
                logs.LogError("Event", "User was successfully able to update", "Class:UserLogic, Method:EditUser");
            }
            catch (Exception c)
            {
                logs.LogError("Error", "User was unable to update", "Class:UserLogic, Method:EditUser");
            }
        }
        /// <summary>
        /// Finds User by UserId and Removes it from the Database
        /// </summary>
        /// <param name="id">int id</param>
        public void RemoveUserById(int id)
        {
            try
            {
                userData.RemoveUserById(id.ToString());
                logs.LogError("Event ", "User was able to remove an employee ", "Class:EmployeeLogic, Method:RemoveUser");
            }
            catch (Exception b)
            {
                logs.LogError("Error ", "User was unable to remove an employee ", "Class:EmployeeLogic, Method:RemoveUser");
            }
        }
        /// <summary>
        /// Locates a single user from the database based on Name
        /// </summary>
        /// <param name="user">UserSM user</param>
        /// <returns>UserSM</returns>
        public UserSM GetUser(UserSM user)
        {
            user.Password.CurrentPassword = hash.GetHash(user.Password.CurrentPassword);
            return Map(userData.GetUser(Map(user)));
        }
        /// <summary>
        /// Compares Newly Input password and Confirmation Password and changes the password in the database
        /// </summary>
        /// <param name="pass">PasswordSM pass</param>
        public void UpdatePassword(UserSM person)
        {
            UserSM guy = GetUserById(person.UserId);
            if (person.Password.NewPassword == person.Password.ConfirmPassword)
            {
                if (hash.GetHash(person.Password.CurrentPassword) == guy.Password.CurrentPassword)
                {
                    guy.Password.CurrentPassword = hash.GetHash(person.Password.NewPassword);
                    userData.EditUserById(Map(guy));
                }
            }
        }
    }
}
