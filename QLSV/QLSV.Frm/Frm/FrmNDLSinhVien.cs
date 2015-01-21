using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using OfficeOpenXml;
using QLSV.Core.Utils.Core;
using QLSV.Data.Utils.Data;

namespace QLSV.Frm.Frm
{
    public partial class FrmNDLSinhVien : Form
    {
        public int gb_iViTriHeader = 0;
        public DataTable ResultValue = new DataTable();
        private readonly bool _multiSheet;
        private Thread _threadLoad;
        private int _iNumberStt;
        private readonly int _iNumberCol;
        private readonly DataTable _result;
        //private readonly IList<SinhVien> _listSinhVien;
        public FrmNDLSinhVien(int stt, DataTable tbTable, int iNumberCol)
        {
            try
            {
                InitializeComponent();
                _multiSheet = false;
                _iNumberStt = stt;
                _result = tbTable;
                _iNumberCol = iNumberCol;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
        //private static DataTable GetTable()
        //{
        //    var table = new DataTable();
        //    table.Columns.Add("STT", typeof(string));
        //    table.Columns.Add("MaSinhVien", typeof(string));
        //    table.Columns.Add("HoSinhVien", typeof(string));
        //    table.Columns.Add("TenSinhVien", typeof(string));
        //    table.Columns.Add("NgaySinh", typeof(string));
        //    table.Columns.Add("MaLop", typeof(string));
        //    table.Columns.Add("TenKhoa", typeof(string));
        //    return table;
        //}

        private void LoadData(object obj)
        {
            try
            {

                if (Path.GetExtension(txtTenFile.Text) == ".xlsx")
                {
                    if (!_multiSheet)
                        Read_2007or2010();
                    Invoke((MethodInvoker)Close);
                }
                else
                {
                    if (!_multiSheet)
                        Read_2003();
                    Invoke((MethodInvoker)Close);
                }
            }
            catch (Exception ex)
            {
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
                var startRows = sheet.FirstRowNum + gb_iViTriHeader;
                var endRows = sheet.LastRowNum;
                var maximum = (endRows - startRows + 1) > 100 ? (endRows - startRows + 1) : 200;
                upsbLoading.SetPropertyThreadSafe(p => p.Maximum, maximum);
                var donvi = (endRows - startRows + 1) == 0 ? maximum : maximum / (endRows - startRows + 1);
                for (var i = startRows; i <= endRows; i++)
                {
                    _result.Rows.Add();
                    _result.Rows[i - startRows][1] = ++_iNumberStt;
                    for (var j = 0; j < _iNumberCol; j++)
                    {
                        _result.Rows[i - startRows][j + 2] = sheet.GetRow(i).GetCell(j).ToString();
                    }
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
                var startRows = oSheet.Dimension.Start.Row + gb_iViTriHeader;
                var endRows = oSheet.Dimension.End.Row;
                var maximum = (endRows - startRows + 1) > 100 ? (endRows - startRows + 1) : 200;
                upsbLoading.SetPropertyThreadSafe(p => p.Maximum, maximum);
                var donvi = (endRows - startRows + 1) == 0 ? maximum : maximum / (endRows - startRows + 1);
                for (var i = startRows; i <= endRows; i++)
                {
                    _result.Rows.Add();
                    _result.Rows[i - startRows][1] = ++_iNumberStt;
                    for (var j = 1; j <= _iNumberCol; j++)
                    {
                        _result.Rows[i - startRows][j + 1] = oSheet.Cells[i, j].GetValue<string>();
                    }
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
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
