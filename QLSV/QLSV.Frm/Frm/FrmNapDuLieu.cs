using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using QLSV.Core.Utils;
using QLSV.Data.Utils;

namespace QLSV.Frm.Frm
{
    public partial class FrmNapDuLieu : Form
    {
        public bool RadioXoavathem = false;
        public int ViTriHeader = 2;
        public bool Checkclose = false;
        public static List<InputParamExcel> ListInput = new List<InputParamExcel>();
        public static DataTable ResultValue = new DataTable();
        private readonly bool _multiSheet;
        public int FormYeuCau { get; set; }
        public DataSet OutPut = new DataSet();
        private Thread _threadLoad;

        public FrmNapDuLieu(List<InputParamExcel> lstInput1)
        {
            try
            {
                InitializeComponent();
                ListInput = lstInput1;
                ViTriHeader = 2;
                _multiSheet = false;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                else
                    MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }
        private void LoadData(object obj)
        {
            try
            {

                if (Path.GetExtension(txtTenFile.Text) == ".xlsx")
                {
                    if (!_multiSheet)
                        Read_2007or2010(ViTriHeader, ListInput);
                    Invoke((MethodInvoker)Close);
                }
                else
                {
                    if (!_multiSheet)
                        Read_2003(ViTriHeader, ListInput);
                    Invoke((MethodInvoker)Close);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                else
                    MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Read_2003(int positionHeader, List<InputParamExcel> inputCot)
        {
            try
            {
                FileStream stream;
                try
                {
                    stream = new FileStream(txtTenFile.Text, FileMode.Open);
                }
                catch (Exception)
                {
                    MessageBox.Show(FormResource.msgKiemTraFile, FormResource.MsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResultValue = null;
                    return;
                }
                var excel = new HSSFWorkbook(stream);
                stream.Close();
                var sheet = excel.GetSheetAt(0);
                var startRows = sheet.FirstRowNum;
                var endRows = sheet.LastRowNum;
                int startCol = 0, endCol = 0;

                #region Khởi tạo datatable
                var result = new DataTable();
                foreach (var item in inputCot)
                {
                    switch (item.ThuocTinh)
                    {
                        case InputTypeExcel.Double:
                            result.Columns.Add(item.Ten, typeof(double));
                            break;
                        case InputTypeExcel.Integer:
                            result.Columns.Add(item.Ten, typeof(int));
                            break;
                        case InputTypeExcel.String:
                            result.Columns.Add(item.Ten, typeof(string));
                            break;
                        case InputTypeExcel.DateTime:
                            result.Columns.Add(item.Ten, typeof(DateTime));
                            break;
                        case InputTypeExcel.Boolean:
                            result.Columns.Add(item.Ten, typeof(bool));
                            break;
                    }
                }
                #endregion

                var maximum = (endRows - startRows + 1) > 100 ? (endRows - startRows + 1) : 100;
                upsbLoading.SetPropertyThreadSafe(p => p.Maximum, maximum);
                var donvi = (endRows - startRows + 1) == 0 ? maximum : maximum / (endRows - startRows + 1);
                var listHeader = new List<string>();
                var listCheckColumn = new List<bool>();
                for (var i = 0; i < inputCot.Count; i++)
                {
                    listCheckColumn.Add(false);
                }
                for (var i = startRows; i <= endRows; i++)
                {
                    var indexColumnInput = 0;
                    var row = sheet.GetRow(i);
                    if (i == startRows + positionHeader - 1)
                    {
                        startCol = row.FirstCellNum;
                        endCol = row.LastCellNum;
                    }
                    var destRow = result.NewRow();
                    var isHeader = true;
                    for (var j = startCol; j < endCol; j++)
                    {
                        var cell = row.GetCell(j);
                        if (i < startRows + positionHeader - 1) continue;
                        if (i == startRows + positionHeader - 1)
                        {
                            listHeader.Add(cell.ToString());
                        }
                        else
                        {
                            isHeader = false;
                            if (listHeader[j] == inputCot[indexColumnInput].Ten)
                            {
                                listCheckColumn[indexColumnInput] = true;
                                try
                                {
                                    switch (inputCot[indexColumnInput].ThuocTinh)
                                    {
                                        #region Get data
                                        case InputTypeExcel.Double:
                                            destRow[inputCot[indexColumnInput].Ten] = cell.CellType == CellType.Numeric
                                                ? cell.NumericCellValue
                                                : double.Parse(cell.ToString());
                                            break;
                                        case InputTypeExcel.Integer:
                                            destRow[inputCot[indexColumnInput].Ten] = cell.CellType == CellType.Numeric
                                                ? (int)cell.NumericCellValue
                                                : int.Parse(cell.ToString());
                                            break;
                                        case InputTypeExcel.String:
                                            destRow[inputCot[indexColumnInput].Ten] = cell == null ? "" : cell.ToString();
                                            break;
                                        case InputTypeExcel.DateTime:
                                            if (cell.CellType == CellType.Numeric)
                                            {
                                                destRow[inputCot[indexColumnInput].Ten] = cell.DateCellValue;
                                            }
                                            else
                                            {
                                                var date = cell.ToString().Split('/');
                                                destRow[inputCot[indexColumnInput].Ten] = new DateTime(int.Parse(date[2]),
                                                    int.Parse(date[1]), int.Parse(date[0]));
                                            }
                                            break;
                                        case InputTypeExcel.Boolean:
                                            var strBoolean = cell.ToString();
                                            if (cell.CellType == CellType.Boolean)
                                                destRow[inputCot[indexColumnInput].Ten] = cell.BooleanCellValue;
                                            else if (!string.IsNullOrEmpty(strBoolean)
                                                && strBoolean.ToLower() == "x")
                                                destRow[inputCot[indexColumnInput].Ten] = true;
                                            else
                                                destRow[inputCot[indexColumnInput].Ten] = false;
                                            break;
                                        #endregion
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ResultValue = null;
                                    throw new Exception(FormResource.msgSaiDuLieuTrongFile, ex);
                                }
                                indexColumnInput += 1;
                            }
                        }
                    }
                    if (!isHeader)
                    {
                        result.Rows.Add(destRow);
                        if (endCol - startCol < inputCot.Count || listCheckColumn.Contains(false))
                        {
                            MessageBox.Show(FormResource.msgThieuCotTrongFile, FormResource.MsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResultValue = null;
                            return;
                        }
                    }
                    upsbLoading.SetPropertyThreadSafe(c => c.Value, (i - startRows + 1) * donvi);
                }
                upsbLoading.SetPropertyThreadSafe(c => c.Value, maximum);
                ResultValue = result;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                else
                    MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
                ResultValue = null;
            }
        }

        private void Read_2007or2010(int positionHeader, List<InputParamExcel> inputCot)
        {
            try
            {
                FileStream stream;
                try
                {
                    stream = new FileStream(txtTenFile.Text, FileMode.Open);
                }
                catch (Exception)
                {
                    MessageBox.Show(FormResource.msgKiemTraFile, FormResource.MsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResultValue = null;
                    return;
                }
                var excelPkg = new ExcelPackage();
                excelPkg.Load(stream);
                stream.Close();
                var oSheet = excelPkg.Workbook.Worksheets[1];
                var startRows = oSheet.Dimension.Start.Row;
                var startCols = oSheet.Dimension.Start.Column;
                var endRows = oSheet.Dimension.End.Row;
                var endCols = oSheet.Dimension.End.Column;

                #region Khởi tạo datatable
                var result = new DataTable();
                foreach (var item in inputCot)
                {
                    switch (item.ThuocTinh)
                    {
                        case InputTypeExcel.Double:
                            result.Columns.Add(item.Ten, typeof(double));
                            break;
                        case InputTypeExcel.Integer:
                            result.Columns.Add(item.Ten, typeof(int));
                            break;
                        case InputTypeExcel.String:
                            result.Columns.Add(item.Ten, typeof(string));
                            break;
                        case InputTypeExcel.DateTime:
                            result.Columns.Add(item.Ten, typeof(DateTime));
                            break;
                        case InputTypeExcel.Boolean:
                            result.Columns.Add(item.Ten, typeof(bool));
                            break;
                    }
                }
                #endregion

                var maximum = (endRows - startRows + 1) > 100 ? (endRows - startRows + 1) : 100;
                upsbLoading.SetPropertyThreadSafe(p => p.Maximum, maximum);
                var donvi = (endRows - startRows + 1) == 0 ? maximum : maximum / (endRows - startRows + 1);
                var listHeader = new List<string>();
                var listCheckColumn = new List<bool>();
                for (var i = 0; i < inputCot.Count; i++)
                {
                    listCheckColumn.Add(false);
                }
                for (var i = startRows; i <= endRows; i++)
                {
                    var indexColumnInput = 0;
                    var destRow = result.NewRow();
                    var isHeader = true;
                    for (var j = startCols; j <= endCols; j++)
                    {
                        if (i < startRows + positionHeader - 1) continue;
                        if (i == startRows + positionHeader - 1)
                        {
                            listHeader.Add(oSheet.Cells[i, j].GetValue<string>());
                        }
                        else
                        {
                            isHeader = false;
                            if (listHeader[j - startCols] == inputCot[indexColumnInput].Ten)
                            {
                                listCheckColumn[indexColumnInput] = true;
                                try
                                {
                                    switch (inputCot[indexColumnInput].ThuocTinh)
                                    {
                                        #region Get data
                                        case InputTypeExcel.Double:
                                            destRow[inputCot[indexColumnInput].Ten] = oSheet.Cells[i, j].GetValue<double>();
                                            break;
                                        case InputTypeExcel.Integer:
                                            //destRow[inputCot[indexColumnInput].Ten] = oSheet.Cells[i, j].GetValue<int>();
                                            destRow[inputCot[indexColumnInput].Ten] = int.Parse(oSheet.Cells[i, j].Value.ToString());
                                            break;
                                        case InputTypeExcel.String:
                                            destRow[inputCot[indexColumnInput].Ten] = oSheet.Cells[i, j].GetValue<string>();
                                            break;
                                        case InputTypeExcel.DateTime:
                                            var typeCell = oSheet.Cells[i, j].Value.GetType().FullName;
                                            if (typeCell.Equals("System.Double") || typeCell.Equals("System.DateTime"))
                                            {
                                                destRow[inputCot[indexColumnInput].Ten] = oSheet.Cells[i, j].GetValue<DateTime>();
                                            }
                                            else
                                            {
                                                var date = oSheet.Cells[i, j].GetValue<string>().Split('/');
                                                destRow[inputCot[indexColumnInput].Ten] = new DateTime(int.Parse(date[2]),
                                                    int.Parse(date[1]), int.Parse(date[0]));
                                            }
                                            break;
                                        case InputTypeExcel.Boolean:
                                            var strBoolean = oSheet.Cells[i, j].GetValue<string>();
                                            if (!string.IsNullOrEmpty(strBoolean)
                                                && strBoolean.ToLower() == "x")
                                                destRow[inputCot[indexColumnInput].Ten] = true;
                                            else
                                                destRow[inputCot[indexColumnInput].Ten] = oSheet.Cells[i, j].GetValue<bool>();
                                            break;
                                        #endregion
                                    }
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show(FormResource.msgSaiDuLieuTrongFile, FormResource.MsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ResultValue = null;
                                    return;
                                }
                                indexColumnInput += 1;
                            }
                        }
                    }
                    if (!isHeader)
                    {
                        result.Rows.Add(destRow);
                        if (endCols - startCols + 1 < inputCot.Count || listCheckColumn.Contains(false))
                        {
                            MessageBox.Show(FormResource.msgThieuCotTrongFile, FormResource.MsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResultValue = null;
                            return;
                        }
                    }
                    upsbLoading.SetPropertyThreadSafe(c => c.Value, (i - startRows + 1) * donvi);
                }
                upsbLoading.SetPropertyThreadSafe(c => c.Value, maximum);
                ResultValue = result;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                else
                    MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
                ResultValue = null;
            }
        }

        private void btnTaiDuLieu_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtTenFile.Text == ""))
                {
                    MessageBox.Show(FormResource.msgChonFileExcel);
                }
                else
                {
                    upsbLoading.Minimum = 0;
                    upsbLoading.Value = 0;
                    if (_threadLoad == null)
                    {
                        _threadLoad = new Thread(LoadData)
                        {
                            IsBackground = true
                        };
                    }
                    if (_threadLoad.ThreadState == (ThreadState.Background | ThreadState.Unstarted)
                        || _threadLoad.ThreadState == ThreadState.Unstarted)
                    {
                        _threadLoad.Start();
                    }
                    else if (_threadLoad.ThreadState == ThreadState.Aborted
                        || _threadLoad.ThreadState == ThreadState.Stopped)
                    {
                        _threadLoad = new Thread(LoadData)
                        {
                            IsBackground = true
                        };
                        _threadLoad.Start();
                    }
                    else
                    {
                        MessageBox.Show(FormResource.txtRunning);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                else
                    MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
                ResultValue = null;
            }
        }

        private void btnChonfile_Click(object sender, EventArgs e)
        {
            try
            {
                var openfiledialog = new OpenFileDialog
                {
                    Filter = FormResource.txtDuoiFileExcel,
                    Multiselect = false,
                    Title = FormResource.txtMoFileExcel,
                    CheckFileExists = true,
                    CheckPathExists = true
                };
                if (openfiledialog.ShowDialog() == DialogResult.OK)
                {
                    var length = new FileInfo(openfiledialog.FileName).Length;
                    if (length <= Convert.ToInt64(FormResource.txtFileSize))
                    {
                        txtTenFile.Text = openfiledialog.FileName;
                    }
                    else
                    {
                        MessageBox.Show(FormResource.msgFileQuaLon);
                        ResultValue = null;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                else
                    MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
                ResultValue = null;
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            try
            {
                Checkclose = true;
                ResultValue = null;
                Close();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                else
                    MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
