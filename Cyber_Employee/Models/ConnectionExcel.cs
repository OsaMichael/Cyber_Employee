using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Employee.Models
{
    public class ConnectionExcel
    {
        public string _pathExcelFile;
        public ExcelQueryFactory _urlConnection;

        public ConnectionExcel(string path)
        {
            this._pathExcelFile = path;
            this._urlConnection = new ExcelQueryFactory(_pathExcelFile);
        }

        public string PathExcelFile
        {
            get { return _pathExcelFile; }
        }

        public ExcelQueryFactory UrlConnection
        {
            get { return _urlConnection; }
        }
    }
}