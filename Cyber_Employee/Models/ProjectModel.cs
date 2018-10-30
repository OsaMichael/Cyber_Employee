using Cyber_Employee.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Employee.Models
{
    public class ProjectModel
    {
        public int ProjectId { get; set; }
        public int StaffId { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual StaffModel Staff { get; set; }


        public ProjectModel()
        {
            new StaffModel();
        
        }
        public ProjectModel(Project project)
        {
            this.Assign(project);
            Staff = new StaffModel();
        

        }
        public Project Create(ProjectModel model)
        {
            return new Project
            {
                StaffId = model.StaffId,
                ProjectDescription = model.ProjectDescription,
                StartDate = model.StartDate,
                EndDate = model.EndDate

            };
        }
     
        public Project Edit(Project entity, ProjectModel model)
        {
            entity.ProjectId = model.ProjectId;
            entity.StaffId = model.StaffId;
            entity.ProjectDescription = model.ProjectDescription;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
          
            return entity;
        }
    }
}