using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Service;
using QLSV.Core.Utils.Core;
using QLSV.Data.Utils.Data;

namespace QLSV.Frm.Frm
{
    public partial class FrmNapDuLieu : Form
    {
        public DataTable ResultValue = new DataTable();
        private readonly bool _multiSheet;
        private Thread _threadLoad;
        private int _stt;
        //private readonly IList<SinhVien> _listSinhVien;
        public FrmNapDuLieu(int stt)
        {
            try
            {
                InitializeComponent();
                _multiSheet = false;
                _stt = stt;
                //_listSinhVien = QlsvSevice.Load<SinhVien>();
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

        private static DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("STT", typeof(string));
            table.Columns.Add("MaSinhVien", typeof(string));
            table.Columns.Add("HoSinhVien", typeof(string));
            table.Columns.Add("TenSinhVien", typeof(string));
            table.Columns.Add("NgaySinh", typeof(string));
            table.Columns.Add("MaLop", typeof(string));
            table.Columns.Add("TenKhoa", typeof(string));
            return table;
        }

        private void LoadData(object obj)
        {
            try
            {

                if (Path.GetExtension(txtTenFile.Text) == ".xlsx")
                {
                    if (!_multiSheet)
                        Read_2007or2010();
                    Invoke((MethodInvoker) Close);
                }
                else
                {
                    if (!_multiSheet)
                        Read_2003();
                    Invoke((MethodInvoker) Close);
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
                var sheet = excel.GetSheetAt(0);
                var startRows = sheet.FirstRowNum;
                var endRows = sheet.LastRowNum;
                var result = GetTable();
                var tb = Core.LINQ.LoadData.Load(2);
                var maximum = (endRows - startRows + 1) > 100 ? (endRows - startRows + 1) : 200;
                upsbLoading.SetPropertyThreadSafe(p => p.Maximum, maximum);
                var donvi = (endRows - startRows + 1) == 0 ? maximum : maximum / (endRows - startRows + 1);
                for (var i = startRows; i <= endRows; i++)
                {
                    foreach (var row in tb.Rows.Cast<DataRow>().Where(row => row[0].ToString()==sheet.GetRow(i).GetCell(0).ToString()))
                    {
                        goto a;
                    }
                    result.Rows.Add(++_stt,
                        sheet.GetRow(i).GetCell(0).ToString(),
                        sheet.GetRow(i).GetCell(1).ToString(),
                        sheet.GetRow(i).GetCell(2).ToString(),
                        sheet.GetRow(i).GetCell(3).ToString(),
                        sheet.GetRow(i).GetCell(4).ToString(),
                        sheet.GetRow(i).GetCell(5).ToString());
                    a:;
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
                var oSheet = excelPkg.Workbook.Worksheets[1];
                var startRows = oSheet.Dimension.Start.Row;
                var endRows = oSheet.Dimension.End.Row;
                var result = GetTable();
                for (var i = startRows; i <= endRows; i++)
                {
                    result.Rows.Add(++_stt,
                        oSheet.Cells[i, 1].GetValue<string>(),
                        oSheet.Cells[i, 2].GetValue<string>(),
                        oSheet.Cells[i, 3].GetValue<string>(),
                        oSheet.Cells[i, 4].GetValue<string>(),
                        oSheet.Cells[i, 5].GetValue<string>(),
                        oSheet.Cells[i, 6].GetValue<string>());
                }
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
