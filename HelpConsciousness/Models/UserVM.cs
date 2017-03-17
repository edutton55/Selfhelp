using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using DAL;
using System.ComponentModel.DataAnnotations;

namespace HelpConsciousness.Models
{
    public class UserVM
    {
        public UserVM()
        {
            Password = new PasswordVM();
        }

        public int UserId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }

        //[Required]
        //[Display(Name = "Password")]
        //[StringLength(32, MinimumLength = 7)]
        //[DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=]).*$", ErrorMessage = "You must have at least one uppercase and a special character.")]
        public PasswordVM Password { get; set; }

        /*[Required]
        [Display(Name = "Confirm Password")]
        [StringLength(32, MinimumLength = 7)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=]).*$", ErrorMessage = "You must have at least one uppercase and a special character.")]
        public string ConPassword { get; set; }*/
        public string SecLev { get; set; }

        public UserSM Map(UserVM user)
        {
            UserSM guy = new UserSM();
            //guy.Password = new PasswordSM();
            guy.UserId = user.UserId;
            guy.UserName = user.UserName;
            guy.Password.ConfirmPassword = user.Password.ConfirmPassword;
            guy.Password.NewPassword = user.Password.NewPassword;
            guy.Password.CurrentPassword = user.Password.CurrentPassword;
            guy.SecLev = user.SecLev;
            return guy;
        }
        public UserVM Map(UserSM user)
        {
            UserVM person = new UserVM();
            //guy.Password = new PasswordSM();
            person.UserId = user.UserId;
            person.UserName = user.UserName;
            person.Password.ConfirmPassword = user.Password.ConfirmPassword;
            person.Password.NewPassword = user.Password.NewPassword;
            person.Password.CurrentPassword = user.Password.CurrentPassword;
            person.SecLev = user.SecLev;
            return person;
        }
        public List<UserSM> Map(List<UserVM> usersList)
        {
            List<UserSM> userlist = new List<UserSM>();
            foreach (UserVM user in usersList)
            {
                userlist.Add(Map(user));
            }
            return userlist;
        }
        public List<UserVM> Map(List<UserSM> usersList)
        {
            List<UserVM> userlist = new List<UserVM>();
            foreach (UserSM user in usersList)
            {
                userlist.Add(Map(user));
            }
            return userlist;
        }
    }
}