using System;
using System.Windows.Forms;
using QLSV.Core.LINQ;

namespace QLSV.Frm.Frm
{
    public partial class FrmSuaDiemThi : Form
    {
        private int _idKt;
        public bool update;
        public FrmSuaDiemThi(int idkt)
        {
            InitializeComponent();
            _idKt = idkt;
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
           if (string.IsNullOrEmpty(txtmade.Text))
            {
                errormade.SetError(txtmade, "Không được để trống");
            }
            else if (string.IsNullOrEmpty(txtchuoi.Text))
            {
                errormade.Dispose();
                errorchuoi.SetError(txtchuoi, "Không được để trống");
            }
            else
            {
                errordiem.Dispose();
                
                if (UpdateData.UpdateDT(int.Parse(txtmasv.Text), txtmade.Text, txtchuoi.Text,_idKt))
                {
                    update = true;
                    MessageBox.Show(@"Sửa thành công.", @"Thông báo");
                }
                Close();
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
