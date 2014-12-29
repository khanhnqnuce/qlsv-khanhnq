using System;
using System.Windows.Forms;
using PerpetuumSoft.Reporting.View;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;
using QLSV.Frm.Base;

namespace QLSV.Frm.Frm
{
    public partial class FrmKiemTraLoiLogic : FunctionControlHasGrid
    {
        private readonly int _idkythi;

        public FrmKiemTraLoiLogic(int idkythi)
        {
            InitializeComponent();
            _idkythi = idkythi;
        }

        private void Rptdanhsach()
        {
            try
            {
                var tb = LoadData.Load(8,_idkythi);
                if (tb == null || tb.Rows.Count == 0)
                {
                    MessageBox.Show(@"Không có lỗi xảy ra", @"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                reportManager1.DataSources.Clear();
                reportManager1.DataSources.Add("danhsach",tb );
                rptkiemtralogic.FilePath = Application.StartupPath + @"\Reports\kiemtrasinhvien.rst";
                rptkiemtralogic.Prepare();
                var previewForm = new PreviewForm(rptkiemtralogic)
                {
                    WindowState = FormWindowState.Maximized,
                    ShowInTaskbar = false
                };
                previewForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }

        private void GetParameter(object sender,
           PerpetuumSoft.Reporting.Components.GetReportParameterEventArgs e)
        {
            try
            {
                e.Parameters["KiemTra"].Value = "Không có lỗi xảy ra";
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Indanhsach()
        {
            Rptdanhsach();
        }
    }
}
