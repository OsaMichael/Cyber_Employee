using Cyber_Employee.Interface;
using Cyber_Employee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cyber_Employee.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectManager _context;
        private IStaffManager _staffMgr;
      
        public ProjectController(IProjectManager context, IStaffManager staffMgr)
        {
            _context = context;
            _staffMgr = staffMgr;
        }
        // GET: Project

        public ActionResult Index( string searchBy, string search)
        {
            if (TempData["message"] != null)
            {
                ViewBag.Success = (string)TempData["message"];
            }
            //var results = _staffMgr.GetStaffs();
     
            var results = _context.GetProjects();

            
            if (searchBy == "Name")
            {
                //
                var staff = results.Result.Where(x => x.Staff.Name.ToLower().Contains(search.ToLower())).ToList();
                return View(staff);

            }

            if (results.Succeeded == true)
            {
                ////ADDED ARRANGE NAMES ALPHABETICAL ORDER
                return View(results.Unwrap().OrderBy(c => c.Staff.Name).ToList());
                //  return View(results.Unwrap().OrderBy(c => c.StaffName).ToPagedList(page ?? 1, 12));
            }

            else
            {
                ModelState.AddModelError(string.Empty, "An error occure");
                return View(results.Unwrap());
            }


        }
        //public ActionResult Index()
        //{
        //    if (TempData["message"] != null)
        //    {
        //        ViewBag.Success = (string)TempData["message"];
        //    }
        //    var results = _context.GetProjects();
        //    if (results.Succeeded == true)
        //    {
        //        return View(results.Unwrap());
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "An error occure");
        //        return View();
        //    }

        //}
        [HttpGet]
        public ActionResult CreateProject()
        {
            ViewBag.staff = new SelectList(_staffMgr.GetStaffs().Result, "StaffId", "Name");

            return View();
        }
        [HttpPost]
        public ActionResult CreateProject(ProjectModel model)
        {
            ViewBag.staff = new SelectList(_staffMgr.GetStaffs().Result, "StaffId", "Name");
            //model.CreatedBy = User.Identity.GetUserName();
            var result = _context.CreateProject(model);
            if (result.Succeeded == true)
            {
                TempData["message"] = $"project{model.StaffId} was successfully added!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult EditProject(int id = 0)
        {
            var result = _context.GetProjectById(id);
            if (result.Succeeded)
            {
                return View(result.Result);
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();

            }
        }
        [HttpPost]
        public ActionResult EditProject(ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                //model.ModifiedBy = User.Identity.GetUserName();

                //model.CreatedBy = User.Identity.GetUserName();
                var result = _context.UpdateProject(model);
                if (result.Succeeded)
                {
                    TempData["message"] = $"Restaurant{model.StaffId} was successfully added!";
                    ModelState.AddModelError(string.Empty, "Update was successfully ");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View(model);

                }
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult DeleteProject(int id, string ProjectName)
        {
            int restId = Convert.ToInt32(id);
            if (restId > 0)
            {
                var result = _context.DeleteProject(restId);
                if (result.Succeeded == true)
                {


                    return Json(new { status = true, message = $" {ProjectName} has been successfully deleted!", JsonRequestBehavior.AllowGet });
                }
                return Json(new { status = false, error = result.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false, error = "Invalid Id" }, JsonRequestBehavior.AllowGet);
        }

    }
}