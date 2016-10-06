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
        [Required(ErrorMessage = "Please enter your username")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Must be at least 4 characters long.")]
        [Remote("CheckForDuplication", "UserSignUp")]
        public string UserName { get; set; }

        
        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Must be at least 6 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your password again")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string UserFirstName { get; set; }

        public string UserMiddleName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string UserLastName { get; set; }

        [Required(ErrorMessage = "Please select a gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please enter your age")]
        [Range(1, 120, ErrorMessage = "Please enter a valid age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please enter your school name")]
        public string School { get; set; }

        [Required(ErrorMessage = "Please select your grade")]
        public string Grade { get; set; }

        [Required(ErrorMessage = "Please enter your parent's first name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string ParentFirstName { get; set; }

        public string ParentMiddleName { get; set; }

        [Required(ErrorMessage = "Please enter your parent's last name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string ParentLastName { get; set; }

        [Required(ErrorMessage = "Please enter your parent's E-mail address")]
        [EmailAddress(ErrorMessage = "Please enter a valid e-mail adress")]
        public string ParentEmail { get; set; }

        [Required(ErrorMessage = "Please enter your parent's phone number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid phone number")]
        public string ParentPhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your street")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Please enter your city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter your state")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter your Zip Code")]
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Please enter a valid Zip Code")]
        public string Zip { get; set; }
        
        public string TeacherTitle { get; set; }

        [Required(ErrorMessage = "Please enter your teacher's first name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string TeacherFirstName { get; set; }
        
        public string TeacherMiddleName { get; set; }

        [Required(ErrorMessage = "Please enter your teacher's last name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string TeacherLastName { get; set; }

        [Required(ErrorMessage = "Please sign your name")]
        public string StudentSignature { get; set; }

        [Required(ErrorMessage = "Please sign your parent's name")]
        public string ParentSignature { get; set; }

    }
}