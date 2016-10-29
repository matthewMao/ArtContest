using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtContest.Models;
using System.IO;



namespace ArtContest.Controllers {
    public class AdminController:Controller {
        //[Authorize(Roles ="Admin")]
        public ActionResult Index() {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            if(Session["userid"] == null || !dbc.Users.Where(u => u.UserTypeId == 1).Select(u => u.Id).ToList().Contains((int)Session["userid"])) return RedirectToAction("Index","Home");
            var userid = (int)Session["userid"];
            AdminIndexViewModel vm = new AdminIndexViewModel();
            vm.School = new SelectList(dbc.Pictures.Where(s=>s.Public.Equals("Yes")).Select(s=>s.Student.School),"School");
            vm.Pictures = dbc.Pictures.Include("Student").Where(p => p.Public.Equals("Yes")).ToList();
            return View(vm);
        }
        public ActionResult ViewAll() {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<Picture> pics = dbc.Pictures.Include("Student").Where(p => p.Public.Equals("Yes")).ToList();
            return View(pics);
        }
        public ActionResult DeleteMission(int id) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<PictureRate> pics = dbc.PictureRates.Where(p => p.JudgeId == id).ToList();
            if(pics.Count() > 0) {
                dbc.PictureRates.RemoveRange(pics); dbc.SaveChanges();
                return RedirectToAction("ViewJudge");
            }
            return RedirectToAction("ViewJudge");
        }
        [HttpGet]
        public ActionResult DividePic() {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<User> judge = dbc.Users.Where(u => u.UserTypeId == 2).ToList();
            return View(judge);
        }
        public ActionResult CheckPic(int id) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            CheckPicViewModel vm = new CheckPicViewModel();
            vm.User = dbc.Users.SingleOrDefault(u => u.Id == id);
            vm.PictureRates = dbc.PictureRates.Where(p => p.JudgeId == id).ToList();
            vm.Grades = new HashSet<string>();
            //vm.Grades = dbc.Students.Where(s=>s.Grade = )
            var pics = new HashSet<Picture>();
            foreach(var item in vm.PictureRates) {
                pics.Add(dbc.Pictures.Where(p => p.Id == item.PictureId).SingleOrDefault());
            }
            foreach(var item in pics) {
                vm.Grades.Add(dbc.Students.SingleOrDefault(s => s.Id == item.UserId).Grade);
            }
            return View(vm);
        }
        [HttpPost]
        public ActionResult DividePic(int judgeId,string[] Grade) {
            if(Grade == null) {
                TempData["notice"] = "Please Don't leave the grade blank";
                return RedirectToAction("DividePic");
            }
            foreach(var grade in Grade) {
                CTEFArtContestEntities dbc = new CTEFArtContestEntities();
                List<Student> stus = dbc.Students.Where(s => s.Grade == grade).ToList();
                foreach(var s in stus) {
                    Picture pic = dbc.Pictures.Where(p => p.UserId == s.Id && p.Public.Equals("Yes")).SingleOrDefault();
                    if(pic == null) {
                        TempData["notice"] = "One or more grades have empty pictures, System won't assign the empty picture";
                        continue;
                    }
                    PictureRate pr = new PictureRate();
                    pr.PictureId = pic.Id;
                    pr.JudgeId = judgeId;
                    pr.Rate = 0;
                    if(!dbc.PictureRates.Any(r => r.JudgeId == judgeId && r.PictureId == pr.PictureId)) {
                        dbc.PictureRates.Add(pr);
                        dbc.SaveChanges();
                    } else {
                        ModelState.AddModelError("You have Assign These Pictures to This Judge","You have Assign These Pictures to This Judge");
                    }
                }
            }
            return RedirectToAction("ViewJudge","Admin");
        }
        public ActionResult CountPic() {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<Picture> pics = dbc.Pictures.Where(s => s.Public.Equals("Yes")).ToList();
            return View(pics);
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
            var admin = dbc.Users.Where(u => u.UserTypeId == 1).SingleOrDefault();
            admin.Password = password1;
            dbc.Entry(admin).State = System.Data.Entity.EntityState.Modified;
            dbc.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult ViewJudge() {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<User> judge = dbc.Users.Where(u => u.UserType.Id == 2).ToList();
            return View(judge);
        }
        [HttpGet]
        public ActionResult CreateJudge() {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            CreateJudgeModel vm = new CreateJudgeModel();
            //vm.UserType = dbc.UserTypes.ToList();
            return View(vm);
        }
        [HttpPost]
        public ActionResult CreateJudge(User user) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            if(user.UserName == null || user.Password == null || user.UserFirstName == null || user.UserLastName == null) {
                return View();
            }
            user.UserTypeId = 2;
            dbc.Users.Add(user);
            dbc.SaveChanges();
            return RedirectToAction("ViewJudge","Admin");
        }
        public ActionResult RemoveJudge(int id) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            var target = dbc.Users.SingleOrDefault(j => j.Id == id);
            List<PictureRate> prlist = dbc.PictureRates.Where(pr => pr.JudgeId == id).ToList();
            if(target != null) {
                dbc.Users.Remove(target);
                dbc.PictureRates.RemoveRange(prlist);
                dbc.SaveChanges();
            }
            return RedirectToAction("ViewJudge","Admin");
        }
        public ActionResult Details(int id) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            Picture pic = dbc.Pictures.Include("Student").SingleOrDefault(p => p.Id == id);
            return View("Details",pic);
        }
        public ActionResult SearchBySchool(FormCollection form) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            string school = form["schools"].ToString();
            List<Picture> pics = dbc.Pictures.Include("Student").Where(p => p.Student.School.Equals(school) && p.Public.Equals("Yes")).ToList();
            return View("ViewAll",pics);
        }
        public ActionResult SearchByGrade(string grade) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<Picture> pics = dbc.Pictures.Include("Student").Where(p => p.Student.Grade.Equals(grade) && p.Public.Equals("Yes")).ToList();
            return View("ViewAll",pics);
        }
        public ActionResult SearchBySchoolThenByGrade(FormCollection form,string grade) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            string school = form["schools"].ToString();
            List<Picture> pics = dbc.Pictures.Include("Student").Where(p => p.Student.School.Equals(school) && p.Student.Grade.Equals(grade) && p.Public.Equals("Yes")).ToList();
            return View("ViewAll",pics);
        }
    }
}