using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class EmployeeModel
    {
         public int c_empid { get; set; }
        public string? c_name { get; set; }
        public string? c_email { get; set; }
        public string? c_gender {get; set;}
        public DateTime c_dob { get; set; }
        public string? c_hobby{get;set;}
        public string? c_password { get; set; }
        public string? c_photo { get; set; }
        public int c_cityid { get; set; }
                public string? c_cityname { get; set; }
        public int c_departmentid {get; set;}
                public string? c_deptname { get; set; }

    }
}