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
    public class Info_User_LoginSSO
    {
        public string Success { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string Address { get; set; }
        public string OwnerCode { get; set; }
        public Boolean Gender { get; set; }
        public string Avatar { get; set; }
        public Boolean Verifyed { get; set; }
        public string IdentifierCode { get; set; }
        public int ErrCode { get; set; }
    }

}
