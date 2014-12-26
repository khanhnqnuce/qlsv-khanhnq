using System;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.LINQ;
using ColumnStyle = Infragistics.Win.UltraWinGrid.ColumnStyle;

namespace QLSV.Frm.Frm
{
    public partial class FrmThongKeKetQua : Form
    {
        public FrmThongKeKetQua()
        {
            InitializeComponent();
        }

        private void FrmChonKyThi_Load(object sender, EventArgs e)
        {
            dgv_DanhSach.DataSource = LoadData.Load(20);
        }

        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            var band = e.Layout.Bands[0];
            band.ColHeadersVisible = false;

            band.Columns["ID"].Hidden = true;
            band.Columns["Chon"].Style = ColumnStyle.CheckBox;
            band.Columns["Chon"].MaxWidth = 70;
            band.Columns["TenKT"].CellActivation = Activation.NoEdit;
        }
        
    }
}
