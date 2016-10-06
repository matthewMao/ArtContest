using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtContest.Models;

namespace ArtContest.Controllers
{
    public class AdminAccountController : Controller
    {
        // GET: AdminAccount
        public ActionResult Index()
        {
            return View();
        }
    }
}