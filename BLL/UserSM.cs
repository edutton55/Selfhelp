using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserSM
    {
        public UserSM()
        {
            Password = new PasswordSM();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public PasswordSM Password { get; set; }
        //public string ConPassword { get; set; }
        public string SecLev { get; set; }
        
    }
}
