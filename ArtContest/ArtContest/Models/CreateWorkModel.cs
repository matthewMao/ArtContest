using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtContest.Models;

namespace ArtContest.Models
{
    public class CreateWorkModel
    {
        public int MyProperty { get; set; }
        public string Private { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
    }
}