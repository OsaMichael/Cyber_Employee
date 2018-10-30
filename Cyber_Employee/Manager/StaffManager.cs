using Cyber_Employee.Interface;
using Cyber_Employee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Cyber_Employee.Manager
{

    public class StaffManager : IStaffManager
    {

        private ApplicationDbContext _context;
        public StaffManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public Operation<StaffModel> CreateStaff(StaffModel model)
        {
            return System.Operation.Create(() =>
            {
                try
                {
                    var isExists = _context.Staffs.Where(c => c.Name == model.Name /*c.StaffName == model.StaffName &&*//* c.StaffNo == model.StaffNo*/).FirstOrDefault();
                    if (isExists != null) throw new Exception("user email already exist");

                    //var exist = _context.Staffs.Where(e => e.StaffNo == model.StaffNo).FirstOrDefault();
                    //if (exist != null) throw new Exception("user staffno already exist");

                    var entity = model.Create(model);
                    _context.Staffs.Add(entity);
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

        public Operation<RateModel[]> GetRatings()
        {
            return System.Operation.Create(() =>
            {
                var entities = _context.Rates.ToList();

                var models = entities.Select(c => new RateModel()
                {
                      Amount = c.Amount,
                      RatePercentage = c.RatePercentage
                }
                ).ToArray();
                return models;
            });
        }
        public Operation<StaffModel[]> GetStaffs()
        {
            return System.Operation.Create(() =>
            {
                var entities = _context.Staffs.ToList();

                var models = entities.Select(c => new StaffModel(c)
                {
                    // this are fk to aviod the id displaying in the UI
                    //User = new ApplicationUser(c.User)
                    Name = c.Name,
                    Address = c.Address,
                    Department = c.Department,
                    DOB = c.DOB,
                    CreatedBy = c.CreatedBy,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                  
                    
                }
                ).ToArray();
                return models;
            });
        }
        public Operation<StaffModel> UpdateStaff(StaffModel model)
        {
            return System.Operation.Create(() =>
            {
                //model.Validate();
                var isExist = _context.Staffs.Find(model.StaffId);
                if (isExist == null) throw new Exception("staff does not exist");

                var entity = model.Edit(isExist, model);
                _context.Entry(entity);
                _context.SaveChanges();
                return model;
            });
        }
        public Operation<StaffModel> GetStaffById(int staffId)
        {
            return System.Operation.Create(() =>
            {
                var entity = _context.Staffs.Where(c => c.StaffId == staffId).FirstOrDefault();
                if (entity == null) throw new Exception("Voter does not exist");
                return new StaffModel(entity);

            });
        }

        //public System.Operation Details(int id)
        //{
        //    return System.Operation.Create(() =>
        //    {
        //        var entity = _context.Staffs.Include(s => s.VoterId == id).FirstOrDefault();
        //        if (entity == null) throw new Exception("Voter does not  exist");
        //        return new VoterModel(entity);
        //    });
        //}
        public System.Operation DeleteStaff(int id)
        {
            return System.Operation.Create(() =>
            {
                var entity = _context.Staffs.Find(id);
                if (entity == null) throw new Exception("Voter does not exist");

                _context.Staffs.Remove(entity);
                _context.SaveChanges();
            });
        }
    }
}