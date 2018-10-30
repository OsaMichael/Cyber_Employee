using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Employee.Models
{
    public class SalaryModel
    {
        public int Id { get; set; }



        [Display(Name = "Input Salary")]
        public double salary { get; set; }
    }
}