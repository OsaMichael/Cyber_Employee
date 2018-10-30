using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Employee.Entities
{
    public partial class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public int StaffId { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Staff Staff { get; set; }
    }
}