using System;
using System.ComponentModel.DataAnnotations;

namespace HoNguyenThiMyAnh.Context
{
    internal class UserMasterData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public Nullable<bool> IsAdmin { get; set; }

        public Nullable<bool> Sex { get; set; }
    }
}