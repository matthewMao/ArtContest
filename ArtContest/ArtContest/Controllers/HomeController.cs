using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtContest.Models;

namespace ArtContest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            if (model.userName == null || model.password == null)
            {
                return View();
            }
            var userList = dbc.Users.Select(u => u.UserName).ToList();
            if (userList.Contains(model.userName) &&
            model.password == dbc.Users.Where(u => u.UserName == model.userName).Select(u => u.Password).Single())
            {
                if (dbc.Users.Where(u => u.UserName == model.userName).Select(u => u.UserTypeId).SingleOrDefault() == 1)
                {
                    Session["userid"] = dbc.Users.Where(s => s.UserName == model.userName).Select(u => u.Id).SingleOrDefault();
                    return RedirectToAction("Index", "Admin");
                }
                if (dbc.Users.Where(u => u.UserName == model.userName).Select(u => u.UserTypeId).SingleOrDefault() == 2)
                {
                    Session["userid"] = dbc.Users.Where(s => s.UserName == model.userName).Select(u => u.Id).SingleOrDefault();
                    return RedirectToAction("Index", "Judge");
                }
                if (dbc.Users.Where(u => u.UserName == model.userName).Select(u => u.UserTypeId).SingleOrDefault() == 3)
                {
                    if(dbc.DisableStudents.Where(d => d.Id==1).Select(d => d.Disable).SingleOrDefault().Equals("yes"))
                    {
                        TempData["notice"] = "Sorry the due date past, you can't login.";
                        return View();
                    }
                    else
                    {
                        model.PicPath = dbc.Pictures
                        .Where(p => p.UserId == dbc.Users.Where(u => model.userName == u.UserName)
                        .Select(u => u.Id).FirstOrDefault()).Select(p => p.PicturePath).ToList();
                        Session["userid"] = dbc.Users.Where(s => s.UserName == model.userName).Select(u => u.Id).SingleOrDefault();
                        return RedirectToAction("Index", "StudentAccount");
                    } 
                }
            }
            return View();
        }

        //[HttpGet]
        //public ActionResult PasswordFindBack()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult PasswordFindBack(VerifyUserEmail veri)
        //{
        //    CTEFArtContestEntities dbc = new CTEFArtContestEntities();
        //    if (veri.UserName == null || veri.Email == null) return View();
        //    var userList = dbc.Users.Select(u => u.UserName).ToList();
        //    var emailList = dbc.Students.Select(s => s.ParentEmail).ToList();
        //    if (userList.Contains(veri.UserName) && emailList.Contains(veri.Email) )
        //    {
        //        return RedirectToAction("ResetPassword");
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult ResetPassword(int id)
        //{
        //    CTEFArtContestEntities dbc = new CTEFArtContestEntities();
        //    EditStudentInfoViewModel vm = new EditStudentInfoViewModel();
        //    vm.User = dbc.Users.SingleOrDefault(b => b.Id == id);
        //    return View(vm);
        //}

        //[HttpPost]
        //public ActionResult ResetPassword(EditStudentInfoViewModel newPass)
        //{
        //    var dbc = new CTEFArtContestEntities();
        //    if (newPass.NewPassword==null||newPass.ConfirmNewPassword==null)
        //    {
        //        return View();
        //    }
        //    dbc.Entry(User).State = System.Data.Entity.EntityState.Modified;
        //    dbc.SaveChanges();
        //    return RedirectToAction("index");
        //}
    }
}