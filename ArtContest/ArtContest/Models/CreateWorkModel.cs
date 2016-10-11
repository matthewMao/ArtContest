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
        public string Public { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}