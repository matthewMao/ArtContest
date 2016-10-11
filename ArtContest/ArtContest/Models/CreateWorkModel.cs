using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtContest.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ArtContest.Models
{
    public class CreateWorkModel
    {
        [Required(ErrorMessage = "Please choose visibility")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Must be at least 6 characters long.")]
        public string Public { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter your description")]
        public string Description { get; set; }
    }
}