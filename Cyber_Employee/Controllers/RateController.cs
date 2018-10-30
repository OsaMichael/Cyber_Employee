using Cyber_Employee.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cyber_Employee.Controllers
{
    public class RateController : Controller
    {
        private IStaffManager _istaffMgr;
        // GET: Rate
      public  RateController(IStaffManager istaffMgr)
        {
            _istaffMgr = istaffMgr;
        }
        public ActionResult Index()
        {
            var result = _istaffMgr.GetRatings();
            return View(result);
        }
    }
}