using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using OfficeOpenXml;
using QLSV.Core.Utils.Core;
using QLSV.Data.Utils.Data;

namespace QLSV.Frm.Frm
{
    public partial class FrmNapDuLieu : Form
    {
        /// <summary>
        /// gb_iViTriHeader: lưu vị trí bắt đầu lấy dữ liệu trong file excel
        /// ResultValue, _result: Bảng lưu dữ liệu lấy trong file excel
        /// _threadLoad: Thread chạy hàm loaddata
        /// _iStartCol: Cột bắt đầu lấy dữ liệu của tbTable bắt đầu từ 0
        /// iEndCol: Cột kết thúc lấy dữ liệu của tbTable
        /// _iSheet: Vị trí sheet lấy dữ liệu của file excel
        /// </summary>

        public DataTable ResultValue = new DataTable();
        private readonly DataTable _result;
        private Thread _threadLoad;

        public int ViTriHeader;
        private readonly int _iEndCol;
        private readonly int _iSheet;

        public FrmNapDuLieu(DataTable tbTable, int iSheet)
        {
            try
            {
                InitializeComponent();
                _result = tbTable;
                _iSheet = iSheet;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void LoadData(object obj)
        {
            try
            {

                if (Path.GetExtension(txtTenFile.Text) == ".xlsx")
                {
                    Read_2007or2010();
                    Invoke((MethodInvoker)Close);
                }
                else
                {
                    Read_2003();
                    Invoke((MethodInvoker)Close);
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        /// <summary>
        /// Vị trí trong excel bắt đầu lấy từ 0
        /// </summary>
        private void Read_2003()
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
                    MessageBox.Show(FormResource.msgKiemTraFile, FormResource.MsgCaption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    ResultValue = null;
                    return;
                }
                var excel = new HSSFWorkbook(stream);
                stream.Close();
                var sheet = excel.GetSheetAt(_iSheet);
                var startRows = sheet.FirstRowNum + ViTriHeader;
                var endRows = sheet.LastRowNum;
                var maximum = (endRows - startRows + 1) > 100 ? (endRows - startRows + 1) : 200;
                upsbLoading.SetPropertyThreadSafe(p => p.Maximum, maximum);
                var donvi = (endRows - startRows + 1) == 0 ? maximum : maximum / (endRows - startRows + 1);
                for (var i = startRows; i <= endRows; i++)
                {
                    _result.Rows.Add(
                    sheet.GetRow(i).GetCell(0).ToString(),
                    sheet.GetRow(i).GetCell(1).ToString(),
                    sheet.GetRow(i).GetCell(2).ToString(),
                    sheet.GetRow(i).GetCell(3).ToString());
                    upsbLoading.SetPropertyThreadSafe(c => c.Value, (i - startRows + 1) * donvi);
                }
                upsbLoading.SetPropertyThreadSafe(c => c.Value, maximum);
                ResultValue = _result;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                ResultValue = null;
            }
        }

        /// <summary>
        /// vị trí trong excel bắt đầu lấu từ 1
        /// </summary>
        private void Read_2007or2010()
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
                    MessageBox.Show(FormResource.msgKiemTraFile, FormResource.MsgCaption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    ResultValue = null;
                    return;
                }
                var excelPkg = new ExcelPackage();
                excelPkg.Load(stream);
                stream.Close();
                var oSheet = excelPkg.Workbook.Worksheets[_iSheet + 1];
                var startRows = oSheet.Dimension.Start.Row + ViTriHeader;
                var endRows = oSheet.Dimension.End.Row;
                var maximum = (endRows - startRows + 1) > 100 ? (endRows - startRows + 1) : 200;
                upsbLoading.SetPropertyThreadSafe(p => p.Maximum, maximum);
                var donvi = (endRows - startRows + 1) == 0 ? maximum : maximum / (endRows - startRows + 1);
                for (var i = startRows; i <= endRows; i++)
                {
                    _result.Rows.Add(
                        oSheet.Cells[i, 1].GetValue<string>(),
                        oSheet.Cells[i, 2].GetValue<string>(),
                        oSheet.Cells[i, 3].GetValue<string>(),
                        oSheet.Cells[i, 4].GetValue<string>());
                    upsbLoading.SetPropertyThreadSafe(c => c.Value, (i - startRows + 1) * donvi);
                }
                upsbLoading.SetPropertyThreadSafe(c => c.Value, maximum);
                ResultValue = _result;
            }
            catch (Exception ex)
            {
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
