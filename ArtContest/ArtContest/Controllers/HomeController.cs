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
            if (dbc.Users.Where(u => u.UserName == model.userName).Select(u => u.UserTypeId).SingleOrDefault() == 1)
            {
                if (userList.Contains(model.userName) &&
                model.password == dbc.Users.Where(u => u.UserName == model.userName).Select(u => u.Password).Single())
                {
                    Session["userid"] = dbc.Users.Where(s => s.UserName == model.userName).Select(u => u.Id).SingleOrDefault();
                    return RedirectToAction("Index", "AdminAccount");
                }
                
            }
            if (dbc.Users.Where(u => u.UserName == model.userName).Select(u => u.UserTypeId).SingleOrDefault() == 2)
            {
                if (userList.Contains(model.userName) &&
                model.password == dbc.Users.Where(u => u.UserName == model.userName).Select(u => u.Password).Single())
                {
                    Session["userid"] = dbc.Users.Where(s => s.UserName == model.userName).Select(u => u.Id).SingleOrDefault();
                    return RedirectToAction("Index", "JudgeAccount");
                }
            }
                
            if(dbc.Users.Where(u => u.UserName == model.userName).Select(u => u.UserTypeId).SingleOrDefault() == 3) {
                model.PicPath = dbc.Pictures
                .Where(p => p.UserId == dbc.Users.Where(u => model.userName == u.UserName)
                .Select(u => u.Id).FirstOrDefault()).Select(p => p.PicturePath).ToList();
                if (userList.Contains(model.userName) &&
                model.password == dbc.Users.Where(u => u.UserName == model.userName).Select(u => u.Password).Single())
                {
                    Session["userid"] = dbc.Users.Where(s => s.UserName == model.userName).Select(u => u.Id).SingleOrDefault();
                    return RedirectToAction("Index", "StudentAccount");
                }
            }

            return View();
        }
    }
}