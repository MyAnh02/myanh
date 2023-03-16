using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoNguyenThiMyAnh.Controllers
{
    public class ListingsController : Controller
    {
        // GET: Listings
        public ActionResult Grid()
        {
            return View();
        }
    }
}