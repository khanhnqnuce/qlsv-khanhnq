using System;
using System.Data;
using System.Windows.Forms;
using PerpetuumSoft.Reporting.View;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Frm
{
    public partial class FrmMsgImportSv : Form
    {
        private readonly DataTable _tb;
        public FrmMsgImportSv(string text,DataTable tb)
        {
            InitializeComponent();
            lbMsg.Text = text;
            _tb = tb;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            RptKhoa();
        }
        private void RptKhoa()
        {

            try
            {
                reportManager1.DataSources.Clear();
                reportManager1.DataSources.Add("danhsach",_tb);
                rptdanhsachsinhvien.FilePath = Application.StartupPath + @"\Reports\danhsachsinhvien.rst";
                rptdanhsachsinhvien.Prepare();
                var previewForm = new PreviewForm(rptdanhsachsinhvien)
                {
                    WindowState = FormWindowState.Maximized
                };
                previewForm.Show();
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }
    }
}
