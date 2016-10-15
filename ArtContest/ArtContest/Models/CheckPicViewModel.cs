using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtContest.Models {
    public class CheckPicViewModel {
        public User User { get; set; }
        public List<PictureRate> PictureRates { get; set; }

        public HashSet<string> Grades { get; set; }
    }
}