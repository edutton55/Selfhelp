using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpConsciousness.Models
{
    public class PasswordVM
    {
        //[Required] // represents current password
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [Display(Name = "New Password")]
        [StringLength(32, MinimumLength = 7)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=]).*$", ErrorMessage = "You must have at least one Uppercase and a special character.")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [StringLength(32, MinimumLength = 7)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=]).*$", ErrorMessage = "You must have at least one Uppercase and a special character.")]
        public string ConfirmPassword { get; set; }

        public int Id { get; set; }

        //public static PasswordSM Map(PasswordVM pass)
        //{
        //    PasswordSM recieve = new PasswordSM(); //Map for use on the Logic Layer
        //    recieve.Id = pass.Id;
        //    recieve.OldPassword = pass.OldPassword;
        //    recieve.NewPassword = pass.NewPassword;
        //    recieve.ConfirmPassword = pass.ConfirmPassword;
        //    return recieve;
        //}
    }
}