using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CNPM_DOAN.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Tên tài khoản không được rỗng")]
        [MinLength(10,ErrorMessage ="Tên tài khoản ít nhất 10 ký tự")]
        public string username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được rỗng")]
        [MinLength(10, ErrorMessage = "Tên tài khoản ít nhất 10 ký tự")]
        [DataType("password")]
        public string password { get; set; }
    }
}