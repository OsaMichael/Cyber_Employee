using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Employee.Entities
{
    public class Rate
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int RatePercentage { get; set; }
    }
}