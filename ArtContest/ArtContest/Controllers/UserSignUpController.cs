using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtContest.Models;

namespace ArtContest.Controllers
{
    public class UserSignUpController : Controller
    {
        CTEFArtContestEntities dbc = new CTEFArtContestEntities();
        // GET: UserSignUp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RemoteValidation(User user)
        {
            return View(user);
        }

        public JsonResult CheckForDuplication(string userName)
        {
            return Json(!dbc.Users.Any(u=>u.UserName==userName), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //public ActionResult UserSignUp(User user, Student student)
        public ActionResult UserSignUp(UserSignUpViewModel suser)
        {
            User user = new User();
            user.UserTypeId = 3;
            user.UserName = suser.UserName;
            user.Password = suser.Password;
            user.UserFirstName = suser.UserFirstName;
            user.UserLastName = suser.UserLastName;
            user.UserMiddleName = suser.UserMiddleName;
            dbc.Users.Add(user);
            dbc.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}