using LinqToExcel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Employee.Models
{
    public class DataModel
    {
        [ExcelColumn("Monthly")]
        public int Monthly { get; set; }

        [ExcelColumn("Rate")]
        public double Rate { get; set; }



// To read excel data from a folder not from db. do the following

//Create a class: DataModel,
//SalaryModel for the view,
//create a class ConnectionExcel,
//create class PayRepository

    }
}