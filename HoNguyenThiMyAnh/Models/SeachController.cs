using HoNguyenThiMyAnh.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoNguyenThiMyAnh.Models
{
    public class SeachController
    {
        WEBBANHANGEntities objWEBBANHANGEntities = new WEBBANHANGEntities();

        public List<Product> SearchByKey(string key)
        {
            return objWEBBANHANGEntities.Products.SqlQuery("Select * From Product Where Name like '%" + key + "%'").ToList();
        }
        }
    }