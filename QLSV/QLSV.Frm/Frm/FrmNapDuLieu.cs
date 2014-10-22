using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using QLSV.Core.Utils.Core;
using QLSV.Data.Utils.Data;

namespace QLSV.Frm.Frm
{
    public partial class FrmNapDuLieu : Form
    {
        public DataTable ResultValue = new DataTable();
        private readonly bool _multiSheet;
        private Thread _threadLoad;
        readonly int _socot;
        public FrmNapDuLieu(int count)
        {
            try
            {
                InitializeComponent();
                _multiSheet = false;
                _socot = count;
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
                        Read_2007or2010(_socot);
                    Invoke((MethodInvoker)Close);
                }
                else
                {
                    if (!_multiSheet)
                        Read_2003(_socot);
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

        private void Read_2003(int count)
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
                #endregion

                var maximum = (endRows - startRows + 1) > 100 ? (endRows - startRows + 1) : 100;
                upsbLoading.SetPropertyThreadSafe(p => p.Maximum, maximum);
                var donvi = (endRows - startRows + 1) == 0 ? maximum : maximum / (endRows - startRows + 1);
                var listHeader = new List<string>();

                for (var i = startRows; i <= endRows; i++)
                {
                    var row = sheet.GetRow(i);
                    if (i == 0)
                    {
                        startCol = row.FirstCellNum;
                        endCol = row.LastCellNum;
                    }
                    var destRow = result.NewRow();
                    var isHeader = true;
                    for (var j = startCol; j < endCol; j++)
                    {
                        var cell = row.GetCell(j);
                        if (i == 0)
                        {
                            if (string.IsNullOrEmpty(cell.ToString())) continue;
                            listHeader.Add(cell.ToString());
                            result.Columns.Add(cell.ToString(), typeof(string));
                        }
                        else
                        {
                            isHeader = false;
                            if (!string.IsNullOrEmpty(cell.ToString()))
                            {
                                destRow[listHeader[j]] = cell.ToString();
                            }
                            else destRow[listHeader[j]] = "";
                        }
                    }
                    if (!isHeader)
                    {
                        result.Rows.Add(destRow);
                        if (endCol - startCol < count)
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

        private void Read_2007or2010(int count)
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
                var endRows = oSheet.Dimension.End.Row;
                var startCols = oSheet.Dimension.Start.Column;
                var endCols = oSheet.Dimension.End.Column;
                var result = new DataTable();

                var maximum = (endRows - startRows + 1) > 100 ? (endRows - startRows + 1) : 100;
                upsbLoading.SetPropertyThreadSafe(p => p.Maximum, maximum);
                var donvi = (endRows - startRows + 1) == 0 ? maximum : maximum / (endRows - startRows + 1);
                var listHeader = new List<string>();
                
                for (var i = startRows; i <= endRows; i++)
                {
                    var destRow = result.NewRow();
                    var isHeader = true;
                    for (var j = startCols; j <= endCols; j++)
                    {
                        if (i == 1)
                        {

                            if (string.IsNullOrEmpty(oSheet.Cells[i, j].GetValue<string>())) continue;
                            listHeader.Add(oSheet.Cells[i, j].GetValue<string>());
                            result.Columns.Add(oSheet.Cells[i, j].GetValue<string>(), typeof(string));
                        }
                        else
                        {
                            isHeader = false;
                            destRow[listHeader[j-1]] = oSheet.Cells[i, j].GetValue<string>();
                        }
                    }
                    if (!isHeader)
                    {
                        result.Rows.Add(destRow);
                        if (endCols - startCols + 1 < count)
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
