using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace Assignment.Models
{
    /* For action Detail, List, Edit users */
    public class GetUser
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        public string Password { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Firstname")]
        public string Firstname { get; set; }

        [Display(Name = "Lastname")]
        public string Lastname { get; set; }
    }

    public class SignInModel {
        [Required(ErrorMessage = "You must enter the username.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "You must enter the password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool Remember { get; set; }
    }

    public class InsertUser {
        [Required(ErrorMessage = "You must enter the username.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "You must enter the password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must enter the confirm password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Address")]
        [StringLength(250, MinimumLength = 2)]
        public string Address { get; set; }

        [Required(ErrorMessage = "You must enter the email address.")]
        [EmailAddress]
        [Display(Name = "Email")]
        [StringLength(150, MinimumLength = 11)]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter the firstname.")]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "You must enter the lastname.")]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }
    }

    public class UpdateUser
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(250, MinimumLength = 2)]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [StringLength(150, MinimumLength = 11)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }
    }

    public class DeleteUser
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
    }

    public class ChangePassword
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

}