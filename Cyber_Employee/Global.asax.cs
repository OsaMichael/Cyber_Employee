using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using static Cyber_Employee.App_Start.Ninject;

namespace Cyber_Employee
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            NinjectContainer.RegisterAssembly();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
