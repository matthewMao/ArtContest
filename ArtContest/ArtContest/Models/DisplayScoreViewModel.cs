using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtContest.Models {
    public class DisplayScoreViewModel {
        public List<Picture> Pictures { get; set; }
        public List<PictureRate> PictureRates { get; set; }
    }
}