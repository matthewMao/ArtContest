using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtContest.Models;

namespace ArtContest.Models
{
    public class LoginModel
    {
        public string userName { get; set; }
        public string password { get; set; }
        public List<String> PicPath { get; set; }
    }
}