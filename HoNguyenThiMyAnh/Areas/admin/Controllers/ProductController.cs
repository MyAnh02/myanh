﻿using HoNguyenThiMyAnh.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoNguyenThiMyAnh.Areas.admin.Controllers
{
   
    public class ProductController : Controller
    {
        WEBBANHANGEntities dbObj = new WEBBANHANGEntities();
        // GET: admin/Product
        public ActionResult Index()
        {
            var lstProduct = dbObj.Products.ToList();
            return View(lstProduct);
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            try
            {
                if(objProduct.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                    fileName = fileName  + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objProduct.Avatar = fileName;
                    objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath(" ~/Content/image/"), fileName));
                }
                dbObj.Products.Add(objProduct);
                dbObj.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = dbObj.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

    }
}