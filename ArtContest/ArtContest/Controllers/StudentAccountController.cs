using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtContest.Models;
using System.IO;

namespace ArtContest.Controllers
{
    public class StudentAccountController : Controller
    {
        
        CTEFArtContestEntities dbc = new CTEFArtContestEntities();
        // GET: StudentAccount
        public ActionResult Index()
        {
            if (Session["userid"] == null || !dbc.Users.Where(u => u.UserTypeId == 3).Select(u => u.Id).ToList().Contains((int)Session["userid"])) return RedirectToAction("Index", "Home");
            var userid = (int)Session["userid"];
            List<Picture> allPic = dbc.Pictures.Where(p => p.UserId == userid).OrderBy(p=>p.Title).ThenBy(p => p.UploadDate).ToList();
            return View(allPic);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(CreateWorkModel newPicture, HttpPostedFileBase uploadingFile)
        {
            Picture pic = new Picture();
            if (newPicture.Title==null||newPicture.Public==null||newPicture.Description==null||uploadingFile==null)
            {
                return View();
            }
            var picIdList = dbc.Pictures.Select(p => p.Id).ToList();
            Random random = new Random();
            int randomNumber = random.Next(1, 5000);
            while (picIdList.Contains(randomNumber))
            {
                randomNumber = random.Next(1, 5000);
            }
            if (uploadingFile != null && !string.IsNullOrEmpty(uploadingFile.FileName))
            {
                var fileName = Path.GetFileName(uploadingFile.FileName);
                var dirPath = Server.MapPath("~/Resource/Images");
                var filePath = Path.Combine(dirPath, fileName);
                uploadingFile.SaveAs(filePath);
                pic.PicturePath = "Resource/Images/" + fileName;
            }
            pic.UserId = (int)Session["userid"];
            pic.Id = randomNumber;
            pic.Title = newPicture.Title;
            pic.Public = newPicture.Public;
            pic.UploadDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            pic.Description = newPicture.Description;
            
            dbc.Pictures.Add(pic);
            if (pic.Public == "Yes")
            {
                foreach (var picture in dbc.Pictures)
                {
                    if (picture.UserId == pic.UserId && picture.Public.Equals("Yes") && picture.Id!=pic.Id)
                    {
                        picture.Public = "No";
                        dbc.Entry(picture).State = System.Data.Entity.EntityState.Modified;
                    }
                }
            }
            dbc.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var target = dbc.Pictures.SingleOrDefault(p => p.Id == id);
            if (target != null)
            {
                dbc.Pictures.Remove(target);
                dbc.SaveChanges();
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult ShowDetail(int id)
        {
            Picture res = dbc.Pictures.Include("Student").SingleOrDefault(p => p.Id == id);
            return View("Detail", res);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EditPictureViewModel vm = new EditPictureViewModel();
            vm.pic = dbc.Pictures.SingleOrDefault(b => b.Id == id);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(Picture pic)
        {
            if (pic.Description == null || pic.Public == null || pic.Title == null||pic.Public==null)
            {
                return View();
            }
            dbc.Entry(pic).State = System.Data.Entity.EntityState.Modified;
            
            if (pic.Public == "Yes")
            {
                foreach (var picture in dbc.Pictures)
                {
                    if (picture.UserId == pic.UserId && picture.Public.Equals("Yes") && picture.Id != pic.Id)
                    {
                        picture.Public = "No";
                        dbc.Entry(picture).State = System.Data.Entity.EntityState.Modified;

                    }
                }
            }
            dbc.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            var id = (int)Session["userid"];
            EditStudentInfoViewModel vm = new EditStudentInfoViewModel();
            vm.User = dbc.Users.Where(u => u.Id == id).SingleOrDefault();
            return View(vm);
        }

        [HttpPost]
        public ActionResult ChangePassword(EditStudentInfoViewModel esi)
        {
            esi.User.Password = esi.Password;
            dbc.Entry(esi.User).State = System.Data.Entity.EntityState.Modified;
            dbc.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult EditStudentInfo()
        {
            var id = (int)Session["userid"];
            EditStudentInfoViewModel vm = new EditStudentInfoViewModel();
            vm.User = dbc.Users.Where(u => u.Id == id).SingleOrDefault();
            vm.Student = dbc.Students.Where(u => u.Id == id).SingleOrDefault();
            vm.UserFirstName = dbc.Users.Where(u => u.Id == id).Select(u => u.UserFirstName).SingleOrDefault();
            vm.UserLastName = dbc.Users.Where(u => u.Id == id).Select(u => u.UserLastName).SingleOrDefault();
            vm.UserMiddleName = dbc.Users.Where(u => u.Id == id).Select(u => u.UserMiddleName).SingleOrDefault();
            vm.Gender = dbc.Students.Where(u => u.Id == id).Select(u => u.Gender).SingleOrDefault();
            vm.Age = dbc.Students.Where(u => u.Id == id).Select(u => u.Age).SingleOrDefault();
            vm.School = dbc.Students.Where(u => u.Id == id).Select(u => u.School).SingleOrDefault();
            vm.Grade = dbc.Students.Where(u => u.Id == id).Select(u => u.Grade).SingleOrDefault();
            vm.ParentFirstName = dbc.Students.Where(u => u.Id == id).Select(u => u.ParentFirstName).SingleOrDefault();
            vm.ParentMiddleName = dbc.Students.Where(u => u.Id == id).Select(u => u.ParentMiddleName).SingleOrDefault();
            vm.ParentLastName = dbc.Students.Where(u => u.Id == id).Select(u => u.ParentLastName).SingleOrDefault();
            vm.ParentEmail = dbc.Students.Where(u => u.Id == id).Select(u => u.ParentEmail).SingleOrDefault();
            vm.ParentPhoneNumber = dbc.Students.Where(u => u.Id == id).Select(u => u.ParentPhoneNumber).SingleOrDefault();
            vm.Street = dbc.Students.Where(u => u.Id == id).Select(u => u.Street).SingleOrDefault();
            vm.City = dbc.Students.Where(u => u.Id == id).Select(u => u.City).SingleOrDefault();
            vm.State = dbc.Students.Where(u => u.Id == id).Select(u => u.State).SingleOrDefault();
            vm.Zip = dbc.Students.Where(u => u.Id == id).Select(u => u.Zip).SingleOrDefault();
            vm.TeacherTitle = dbc.Students.Where(u => u.Id == id).Select(u => u.TeacherTitle).SingleOrDefault();
            vm.TeacherFirstName = dbc.Students.Where(u => u.Id == id).Select(u => u.TeacherFirstName).SingleOrDefault();
            vm.TeacherMiddleName = dbc.Students.Where(u => u.Id == id).Select(u => u.TeacherMiddleName).SingleOrDefault();
            vm.TeacherLastName = dbc.Students.Where(u => u.Id == id).Select(u => u.TeacherLastName).SingleOrDefault();
            return View(vm);
        }

        [HttpPost]
        public ActionResult EditStudentInfo(EditStudentInfoViewModel esi)
        {
            esi.User.UserFirstName = esi.UserFirstName;
            esi.User.UserLastName = esi.UserLastName;
            esi.User.UserMiddleName = esi.UserMiddleName;
            esi.Student.Gender = esi.Gender;
            esi.Student.Age = esi.Age;
            esi.Student.School = esi.School;
            esi.Student.Grade = esi.Grade;
            esi.Student.ParentFirstName = esi.ParentFirstName;
            esi.Student.ParentMiddleName = esi.ParentMiddleName;
            esi.Student.ParentLastName = esi.ParentLastName;
            esi.Student.ParentEmail = esi.ParentEmail;
            esi.Student.ParentPhoneNumber = esi.ParentPhoneNumber;
            esi.Student.Street = esi.Street;
            esi.Student.City = esi.City;
            esi.Student.State = esi.State;
            esi.Student.Zip = esi.Zip;
            esi.Student.TeacherTitle = esi.TeacherTitle;
            esi.Student.TeacherFirstName = esi.TeacherFirstName;
            esi.Student.TeacherMiddleName = esi.TeacherMiddleName;
            esi.Student.TeacherLastName = esi.TeacherLastName;
            dbc.Entry(esi.User).State = System.Data.Entity.EntityState.Modified;
            dbc.Entry(esi.Student).State = System.Data.Entity.EntityState.Modified;
            dbc.SaveChanges();
            return RedirectToAction("index");
        }
    }
}