using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ArtContest.Models {
    public class ScoreViewModel {
        public Picture Picture { get; set; }
        public PictureRate PictureRate { get; set; }

        [Required(ErrorMessage = "Please enter new score")]
        [Range(0, 100, ErrorMessage = "Please enter a valid score")]
        public int Score { get; set; }
    }
}