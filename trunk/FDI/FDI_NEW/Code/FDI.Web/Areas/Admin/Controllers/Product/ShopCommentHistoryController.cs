using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using FDI.Business;
using FDI.DA.Admin;
using FDI.Simple;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using FDI.Web;

namespace FDI.Areas.Admin.Controllers
{
    public class ShopCommentHistoryController : BaseController
    {
        readonly Shop_CommentDA _shopCommentDA = new Shop_CommentDA("#");
        readonly CustomerDA _customerDA = new CustomerDA("#");
        //
        // GET: /Admin/ShopCommentHistory/

        public ActionResult Index()
        {
            var ltsAdmin = _customerDA.GetCustomerIsAdmin();
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

            ViewData.Model = _shopCommentDA.GetListSimpleByEmail(Request, emailCustomer, ltsTemp);
            ViewBag.PageHtml = _shopCommentDA.GridHtmlPage;
            return View();
        }

        public virtual void ExportReportToExcel(string filePath, List<ShopCommentItem> report)
        {
            var newFile = new FileInfo(filePath);

            // ok, we can run the real code of the sample now
            var dem = 0;
            using (var xlPackage = new ExcelPackage(newFile))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 
                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("ShopCommentHistory");
                xlPackage.Workbook.CalcMode = ExcelCalcMode.Manual;
                //Create Headers and format them
                var properties = new string[]
                    {
                        "Sản phẩm",
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

                    worksheet.Cells[row, col].Value = item.Product.Name;
                    col++;
                    //tenkh
                    worksheet.Cells[row, col].Value = item.Email;
                    col++;
                    worksheet.Cells[row, col].Value = item.IsActive != null && (item.IsActive.Value) ? "Đã duyệt" : "Chưa duyệt";
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
            var ltsListComment = _shopCommentDA.GetListSimpleByEmailNoPaging(Request, emailCustomer, ltsTemp);
            //#region Gán các giá trị vào biến dành cho trường hợp xuất ra word, excel
            //DataTable dtTable = ConvertLookupDataToTable(ltsListComment);
            //strArrWordInRowToBeReplaced = new string[]
            //                                          {
            //                                              "[STT]", "[SanPham]", "[KhachHang]","[TinhTrangPheDuyet]","[DuocTaoVao]","[DuocDuyetVao]","[ThoiGianChoDuyet]","[AdminPhuTrach]"
            //                                          };
            //strArrColumnTitle = new string[]
            //                                {
            //                                    "STT", "SanPham", "KhachHang","TinhTrangPheDuyet","DuocTaoVao","DuocDuyetVao","ThoiGianChoDuyet","AdminPhuTrach"
            //                                };

            //#endregion khai báo các tham số
            //Export export = new Export();
            //export.WriteFile(dtTable, "/Areas/Admin/ExportTemp/ExportThongKeCommentTable.xml",
            //                 "/Areas/Admin/ExportTemp/ExportThongKeCommentRows.xml",
            //                 "thong-ke-binh-luan",
            //                 strArrWordInRowToBeReplaced, strArrColumnTitle, "dd-MM-yyyy",
            //                 "application/vnd.ms-excel", ".xls", Response, Request);
            //msg.Message = "Xuất ra file Excel thành công";
            //msg.Erros = false;
            //return Json(msg, JsonRequestBehavior.AllowGet);
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
        /// <summary>
        /// Convert from lookupdata to table
        /// </summary>
        /// <param name="LtsLookupData"></param>
        /// <returns></returns>
        public DataTable ConvertLookupDataToTable(List<ShopCommentItem> LtsLookupData)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("STT", typeof(int));
            dataTable.Columns.Add("SanPham", typeof(string));
            dataTable.Columns.Add("KhachHang", typeof(string));

            dataTable.Columns.Add("TinhTrangPheDuyet", typeof(string));
            dataTable.Columns.Add("DuocTaoVao", typeof(string));
            dataTable.Columns.Add("DuocDuyetVao", typeof(string));
            dataTable.Columns.Add("ThoiGianChoDuyet", typeof(string));
            dataTable.Columns.Add("AdminPhuTrach", typeof(string));
            int i = 0;
            foreach (ShopCommentItem objShopCommentItem in LtsLookupData)
            {
                i++;
                DataRow dataRow = dataTable.NewRow();
                dataRow["STT"] = i;
                dataRow["SanPham"] = objShopCommentItem.Product.Name;
                dataRow["KhachHang"] = objShopCommentItem.Email;
                dataRow["TinhTrangPheDuyet"] = objShopCommentItem.IsActive != null && (objShopCommentItem.IsActive.Value) ? "Đã duyệt" : "Chưa duyệt";
                dataRow["DuocTaoVao"] = objShopCommentItem.DateCreated;
                dataRow["DuocDuyetVao"] = objShopCommentItem.NgayTraLoi;
                
                if (objShopCommentItem.DateCreated != null && Utility.CheckChamTraLoiComment(objShopCommentItem.DateCreated.Value, objShopCommentItem.NgayTraLoi))
                {
                    dataRow["ThoiGianChoDuyet"] = "Không đạt";
                }
                else
                {
                    dataRow["ThoiGianChoDuyet"] = "Đạt";
                }

                // dataRow["ThoiGianChoDuyet"] = objShopCommentItem.DateIsActive;
                dataRow["AdminPhuTrach"] = objShopCommentItem.NguoiDuyet;
                dataTable.Rows.Add(dataRow);

            }
            return dataTable;
        }

    }

    
}
