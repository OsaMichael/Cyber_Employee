using Cyber_Employee.Interface;
using Cyber_Employee.Manager;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Employee.Infrastructure
{
    public class Manager : NinjectModule
    {
        public override void Load()
        {

            Bind<IStaffManager>().To<StaffManager>();
            Bind<IProjectManager>().To<ProjectManager>();

        }
    }
}