﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HoNguyenThiMyAnh.Context
{
    [MetadataType(typeof(UserMasterData))]
    public partial class User
    {


    }
    [MetadataType(typeof(ProductMasterData))]
    public partial class ProductMasterData
    {
        [NotMapped]

        public System.Web.HttpPostedFileBase ImageUpload { get; set; }

    }

}
