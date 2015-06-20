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
        private readonly int _number;
        public FrmMsgImportSv(string text,DataTable tb, int i)
        {
            InitializeComponent();
            lbMsg.Text = text;
            _tb = tb;
            _number = i;
        }
        public FrmMsgImportSv()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            switch (_number)
            {
                case 1:
                    RptKhoa();
                    break;
                case 2:
                    RptChamthi();
                    break;
                case 3:
                    RptView("danhsachsinhvien");
                    break;
            }
        }

        public void RptView(string rptname,string source = "danhsach")
        {
            try
            {
                reportManager.DataSources.Clear();
                reportManager.DataSources.Add(source,_tb);
                ReportView.FilePath = Application.StartupPath + @"\Reports\" + rptname + ".rst";
                ReportView.Prepare();
                var previewForm = new PreviewForm(ReportView)
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

        private void RptKhoa()
        {

            try
            {
                reportManager.DataSources.Clear();
                reportManager.DataSources.Add("danhsach",_tb);
                rptdanhsachsinhvien.FilePath = Application.StartupPath + @"\Reports\loichamthi.rst";
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
        
        private void RptChamthi()
        {

            try
            {
                reportManager.DataSources.Clear();
                reportManager.DataSources.Add("danhsach",_tb);
                rptdanhsachsinhvien.FilePath = Application.StartupPath + @"\Reports\loichamthi.rst";
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Escape):
                    Close();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
