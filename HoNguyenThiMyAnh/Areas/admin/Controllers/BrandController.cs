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
    public class BrandController : Controller
    {
        WEBBANHANGEntities dbObj = new WEBBANHANGEntities();

        // GET: admin/Brand
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstBrand = new List<Brand>();
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
                lstBrand = dbObj.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstBrand = dbObj.Brands.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstBrand = lstBrand.OrderByDescending(n => n.id).ToList();
            return View(lstBrand.ToPagedList(pageNumber, pageSize));


        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var lstBrand = dbObj.Brands.Where(n => n.id == id).FirstOrDefault();
            return View(lstBrand);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = dbObj.Brands.Where(n => n.id == id).FirstOrDefault();
            return View(objBrand);

        }
        [HttpPost]
        public ActionResult Delete(Brand objBran)
        {

            var objBrand = dbObj.Brands.Where(n => n.id == objBran.id).FirstOrDefault();
            dbObj.Brands.Remove(objBrand);
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            var objBrand = dbObj.Brands.Where(n => n.id == id).FirstOrDefault();

            return View(objBrand);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Brand objBrand)
        {
            if (ModelState.IsValid)
            {
                if (objBrand.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                    string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                    fileName = fileName + extension;
                    objBrand.Avatar = fileName;
                    objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                }
                objBrand.UpdatedOnUtc = DateTime.Now;
                dbObj.Entry(objBrand).State = System.Data.Entity.EntityState.Modified;
                dbObj.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Create()
        {


            return View();


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand objBrand)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    if (objBrand.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                        string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objBrand.Avatar = fileName;
                        objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objBrand.CreatedOnUtc = DateTime.Now;
                    dbObj.Brands.Add(objBrand);
                    dbObj.SaveChanges();
                    return RedirectToAction("Index");
                }

                catch (Exception)

                {
                    return RedirectToAction("Index");
                }
            }

            return View(objBrand);
        }

    }
}