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
        // GET: JudgeAccount
        public ActionResult Index()
        {
            var userid = (int)Session["userid"];
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            var prs = dbc.PictureRates.Where(p => p.JudgeId == userid).ToList();
            List<Picture> pics = new List<Picture>();
            foreach (var pid in prs)
            {
                var pic = dbc.Pictures.SingleOrDefault(p => p.Id == pid.PictureId);
                if (pic != null)
                {
                    pics.Add(pic);
                }
            }
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