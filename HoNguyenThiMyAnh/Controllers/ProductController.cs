using HoNguyenThiMyAnh.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoNguyenThiMyAnh.Controllers
{
   
    public class ProductController : Controller
    {
        WEBBANHANGEntities objwEBBANHANGEntities = new WEBBANHANGEntities();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objwEBBANHANGEntities.Products.Where(n=>n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}