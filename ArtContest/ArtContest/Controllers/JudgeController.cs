using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtContest.Models;
namespace ArtContest.Controllers
{
    [Authorize(Roles = "Judge")]
    public class JudgeController : Controller
    {
        // GET: Judge
        public ActionResult Index()
        {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<Picture> pics = dbc.Pictures.Include("Student").ToList();
            return View(pics);
        }
    }
}