using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtContest.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ArtContest.Models
{
    public class UserSignUpViewModel
    {
        [Required(ErrorMessage = "An username is required")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Must be at least 4 characters long.")]
        [Remote("CheckForDuplication", "UserSignUp")]
        public string UserName { get; set; }

        
        [Required(ErrorMessage = "An password is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Must be at least 6 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "An confirmation is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Must be at least 6 characters long.")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "An first name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string UserFirstName { get; set; }

        public string UserMiddleName { get; set; }

        [Required(ErrorMessage = "An last name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string UserLastName { get; set; }


    }
}