using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoNguyenThiMyAnh.Context;
using HoNguyenThiMyAnh.Models;

namespace HoNguyenThiMyAnh.Controllers
{
    public class CartController : Controller
    {
        WEBBANHANGEntities objWEBBANHANGEntities = new WEBBANHANGEntities();
        // GET: Card
        public ActionResult Index()
        {
            return View((List<CartModel>)Session["cart"]);
        }
        public ActionResult AddToCart(int Id, int quantity)
        {
            if (Session["Cart"] == null)
            {
                List<CartModel> cart = new List<CartModel>();
                cart.Add(new CartModel { Product = objWEBBANHANGEntities.Products.Find(Id), Quantity = quantity });
                Session["cart"] = cart;
                Session["count"] = 1;

            }
            else
            {
                List<CartModel> cart = (List<CartModel>)Session["Cart"];
                //kiem tra san pham co ton tai trong gio hang chua ??
                int index = isExist(Id);
             
                if(index != -1)
                {
                    //neu san pham ton tai trong gio hang thi cong them so luong
                    cart[index].Quantity += quantity;
                }
                else
                {
                    //neu khong ton tai thi them san pham vao gio hang
                    cart.Add(new CartModel { Product = objWEBBANHANGEntities.Products.Find(Id), Quantity = quantity });

                    //tinh lai so san pham trong gio hang
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;

                }
                Session["cart"] = cart;
            }
            return Json(new {Message = "Thành công ", JsonRequestBehavior.AllowGet});
        }
        private int isExist(int Id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.Id.Equals(Id))
                    return i;
            return -1;
        }
        //xoa san pham khoi gio hang
        public ActionResult Remove(int Id)
        {
            List<CartModel> li = (List<CartModel>)Session["cart"];
            li.RemoveAll(x => x.Product.Id == Id);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return Json(new { Message = "Thành công ", JsonRequestBehavior.AllowGet });
        }
    }
}