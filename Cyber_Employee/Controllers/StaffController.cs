using Cyber_Employee.Entities;
using Cyber_Employee.Interface;
using Cyber_Employee.Models;
using Microsoft.AspNet.Identity;
using OfficeOpenXml;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cyber_Employee.Controllers
{
    public class StaffController : Controller
    {
        private IStaffManager _context;
        //private IExcelProcessor _excel;
        public StaffController(IStaffManager context)
        {
            _context = context;
        }
        //the computePay read data from the excel folder
        [HttpGet]
        public ActionResult ComputePaye()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ComputePaye(SalaryModel data)
        {
            var inputs = data.salary;


            var taxing = PayRepository.ComputePay(inputs);
            var takehome = inputs - taxing;
            ViewBag.balance = takehome;
            ViewBag.tax = taxing;

            return View();

        }

        // the compute action read the data from the database whhile the computePay read data from the excel folder
        public ActionResult Compute()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Compute(decimal Salary)
        {
            Decimal tax1, tax2, tax3, tax4, tax5, tax6 = 0;
            Decimal totaldeduction, balance = 0;
            Decimal[] tax = new Decimal[6];


            Decimal takehome = 0;

            Decimal[] total = new Decimal[6];

            var myarray = _context.GetRatings();

            //int row = 6, col = 2;
            //Decimal[,] myarray = new Decimal[row, col];
            //myarray[0, 0] = 25000; myarray[0, 1] = 7;//rate

            //myarray[1, 0] = 25000; myarray[1, 1] = 11;

            //myarray[2, 0] = 41666; myarray[2, 1] = 15;

            //myarray[3, 0] = 41666; myarray[3, 1] = 19;

            //myarray[4, 0] = 133333; myarray[4, 1] = 21;

            //myarray[5, 0] = 266666; myarray[5, 1] = 24;

            if (Salary > myarray.Result[0].Amount)
            {
                tax1 = (myarray.Result[0].Amount * (myarray.Result[0].RatePercentage) / 100);
                balance = Salary - myarray.Result[0].Amount;
                tax[0] = tax1;
                total[0] = tax1;
                takehome = Salary - tax1;
                if (balance > myarray.Result[1].Amount)
                {
                    balance = balance - myarray.Result[1].Amount;
                    tax2 = (myarray.Result[1].Amount * (myarray.Result[1].RatePercentage) / 100);
                    totaldeduction = tax1 + tax2;
                    total[1] = totaldeduction;
                    tax[1] += tax2;
                    takehome = Salary - total[1];
                    if (balance > myarray.Result[2].Amount)
                    {
                        balance = balance - myarray.Result[2].Amount;
                        tax3 = (myarray.Result[2].Amount * (myarray.Result[2].RatePercentage) / 100);
                        totaldeduction = tax1 + tax2 + tax3;
                        total[2] = totaldeduction;
                        tax[2] += tax3;
                        takehome = Salary - total[2];
                        if (balance > myarray.Result[3].Amount)
                        {
                            balance = balance - myarray.Result[3].Amount;
                            tax4 = (myarray.Result[3].Amount * (myarray.Result[3].RatePercentage) / 100);
                            totaldeduction = tax1 + tax2 + tax3 + tax4;
                            total[3] = totaldeduction;
                            tax[3] += tax4;
                            takehome = Salary - total[3];

                            if (balance > myarray.Result[4].Amount)
                            {
                                balance = balance - myarray.Result[4].Amount;
                                tax5 = (myarray.Result[4].Amount * (myarray.Result[4].RatePercentage) / 100);
                                totaldeduction = tax1 + tax2 + tax3 + tax4 + tax5;
                                total[4] = totaldeduction;
                                takehome = Salary - total[4];
                                tax[4] += tax5;
                                if (balance > myarray.Result[5].Amount)
                                {
                                    balance = balance - myarray.Result[5].Amount;
                                    tax6 = (myarray.Result[5].Amount * (myarray.Result[5].RatePercentage) / 100);
                                    totaldeduction = tax1 + tax2 + tax3 + tax4 + tax5 + tax6;
                                    tax[5] += tax6;

                                    total[5] = totaldeduction;
                                    takehome = Salary - total[5];
                                }
                            }
                        }
                    }

                }


                for (int j = 0; j < tax.Length; j++)
                {
                    if (tax[j] == 0)
                        break;
                    //Console.Write("tax = {0}", tax[j]);
                    //Console.WriteLine("Commulative tax = " + total[j] + "  ");
                    //Console.WriteLine("Balance = " + balance);
                    //Console.WriteLine("takehome = " + takehome);

                    ViewBag.Tax = total[j];

                }


            }

            ViewBag.Balance = balance;


            return View();
        }

        public ActionResult Index()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Success = (string)TempData["message"];
            }
            var results = _context.GetStaffs();
            if (results.Succeeded == true)
            {
                return View(results.Unwrap());
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occure");
                return View();
            }

        }

        public ActionResult Export()
        {
            var list = _context.GetStaffs().Result;

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "StaffId";
            ws.Cells["B1"].Value = "Name";
            ws.Cells["C1"].Value = "Department";
            ws.Cells["D1"].Value = "DOB";
            ws.Cells["E1"].Value = "Address";

            int rowStart = 2;
            foreach (var item in list)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.StaffId;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Name;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Department;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.DOB;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Address;
                rowStart++;
            }

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

            return RedirectToAction("Index");


        }



        public ActionResult Pdf()
        {
            var file = _context.GetStaffs().Result;

            var viewAsPdf = new ViewAsPdf("Index", file)

            {
                FileName = "StaffProject.pdf"
            };

            return viewAsPdf;
        }
        [HttpGet]
        public ActionResult CreateStaff()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateStaff(StaffModel model)
        {
            model.CreatedBy = User.Identity.GetUserName();
            var result = _context.CreateStaff(model);
            if (result.Succeeded == true)
            {
                TempData["message"] = $"staff{model.Name} was successfully added!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult EditStaff(int id = 0)
        {
            var result = _context.GetStaffById(id);
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
        public ActionResult EditStaff(StaffModel model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedBy = User.Identity.GetUserName();

                model.CreatedBy = User.Identity.GetUserName();
                var result = _context.UpdateStaff(model);
                if (result.Succeeded)
                {
                    TempData["message"] = $"Restaurant{model.Name} was successfully added!";
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
        public JsonResult DeleteStaff(int id, string staffName)
        {
            int restId = Convert.ToInt32(id);
            if (restId > 0)
            {
                var result = _context.DeleteStaff(restId);
                if (result.Succeeded == true)
                {


                    return Json(new { status = true, message = $" {staffName} has been successfully deleted!", JsonRequestBehavior.AllowGet });
                }
                return Json(new { status = false, error = result.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false, error = "Invalid Id" }, JsonRequestBehavior.AllowGet);
        }

    }
}