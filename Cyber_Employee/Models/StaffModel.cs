using Cyber_Employee.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Employee.Models
{
    public class StaffModel
    {
        public int StaffId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        [Display(Name = "Date of birth")]
        public string DOB { get; set; }
        public string Address { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }


        public virtual ICollection<ProjectModel> Projects { get; set; }
  
        public StaffModel()
        {
            new HashSet<ProjectModel>();
      
        }
        public StaffModel(Staff staff)
        {
            this.Assign(staff);
            Projects = new HashSet<ProjectModel>();
       
        }
        public Staff Create(StaffModel model)
        {
            return new Staff
            {
                 Name = model.Name,
                 Department = model.Department,
                 DOB = model.DOB,
                 Address = model.Address,
                 CreatedBy = model.CreatedBy,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };
        }
        public Staff Edit(Staff entity, StaffModel model)
        {
            entity.StaffId = model.StaffId;
            entity.Name = model.Name;
            entity.Department = model.Department;
            entity.DOB = model.DOB;
            entity.Address = model.Address;
            entity.ModifiedBy = model.ModifiedBy;
            //entity.CreatedBy = model.CreatedBy;
            entity.ModifiedDate = DateTime.Now;
            //entity.CreatedDate = DateTime.Now;
            return entity;
        }
    }
}