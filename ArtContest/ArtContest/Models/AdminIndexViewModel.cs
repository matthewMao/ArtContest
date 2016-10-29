using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtContest.Models {
    public class AdminIndexViewModel {
        public Student Student { get; set; }
        public List<Picture> Pictures { get; set; }
        public SelectList School { get; set; }
    }
}