﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ArtContest.Models
{
    public class EditStudentInfoViewModel
    {
        public User User { get; set; }
        public Student Student { get; set; }


        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Must between 6 to 20 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your password again")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string UserFirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
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

        [Required(ErrorMessage = "Please enter parent's first name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string ParentFirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string ParentMiddleName { get; set; }

        [Required(ErrorMessage = "Please enter parent's last name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string ParentLastName { get; set; }

        [Required(ErrorMessage = "Please enter parent's E-mail address")]
        [EmailAddress(ErrorMessage = "Please enter a valid e-mail adress")]
        public string ParentEmail { get; set; }

        [Required(ErrorMessage = "Please enter parent's phone number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid phone number")]
        public string ParentPhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your street")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Please enter your city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter your state")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter your Zip Code")]
        public string Zip { get; set; }

        public string TeacherTitle { get; set; }

        [Required(ErrorMessage = "Please enter teacher's first name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string TeacherFirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string TeacherMiddleName { get; set; }

        [Required(ErrorMessage = "Please enter teacher's last name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string TeacherLastName { get; set; }


    }
}