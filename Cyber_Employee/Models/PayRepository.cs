using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Employee.Models
{
    public class PayRepository
    {
        private static Dictionary<double, double> dictionary;


        public static double ComputePay(double salary)
        {
            string pathToExcelFile = @"D:\Salary.xlsx";
            //string pathToExcelFile = @"D:\ExcelData.xlsx";
            ConnectionExcel connection = new ConnectionExcel(pathToExcelFile);
            var query = (from a in connection.UrlConnection.Worksheet<DataModel>()
                         select a).ToArray();
            Double deductable = salary;
            var balance = salary;
            double tax = 0;
            dictionary = new Dictionary<double, double>();

            for (int i = 0; i < query.Length; i++)
            {
                var Tax = query[i].Monthly;
                var Rate = query[i].Rate;

                if (deductable < Tax)
                {
                    break;
                }
                if (deductable >= Tax)
                {
                    deductable = salary - Tax;
                    tax = tax + (Rate * Tax);
                    balance = salary - tax;
                    dictionary.Add(balance, tax);
                }
            }

            return tax;

        }
    }
}