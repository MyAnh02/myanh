using HoNguyenThiMyAnh.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoNguyenThiMyAnh.Controllers
{
    public class CategoryController : Controller
    {
        WEBBANHANGEntities objwEBBANHANGEntities = new WEBBANHANGEntities();

        // GET: Category
        public ActionResult Category()
        {
            var lstCategory = objwEBBANHANGEntities.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var ListProduct = objwEBBANHANGEntities.Products.Where(n => n.CategoryId == Id).ToList();
            return View(ListProduct);
        }
    }
}