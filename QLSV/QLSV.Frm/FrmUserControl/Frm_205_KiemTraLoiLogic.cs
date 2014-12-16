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
        public FrmKiemTraLoiLogic()
        {
            InitializeComponent();
        }

        private void Rptdanhsach()
        {
            try
            {
                var tb = LoadData.Load(13);
                reportManager1.DataSources.Clear();
                reportManager1.DataSources.Add("danhsach",tb );
                rptkiemtralogic.FilePath = Application.StartupPath + @"\Reports\kiemtrasinhvien.rst";
                using (var previewForm = new PreviewForm(rptkiemtralogic))
                {
                    previewForm.WindowState = FormWindowState.Maximized;
                    if(tb==null || tb.Rows.Count == 0)
                    rptkiemtralogic.GetReportParameter += GetParameter;
                    rptkiemtralogic.Prepare();
                    previewForm.ShowDialog();
                }
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
