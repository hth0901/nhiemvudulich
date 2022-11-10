using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HueCit
{
    public class SYS_User
    {
        public int ID { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string HopThu { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int? Quyen { get; set; }
        public int TrangThai { get; set; }
    }

    public class SYS_UserTable
    {
        public int ID { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string HopThu { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int? Quyen { get; set; }
        public string Ten { get; set; }
        public int TrangThai { get; set; }
    }

    public class UserRequest
    {
        public string TuKhoa { get; set; }
        public int? Quyen { get; set; }
    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
