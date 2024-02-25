using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class AuthModel
    {
        public int c_userid{ get; set; }   
        public string? c_name{ get; set; }   

        public string? c_email{ get; set; }   

        public string? c_password{ get; set; }   

        public int c_role{ get; set; }   

    }
}