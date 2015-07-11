using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FDI.DA.Admin;
using FDI.Simple;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace FDI.Areas.Admin.Controllers
{
    public class NewsCommentHistoryController : BaseController
    {
        readonly News_CommentDA _commentDA = new News_CommentDA("#");
        readonly CustomerDA _customerDA = new CustomerDA("#");
        readonly News_CategoryDA _newsCategory = new News_CategoryDA();
        //
        // GET: /Admin/NewsCommentHistory/

        public ActionResult Index()
        {
            var ltsAdmin = _customerDA.GetCustomerIsAdmin();
            var listItem = _newsCategory.GetListSimpleAll(true);
            ViewData.Model = listItem;
            ViewBag.ltsAdmin = ltsAdmin;
            return View();
        }
        public ActionResult ListItems()
        {

            string emailCustomer = Request["EmailCustomer"];
            var ltsTemp = new List<int>();
            if (!string.IsNullOrEmpty(Request["Admin"]))
            {
                try
                {
                    ltsTemp = Request["Admin"].Contains(",") ? Request["Admin"].Trim().Split(',').Select(o => Convert.ToInt32(o)).ToList() : new List<int> { Convert.ToInt32(Request["Admin"]) };
                }
                catch (Exception)
                {
                    ltsTemp = new List<int>();
                }
            }

            var listItem = _commentDA.GetListSimpleByEmail(Request, emailCustomer, ltsTemp);
            var model = new ModelNewsCommentItem
            {
                SystemActionItem = systemActionItem,
                ListItem = listItem,
                PageHtml = _commentDA.GridHtmlPage
            };
            ViewData.Model = model;
            return View();
        }
        public virtual void ExportReportToExcel(string filePath, List<NewsCommentItem> report)
        {
            var newFile = new FileInfo(filePath);

            // ok, we can run the real code of the sample now
            int dem = 0;
            using (var xlPackage = new ExcelPackage(newFile))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 
                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("NewsCommentHistory");
                xlPackage.Workbook.CalcMode = ExcelCalcMode.Manual;
                //Create Headers and format them
                var properties = new string[]
                    {
                        "Tiêu đề",
                        "Khách hàng",
                        "Trạng thái phê duyệt",
                        "Được tạo vào",
                        "Đạt KPI"
                    };
                for (var i = 0; i < properties.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = properties[i];
                    worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                }

                var row = 2;
                foreach (var item in report)
                {
                    dem++;
                    int col = 1;
                    ////order properties
                    //worksheet.Cells[row, col].Value = dem;
                    //col++;



                    worksheet.Cells[row, col].Value = item.Title;
                    col++;
                    //tenkh
                    worksheet.Cells[row, col].Value = item.Email;
                    col++;
                    worksheet.Cells[row, col].Value = (item.IsShow) ? "Hiển thị" : "Ẩn";
                    col++;
                    if (item.DateCreated != null)
                        worksheet.Cells[row, col].Value = item.DateCreated.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    col++;
                    worksheet.Cells[row, col].Value = (item.DatKPI.HasValue && item.DatKPI.Value) ? "Đạt" : "không Đạt";

                    col++;
                    //next row
                    row++;
                }

                // we had better add some document properties to the spreadsheet 
                // set some core property values
                var nameexcel = "Danh sách tồn kho" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                xlPackage.Workbook.Properties.Title = string.Format("{0} reports", nameexcel);
                xlPackage.Workbook.Properties.Author = "Admin-IT";
                xlPackage.Workbook.Properties.Subject = string.Format("{0} reports", "");
                //xlPackage.Workbook.Properties.Keywords = string.Format("{0} orders", _storeInformationSettings.StoreName);
                xlPackage.Workbook.Properties.Category = "Report";
                //xlPackage.Workbook.Properties.Comments = string.Format("{0} orders", _storeInformationSettings.StoreName);

                // set some extended property values
                xlPackage.Workbook.Properties.Company = "FDI";
                //xlPackage.Workbook.Properties.HyperlinkBase = new Uri(_storeInformationSettings.StoreUrl);
                // save the new spreadsheet
                xlPackage.Save();
            }
        }
        public ActionResult XuatExcelHistory()
        {

            string emailCustomer = Request["EmailCustomer"];
            var ltsTemp = new List<int>();
            if (!string.IsNullOrEmpty(Request["Admin"]) && Request["Admin"] != null)
            {
                try
                {

                    ltsTemp = Request["Admin"].Contains(",") ? Request["Admin"].Trim().Split(',').Select(o => Convert.ToInt32(o)).ToList() : new List<int> { Convert.ToInt32(Request["Admin"]) };
                }
                catch (Exception)
                {
                    ltsTemp = new List<int>();
                }
            }
            var ltsListComment = _commentDA.GetListSimpleByEmailNoPaging(Request, emailCustomer, ltsTemp);

            var fileName = string.Format("thong-ke-binh-luan_{0}.xlsx", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
            var filePath = Path.Combine(Request.PhysicalApplicationPath, "File\\ExportImport", fileName);
            var folder = Request.PhysicalApplicationPath + "File\\ExportImport";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            ExportReportToExcel(filePath, ltsListComment);
            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "text/xls", fileName);
        }
    }
}
