using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtContest.Models;

namespace ArtContest.Controllers
{
    public class StudentAccountController : Controller
    {
        CTEFArtContestEntities dbc = new CTEFArtContestEntities();
        // GET: StudentAccount
        public ActionResult Index()
        {
            var userid = (int)Session["userid"];
            List<Picture> allPic = dbc.Pictures.Where(p => p.UserId == userid).ToList();
            return View(allPic);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Picture pic, HttpPostedFileBase uploadingFile)
        {
            if (uploadingFile != null && !string.IsNullOrEmpty(uploadingFile.FileName))
            {
                var fileName = Path.GetFileName(uploadingFile.FileName);
                var dirPath = Server.MapPath("~/Resource/Images");
                var filePath = Path.Combine(dirPath, fileName);
                uploadingFile.SaveAs(filePath);
                pic.PicturePath = "Resource/Images/" + fileName;
            }
            pic.UserId = (int)Session["userid"];
            if (pic.Description == null || pic.Private == null || pic.Title == null || pic.UploadDate == null || pic.PicturePath == null)
            {
                return View();
            }
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            dbc.Pictures.Add(pic);
            dbc.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
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
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            Picture res = dbc.Pictures.Include("Student").SingleOrDefault(p => p.Id == id);
            if (res == null)
            {
                return RedirectToAction("NoResult");
            }
            return View("Detail", res);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            EditPictureViewModel vm = new EditPictureViewModel();
            vm.pic = dbc.Pictures.SingleOrDefault(b => b.Id == id);
            return View(vm);
        }
        [HttpPost]
        public ActionResult Edit(Picture pic)
        {
            CTEFArtContestEntities dbc = new CTEFArtContestEntities();
            dbc.Entry(pic).State = System.Data.Entity.EntityState.Modified;
            dbc.SaveChanges();
            return RedirectToAction("index");
        }
    }
}