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
        public ActionResult UserSignUp(UserSignUpViewModel newUser)
        {
            User user = new User();
            Student student = new Student();
            user.UserTypeId = 3;
            user.UserName = newUser.UserName;
            user.Password = newUser.Password;
            user.UserFirstName = newUser.UserFirstName;
            user.UserLastName = newUser.UserLastName;
            user.UserMiddleName = newUser.UserMiddleName;
            student.Gender = newUser.Gender;
            student.Age = newUser.Age;
            student.School = newUser.School;
            student.Grade = newUser.Grade;
            student.ParentFirstName = newUser.ParentFirstName;
            student.ParentMiddleName = newUser.ParentMiddleName;
            student.ParentLastName = newUser.ParentLastName;
            student.ParentEmail = newUser.ParentEmail;
            student.ParentPhoneNumber = newUser.ParentPhoneNumber;
            student.Street = newUser.Street;
            student.City = newUser.City;
            student.State = newUser.State;
            student.Zip = newUser.Zip;
            student.TeacherTitle = newUser.TeacherTitle;
            student.TeacherFirstName = newUser.TeacherFirstName;
            student.TeacherMiddleName = newUser.TeacherMiddleName;
            student.TeacherLastName = newUser.TeacherLastName;
            student.StudentSignature = newUser.StudentSignature;
            student.ParentSignature = newUser.ParentSignature;
            dbc.Users.Add(user);
            student.Id = user.Id;
            dbc.Students.Add(student);
            dbc.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}