using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.LINQ;
using ColumnStyle = Infragistics.Win.UltraWinGrid.ColumnStyle;

namespace QLSV.Frm.Frm
{
    public partial class FrmGopKetQua : Form
    {
        public bool Check;
        public readonly IList<int> LstIdKyThi = new List<int>();
        public FrmGopKetQua()
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

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var row in dgv_DanhSach.Rows)
            {
                if (bool.Parse(row.Cells["Chon"].Text))
                    LstIdKyThi.Add(int.Parse(row.Cells["ID"].Text));
            }
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
