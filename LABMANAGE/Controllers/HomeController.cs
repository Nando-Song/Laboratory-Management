using LABMANAGE.Service.QQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LABMANAGE.Controllers
{
    public class HomeController : Controller
    {
        public IQQService qqService { get; set; }
        // GET: Home
        public ActionResult Index()
        {
            
            return View();
        }
    }
}