using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ArtContest.Models
{
    public class EditStudentInfoViewModel
    {
        public User User { get; set; }
        public Student Student { get; set; }

        [Required(ErrorMessage = "Please enter your new password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Must be at least 6 characters long.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please enter your new password again")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}