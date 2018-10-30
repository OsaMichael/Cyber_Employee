using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Employee.Entities
{
    public partial class Staff
    {
        [Key]
        public int StaffId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        [Display(Name = "Date of birth")]
        public string DOB { get; set; }
        public string Address { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}