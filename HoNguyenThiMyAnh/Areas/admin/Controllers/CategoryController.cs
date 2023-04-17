using HoNguyenThiMyAnh.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoNguyenThiMyAnh.Areas.admin.Controllers
{
    public class CategoryController : Controller
    {
        WEBBANHANGEntities dbObj = new WEBBANHANGEntities();

        // GET: admin/Category
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstCategory = new List<Category>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstCategory = dbObj.Categories.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstCategory = dbObj.Categories.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstCategory = lstCategory.OrderByDescending(n => n.id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));

          
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var lstCategory = dbObj.Categories.Where(n => n.id == id).FirstOrDefault();
            return View(lstCategory);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = dbObj.Categories.Where(n => n.id == id).FirstOrDefault();
            return View(objCategory);

        }
        [HttpPost]
        public ActionResult Delete(Category objCate)
        {

            var objCategory = dbObj.Categories.Where(n => n.id == objCate.id).FirstOrDefault();
            dbObj.Categories.Remove(objCategory);
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            var objCategory = dbObj.Categories.Where(n => n.id == id).FirstOrDefault();

            return View(objCategory);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id,Category objCategory )
        {
            if (ModelState.IsValid)
            {
                if (objCategory.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                    string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                    fileName = fileName + extension;
                    objCategory.Avatar = fileName;
                    objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                }
                objCategory.UpdatedOnUtc = DateTime.Now;
                dbObj.Entry(objCategory).State = System.Data.Entity.EntityState.Modified;
                dbObj.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Create()
        {


            return View();


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    if (objCategory.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                        string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objCategory.Avatar = fileName;
                        objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objCategory.CreatedOnUtc = DateTime.Now;
                    dbObj.Categories.Add(objCategory);
                    dbObj.SaveChanges();
                    return RedirectToAction("Index");
                }

                catch (Exception)

                {
                    return RedirectToAction("Index");
                }
            }

            return View(objCategory);
        }

    }
}
