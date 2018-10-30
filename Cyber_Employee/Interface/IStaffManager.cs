using Cyber_Employee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Employee.Interface
{
    public interface IStaffManager
    {
        Operation<StaffModel> CreateStaff(StaffModel model);
        Operation<StaffModel[]> GetStaffs();
        Operation<StaffModel> UpdateStaff(StaffModel model);
        Operation<StaffModel> GetStaffById(int staffId);
        System.Operation DeleteStaff(int id);
        Operation<RateModel[]> GetRatings();

    }
}