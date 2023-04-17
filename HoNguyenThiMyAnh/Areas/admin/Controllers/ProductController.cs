using HoNguyenThiMyAnh.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static HoNguyenThiMyAnh.Common;
using static HoNguyenThiMyAnh.Common.ListtoDataTableConverter;

namespace HoNguyenThiMyAnh.Areas.admin.Controllers
{
    
    public class ProductController : Controller
    {
        WEBBANHANGEntities dbObj = new WEBBANHANGEntities();
        // GET: admin/Product
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<Product>();
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
                lstProduct = dbObj.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = dbObj.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));


        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var lstProduct = dbObj.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(lstProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var lstProduct = dbObj.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(lstProduct);

        }
        [HttpPost]
        public ActionResult Delete(Product lstPro)
        {

            var lstProduct = dbObj.Products.Where(n => n.Id == lstPro.Id).FirstOrDefault();
            dbObj.Products.Remove(lstProduct);
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var lstProduct = dbObj.Products.Where(n => n.Id == id).FirstOrDefault();

            return View(lstProduct);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Product lstProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                if (lstProduct.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(lstProduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(lstProduct.ImageUpload.FileName);
                    fileName = fileName + extension;
                    lstProduct.Avatar = fileName;
                    lstProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                }
                lstProduct.UpdatedOnUtc = DateTime.Now;
                dbObj.Entry(lstProduct).State = System.Data.Entity.EntityState.Modified;
                dbObj.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(lstProduct);
        }
        [HttpGet]
        public ActionResult Create()
        {

            this.LoadData();
            return View();


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product lstProduct)
        {
            this.LoadData();

            if (ModelState.IsValid)
            {
                try
                {
                    if (lstProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(lstProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(lstProduct.ImageUpload.FileName);
                        fileName = fileName + extension;
                        lstProduct.Avatar = fileName;
                        lstProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    lstProduct.CreatedOnUtc = DateTime.Now;
                    dbObj.Products.Add(lstProduct);
                    dbObj.SaveChanges();
                    return RedirectToAction("Index");
                }

                catch (Exception)

                {
                    return RedirectToAction("Index");
                }
            }

            return View(lstProduct);
        }

        void LoadData()
        {
            Common objCommon = new Common();
            //lây dữ liệu db

            var lstCat = dbObj.Categories.ToList();
            //conver sang select
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);

            ViewBag.ListCategory = objCommon.ToselectList(dtCategory, "id", "Name");
            //lấy dữ liệu thuowg hiêu

            var lstBrand = dbObj.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            //convert sang select

            ViewBag.ListBrand = objCommon.ToselectList(dtBrand, "id", "Name");

            List<ProductType> lstProductType = new List<ProductType>();

            ProductType objProductType = new ProductType();
            objProductType.Id = 1;
            objProductType.Name = "Giảm giá sốc";

            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 2;
            objProductType.Name = "Đề xuất";

            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);

            ViewBag.ProductType = objCommon.ToselectList(dtProductType, "id", "Name");
        }

    }
}