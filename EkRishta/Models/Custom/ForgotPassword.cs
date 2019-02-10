using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Models.Custom
{
    public class ForgotPassword
    {
        [Required(ErrorMessage="Please Enter Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password does not match")]
        public string ConfirmPassword { get; set; }
        public string OTP { get; set; }
        public string MobileNo { get; set; }
    }
}