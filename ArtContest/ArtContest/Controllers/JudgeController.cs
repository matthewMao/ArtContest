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
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            if (Session["userid"]==null||!dbc.Users.Where(u=>u.UserTypeId==2).Select(u=>u.Id).ToList().Contains((int)Session["userid"])) return RedirectToAction("Index", "Home");
            var userid = (int)Session["userid"];
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
            var userid = (int)Session["userid"];
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            ScoreViewModel vm = new ScoreViewModel();
            vm.PictureRate = dbc.PictureRates.SingleOrDefault(p => p.PictureId == id && p.JudgeId == userid);
            vm.Picture = dbc.Pictures.SingleOrDefault(p => p.Id == id);
            return View(vm);
        }
        

        [HttpPost]
        public ActionResult Score(ScoreViewModel sc) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            sc.PictureRate.Rate = sc.Score;
            dbc.Entry(sc.PictureRate).State=System.Data.Entity.EntityState.Modified;
            dbc.SaveChanges();
            return RedirectToAction("Index","Judge");
        }
        [HttpGet]
        public ActionResult ChangeP() {
            return View();

        }
        [HttpPost]
        public ActionResult ChangeP(string password1,string password2,User user) {
            if(password1 != password2) {
                TempData["notice"] = "The password You just enter was different from the First one you enter";
                return RedirectToAction("ChangeP");
            }
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            var judges = dbc.Users.Where(u => u.UserTypeId == 2).ToList();
            
            foreach(var judge in judges) {
                if(judge.Id== (int)Session["userid"]) {
                    judge.Password = password1;
                    dbc.Entry(judge).State = System.Data.Entity.EntityState.Modified;
                }
            }          
            dbc.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}