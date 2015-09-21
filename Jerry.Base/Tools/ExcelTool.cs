using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Spire.Xls;

namespace Jerry.Base.Tools
{
    public class ExcelTool
    {
        #region NPIO方法 Excel API class Library to Operate Npoi Excel

        /// <summary>
        /// DataTable To Excel
        /// </summary>
        public static void SaveToExcel(DataTable dt, string path)
        {
            var workbook = GetWorkBook(dt);
            workbook.SaveToFile(path);
        }

        /// <summary>
        /// DataTable Export To Excel
        /// </summary>
        public static void ExportToExcel(DataTable dt, string fileName)
        {
            var workbook = GetWorkBook(dt);
            var resopne = HttpContext.Current.Response;
            workbook.SaveToHttpResponse(fileName, resopne);
        }


        /// <summary>
        /// 获取WorkBook
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static Workbook GetWorkBook(DataTable dt)
        {
            //Creates workbook
            Workbook workbook = new Workbook();
            //Gets first worksheet
            Worksheet sheet = workbook.Worksheets[0];
            //Insert DataTable into sheet
            sheet.InsertDataTable(dt, true, 1, 1);
            return workbook;
        }

        /// <summary>
        /// Excel To DataTable
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(string path)
        {
            //Create a new workbook
            var workbook = new Workbook();
            //Load a file and imports its data
            workbook.LoadFromFile(path);
            //Initialize worksheet
            var sheet = workbook.Worksheets[0];
            // get the data source that the grid is displaying data for
            return sheet.ExportDataTable();
        }

        #endregion

    }
}
