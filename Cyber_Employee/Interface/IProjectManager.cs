using Cyber_Employee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Employee.Interface
{
    public interface IProjectManager
    {
        Operation<ProjectModel> CreateProject(ProjectModel model);
        Operation<ProjectModel[]> GetProjects();
        Operation<ProjectModel> UpdateProject(ProjectModel model);
        Operation<ProjectModel> GetProjectById(int projectId);
        System.Operation DeleteProject(int id);
        //Operation<ProjectModel[]> GetProjectByStaffId(int staffId);
    }
}