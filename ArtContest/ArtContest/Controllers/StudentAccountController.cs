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
            var picIdList = dbc.Pictures.Select(p => p.Id).ToList();
            Random random = new Random();
            int randomNumber = random.Next(1, 1000);
            while (picIdList.Contains(randomNumber))
            {
                randomNumber = random.Next(1, 1000);
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

        //[HttpGet]
        //public ActionResult EditStudentInfo(int id)
        //{
        //    EditStudentInfoViewModel vm = new EditStudentInfoViewModel();
        //    vm.User = dbc.Users.SingleOrDefault(b => b.Id==id);
        //    vm.Student = dbc.Students.SingleOrDefault(b => b.Id == id);
        //    return View(vm);
        //}

        //[HttpPost]
        //public ActionResult EditStudentInfo(User user, Student student)
        //{
        //    if (pic.Description == null || pic.Public == null || pic.Title == null)
        //    {
        //        return View();
        //    }
        //    dbc.Entry(pic).State = System.Data.Entity.EntityState.Modified;
        //    dbc.SaveChanges();
        //    return RedirectToAction("index");
        //}
    }
}