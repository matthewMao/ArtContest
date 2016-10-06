using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtContest.Models;
using System.IO;
namespace ArtContest.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        //[Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<Picture> pics = dbc.Pictures.Include("Student").Where(p=>p.Private.Equals("yes")).ToList();
            return View(pics);
        }
        public ActionResult ViewJudge() {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<User> judge = dbc.Users.Where(u=>u.UserType.Id==2).ToList();
            return View(judge);
        }
        [HttpGet]
        public ActionResult CreateJudge() {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            CreateJudgeModel vm = new CreateJudgeModel();
            vm.UserType = dbc.UserTypes.ToList();
            return View(vm);
        }
        [HttpPost]
        public ActionResult CreateJudge(User user) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            dbc.Users.Add(user);
            dbc.SaveChanges();
            return RedirectToAction("ViewJudge","Admin");
        }
        public ActionResult RemoveJudge(int id) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            var target = dbc.Users.SingleOrDefault(j => j.Id == id);
            if(target != null) {
                dbc.Users.Remove(target);
                dbc.SaveChanges();
            }
            return RedirectToAction("ViewJudge","Admin");
        }
        public ActionResult Details(int id) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            Picture pic = dbc.Pictures.Include("Student").SingleOrDefault(p => p.Id == id);
            return View("Details",pic);
        }
        public ActionResult SearchBySchool(string school) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<Picture> pics = dbc.Pictures.Include("Student").Where(p => p.Student.School.Equals(school) && p.Private.Equals("yes")).ToList();
            return View("Index",pics);
        }
        public ActionResult SearchByGrade(string grade) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<Picture> pics = dbc.Pictures.Include("Student").Where(p => p.Student.Grade.Equals(grade) && p.Private.Equals("yes")).ToList();
            return View("Index",pics);
        }
        public ActionResult SearchBySchoolThenByGrade(string school, string grade) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<Picture> pics = dbc.Pictures.Include("Student").Where(p => p.Student.School.Equals(school) && p.Student.Grade.Equals(grade) && p.Private.Equals("yes")).ToList();
            return View("Index",pics);
        }
    }
}