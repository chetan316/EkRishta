using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Models
{
    public class Login
    {
        [Required(ErrorMessage = "Please Enter LoginId")]
        public string LoginId { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }

        [Required(ErrorMessage = "Please Enter OTP")]
        public string LoginOTP { get; set; }
    }
}