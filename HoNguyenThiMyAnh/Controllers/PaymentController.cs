using HoNguyenThiMyAnh.Context;
using HoNguyenThiMyAnh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoNguyenThiMyAnh.Controllers
{
    public class PaymentController : Controller
    {
        WEBBANHANGEntities objWEBBANHANGEntities = new WEBBANHANGEntities();

        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"]==null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];

                Order objOrder = new Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objWEBBANHANGEntities.Orders.Add(objOrder);

                //
                int intOrderId = objOrder.Id;
                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                foreach(var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);


                }
                objWEBBANHANGEntities.OrderDetails.AddRange(lstOrderDetail);
                objWEBBANHANGEntities.SaveChanges();
            }
            return View();
        }
    }
}