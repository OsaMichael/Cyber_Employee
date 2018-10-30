using Cyber_Employee.Interface;
using Cyber_Employee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Employee.Manager
{
    public class ProjectManager : IProjectManager
    {
        private ApplicationDbContext _context;
        public ProjectManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public Operation<ProjectModel> CreateProject(ProjectModel model)
        {
            return System.Operation.Create(() =>
            {
                try
                {
                    //var isExists = _context.Staffs.Where(c => c.Email == model.Email /*c.StaffName == model.StaffName &&*//* c.StaffNo == model.StaffNo*/).FirstOrDefault();
                    //if (isExists != null) throw new Exception("user email already exist");

                    //var exist = _context.Staffs.Where(e => e.StaffNo == model.StaffNo).FirstOrDefault();
                    //if (exist != null) throw new Exception("user staffno already exist");

                    var entity = model.Create(model);
                    _context.Projects.Add(entity);
                    _context.SaveChanges();

                }
                catch (Exception xe)
                {
                    throw xe;
                }
                //model.Validate();

                return model;
            });
        }
        public Operation<ProjectModel[]> GetProjects()
        {
            return System.Operation.Create(() =>
            {
                var entities = _context.Projects.ToList();

                var models = entities.Select(c => new ProjectModel(c)
                {
                    Staff = new StaffModel(c.Staff),
                    // this are fk to aviod the id displaying in the UI
                    //User = new ApplicationUser(c.User)
                    StaffId = c.StaffId,
                   ProjectDescription = c.ProjectDescription,
                    StartDate = c.StartDate,
                     EndDate = c.EndDate
                }
                ).ToArray();
                return models;
            });
        }
        public Operation<ProjectModel> UpdateProject(ProjectModel model)
        {
            return System.Operation.Create(() =>
            {
                //model.Validate();
                var isExist = _context.Projects.Find(model.ProjectId);
                if (isExist == null) throw new Exception("staff does not exist");

                var entity = model.Edit(isExist, model);
                _context.Entry(entity);
                _context.SaveChanges();
                return model;
            });
        }
        public Operation<ProjectModel> GetProjectById(int projectId)
        {
            return System.Operation.Create(() =>
            {
                var entity = _context.Projects.Where(c => c.ProjectId == projectId).FirstOrDefault();
                if (entity == null) throw new Exception("Voter does not exist");
                return new ProjectModel(entity);

            });
        }
        //public Operation<ProjectModel[]> GetProjectByStaffId(int staffId)
        //{
        //    return System.Operation.Create(() =>
        //    {
        //        var entity = _context.Projects.Where(c => c.StaffId == staffId).ToList();
        //        if (entity == null) throw new Exception("Voter does not exist");

        //        return entity.Select(d => new ProjectModel(d)).ToArray();
        //        //return new ProjectModel(entity);

        //    });
        //}

        //public System.Operation Details(int id)
        //{
        //    return System.Operation.Create(() =>
        //    {
        //        var entity = _context.Staffs.Include(s => s.VoterId == id).FirstOrDefault();
        //        if (entity == null) throw new Exception("Voter does not  exist");
        //        return new VoterModel(entity);
        //    });
        //}
        public System.Operation DeleteProject(int id)
        {
            return System.Operation.Create(() =>
            {
                var entity = _context.Projects.Find(id);
                if (entity == null) throw new Exception("project does not exist");

                _context.Projects.Remove(entity);
                _context.SaveChanges();
            });
        }
    }
}