using System;
using System.Collections.Generic;
using System.Data;
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
        private IList<int> _list;

        public FrmKiemTraLoiLogic(int idkythi)
        {
            InitializeComponent();
            _idkythi = idkythi;
        }

        protected override void LoadFormDetail()
        {
            try
            {
                var frm1 = new FrmRptkiemtraloilogic {Check = false};
                frm1.ShowDialog();
                if (frm1.rdoone.Checked && frm1.Check)
                {
                    Rptdanhsach();
                }
                else if (frm1.rdoall.Checked && frm1.Check)
                {
                    var frm = new FrmGopKetQua { Check = false };
                    frm.ShowDialog();
                    _list = frm.LstIdKyThi;
                    if (_list.Count > 1 && frm.Check)
                    {
                        RptKtralogic();
                    }
                }
                
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void RptKtralogic()
        {
            try
            {
                var tb = Statistic.GopKetQua2(_list);
                if (tb.Length == 0)
                {
                    MessageBox.Show(@"Không có lỗi xảy ra", @"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                reportManager1.DataSources.Clear();
                reportManager1.DataSources.Add("danhsach",tb[1]);
                reportManager1.DataSources.Add("danhsach1",tb[0]);
                rptkiemtralogic.FilePath = Application.StartupPath + @"\Reports\kiemtrasinhvien1.rst";
                rptkiemtralogic.Prepare();
                rptkiemtralogic.GetReportParameter += GetParameter1;
                var previewForm = new PreviewForm(rptkiemtralogic)
                {
                    WindowState = FormWindowState.Maximized,
                };
                previewForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);

            }
        }

        private void GetParameter1(object sender,
           PerpetuumSoft.Reporting.Components.GetReportParameterEventArgs e)
        {
            try
            {
                var tb = LoadData.Load(3, _list[0]);
                var tb1 = LoadData.Load(3, _list[1]);
                foreach (DataRow row in tb.Rows)
                {
                    e.Parameters["TT2"].Value = row["GhiChu"].ToString();
                }
                foreach (DataRow row in tb1.Rows)
                {
                    e.Parameters["TT1"].Value = row["GhiChu"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void Rptdanhsach()
        {
            try
            {
                var tb1 = LoadData.Load(2051,_idkythi);
                var tb2 = LoadData.Load(2052,_idkythi);
                if (tb1.Rows.Count == 0 && tb2.Rows.Count == 0)
                {
                    MessageBox.Show(@"Không có lỗi xảy ra", @"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                reportManager1.DataSources.Clear();
                reportManager1.DataSources.Add("danhsach",tb1);
                reportManager1.DataSources.Add("danhsach1",tb2);
                rptkiemtralogic.FilePath = Application.StartupPath + @"\Reports\kiemtrasinhvien.rst";
                rptkiemtralogic.Prepare();
                rptkiemtralogic.GetReportParameter += GetParameter;
                var previewForm = new PreviewForm(rptkiemtralogic)
                {
                    WindowState = FormWindowState.Maximized,
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
                var tb = LoadData.Load(3, _idkythi);
                foreach (DataRow row in tb.Rows)
                {
                    e.Parameters["TenKT"].Value = row["TenKT"].ToString();
                }
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
