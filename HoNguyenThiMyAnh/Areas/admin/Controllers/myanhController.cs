using HoNguyenThiMyAnh.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoNguyenThiMyAnh.Areas.admin.Controllers
{
    public class myanhController : Controller
    {
        WEBBANHANGEntities dbObj = new WEBBANHANGEntities();
        // GET: admin/myanh
        public ActionResult Index()
        {
            var lstProduct = dbObj.Products.ToList();
            return View(lstProduct);
        }
    }
}