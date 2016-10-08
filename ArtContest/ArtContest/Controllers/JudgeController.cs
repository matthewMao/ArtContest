using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtContest.Models;
namespace ArtContest.Controllers
{
    //[Authorize(Roles = "Judge")]
    public class JudgeController : Controller
    {
        // GET: Judge
        public ActionResult Index()
        {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<Picture> pics = dbc.Pictures.Include("Student").Where(p=>p.Public.Equals("yes")).ToList();
            return View(pics);
        }
        public ActionResult Larger(int id) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            ScoreViewModel vm = new ScoreViewModel();
            vm.PictureRate = dbc.PictureRates.SingleOrDefault(p => p.PictureId == id);
            vm.Picture = dbc.Pictures.SingleOrDefault(p => p.Id == id);
            return View(vm);
        }
        public ActionResult Score(int picid, int judgeid, int score) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            PictureRate pr = new PictureRate();
            pr.JudgeId = judgeid;
            pr.PictureId = picid;
            pr.Rate = score;
            dbc.Entry(pr).State=System.Data.Entity.EntityState.Modified;
            dbc.SaveChanges();
            return RedirectToAction("Index","Judge");
        }
    }
}