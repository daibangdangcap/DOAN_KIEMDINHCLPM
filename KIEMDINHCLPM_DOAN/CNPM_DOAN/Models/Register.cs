using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CNPM_DOAN.Models
{
    public class Register
    {
        public NGUOIDUNG nguoidung { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string gender { get; set; }
        public string username { get; set; }
        public string repassword { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime NgaySinh { get; set; }
        public Register() { }
    }
}