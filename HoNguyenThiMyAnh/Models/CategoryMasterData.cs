using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoNguyenThiMyAnh.Models
{
    public class CategoryMasterData
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Hình đại diện")]
        public string Avatar { get; set; }
        [Display(Name = "Tên danh mục")]
        public string Slug { get; set; }
        [Display(Name = "Loại sản phẩm")]
        public Nullable<bool> ShowOnHomePage { get; set; }
        [Display(Name = "Thứ tự hiển thị")]
        public Nullable<int> DisplayOrder { get; set; }
        [Display(Name = "Ngày tạo")]

        public Nullable<bool> Deleted { get; set; }
        [Display(Name = "Đã xóa")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        [Display(Name = "Ngày cập nhật")]

        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }
      
    }
}