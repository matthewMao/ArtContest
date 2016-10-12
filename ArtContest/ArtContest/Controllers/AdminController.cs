﻿using System;
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
        //[Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            
            List<Picture> pics= dbc.Pictures.Include("Student").Where(p => p.Public.Equals("Yes")).ToList();
            return View(pics);
        }
        public ActionResult DeleteMission(int id) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<PictureRate> pics= dbc.PictureRates.Where(p => p.JudgeId == id).ToList();
            if(pics.Count() > 0) { dbc.PictureRates.RemoveRange(pics); dbc.SaveChanges();
                return RedirectToAction("ViewJudge");
            }
            return RedirectToAction("ViewJudge");
        }
        [HttpGet]
        public ActionResult DividePic() {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<User> judge = dbc.Users.Where(u=>u.UserTypeId==2).ToList();
            return View(judge);
        }
        public ActionResult CheckPic(int id) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            CheckPicViewModel vm = new CheckPicViewModel();
            vm.User= dbc.Users.SingleOrDefault(u => u.Id == id);
            vm.PictureRates = dbc.PictureRates.Where(p => p.JudgeId == id).ToList();
            return View(vm);
        }
        [HttpPost]
        public ActionResult DividePic(int judgeId,string[] Grade) {
            foreach(var grade in Grade) {
                CTEFArtContestEntities dbc = new CTEFArtContestEntities();
                List<Student> stus = dbc.Students.Where(s => s.Grade == grade).ToList();
                int amount = stus.Count();
                foreach(var s in stus) {
                    Picture pic = dbc.Pictures.Where(p => p.UserId == s.Id && p.Public.Equals("Yes")).SingleOrDefault();
                    PictureRate pr = new PictureRate();
                    pr.PictureId = pic.Id;
                    pr.JudgeId = judgeId;
                    pr.Rate = 0;
                    if(!dbc.PictureRates.Any(r => r.JudgeId == judgeId && r.PictureId == pr.PictureId)) {
                        dbc.PictureRates.Add(pr);
                        dbc.SaveChanges();
                        amount--;
                    } else {
                        ModelState.AddModelError("You have Assign These Pictures to This Judge","You have Assign These Pictures to This Judge");
                    }
                }

                
            }
                  return RedirectToAction("Index","Admin");
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
        public ActionResult SearchBySchool(string school) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<Picture> pics = dbc.Pictures.Include("Student").Where(p => p.Student.School.Equals(school) && p.Public.Equals("Yes")).ToList();
            return View("Index",pics);
        }
        public ActionResult SearchByGrade(string grade) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<Picture> pics = dbc.Pictures.Include("Student").Where(p => p.Student.Grade.Equals(grade) && p.Public.Equals("Yes")).ToList();
            return View("Index",pics);
        }
        public ActionResult SearchBySchoolThenByGrade(string school, string grade) {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            List<Picture> pics = dbc.Pictures.Include("Student").Where(p => p.Student.School.Equals(school) && p.Student.Grade.Equals(grade) && p.Public.Equals("Yes")).ToList();
            return View("Index",pics);
        }
    }
}