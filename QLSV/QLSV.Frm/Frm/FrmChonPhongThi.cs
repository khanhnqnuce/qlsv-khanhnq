using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using ColumnStyle = Infragistics.Win.UltraWinGrid.ColumnStyle;

namespace QLSV.Frm.Frm
{
    public partial class FrmChonPhongThi : Form
    {
        private IList<KTPhong> _listPhanPhongs = new List<KTPhong>();
        private IList<XepPhong> _listXepPhong = new List<XepPhong>();
        private int _tongsucchua;
        private readonly int _idKythi;
        private readonly int _tongsv;
        public readonly DataTable TbTable = new DataTable();

        public FrmChonPhongThi(int idkythi, int tongsv,DataTable tb)
        {
            InitializeComponent();
            _idKythi = idkythi;
            _tongsv = tongsv;
            TbTable = tb;
        }

        private static DataTable GetTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Chon", typeof(bool));
            table.Columns.Add("TenPhong", typeof(string));
            table.Columns.Add("SucChua", typeof(int));
            return table;
        }

        private void dgv_DanhSach_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            var band = e.Layout.Bands[0];
            band.ColHeadersVisible = false;
            band.Columns["ID"].Hidden = true;
            band.Columns["Chon"].Style = ColumnStyle.CheckBox;
            band.Columns["Chon"].MaxWidth = 70;


            band.Override.CellAppearance.TextHAlign = HAlign.Center;
            band.Columns["TenPhong"].CellActivation = Activation.NoEdit;
            band.Columns["SucChua"].CellActivation = Activation.NoEdit;
        }

        private void FrmChonPhongThi_Load(object sender, System.EventArgs e)
        {
            dgv_DanhSach.DataSource = SearchData.LoadPhong(_idKythi);
        }

        private void dgv_DanhSach_CellChange(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Key == "Chon")
            {
                var b = bool.Parse(e.Cell.Row.Cells["Chon"].Text);
                if (b)
                {_tongsucchua = _tongsucchua + int.Parse(e.Cell.Row.Cells["SucChua"].Text);
                }
                else
                {
                    _tongsucchua = _tongsucchua - int.Parse(e.Cell.Row.Cells["SucChua"].Text);
                }

                lbtong.Text = @"Tổng sức chứa: " + _tongsucchua + @" sinh viên.";
            }
        }

        private void ckbChon_CheckedChanged(object sender, System.EventArgs e)
        {
            _tongsucchua = 0;
            if (ckbChon.Checked)
            {
                foreach (var row in dgv_DanhSach.Rows)
                {
                    row.Cells["Chon"].Value = "true";
                    _tongsucchua = _tongsucchua + int.Parse(row.Cells["SucChua"].Text);
                    
                }
                lbtong.Text = @"Tổng sức chứa: " + _tongsucchua + @" sinh viên.";
            }
            else
            {
                foreach (var row in dgv_DanhSach.Rows)
                {
                    row.Cells["Chon"].Value = "false";
                }
                lbtong.Text = @"Tổng sức chứa: " + _tongsucchua + @" sinh viên.";
            }
        }

        private void Xepphong()
        {
            if (_tongsucchua < _tongsv)
            {
                MessageBox.Show(@"Không đủ phòng để sắp xếp sinh viên");
                return;
            }
            var kt = 0;
            foreach (var row in dgv_DanhSach.Rows)
            {
                if (!bool.Parse(row.Cells["Chon"].Text)) return;
                var sc = int.Parse(row.Cells["SucChua"].Text);
                var idphong = int.Parse(row.Cells["ID"].Text);
                var phong = row.Cells["TenPhong"].Text;
                var bd = kt;
                kt = kt + sc;
                if (kt < _tongsv)
                {
                    for (var i = bd; i < kt; i++)
                    {
                        var hsxp = new XepPhong
                        {
                            IdKyThi = _idKythi,
                            IdPhong = idphong,
                            IdSV = int.Parse(TbTable.Rows[i]["MaSV"].ToString())
                        };
                        _listXepPhong.Add(hsxp);
                        TbTable.Rows[i]["PhongThi"] = row.Cells["TenPhong"].Text;
                    }
                    var hspp = new KTPhong
                    {
                        IdKyThi = _idKythi,
                        IdPhong = idphong,
                        SiSo = sc
                    };
                    _listPhanPhongs.Add(hspp);
                    continue;
                }
                else
                {
                    for (var i = bd; i < _tongsv; i++)
                    {
                        var hsxp = new XepPhong
                        {
                            IdKyThi = _idKythi,
                            IdPhong = idphong,
                            IdSV = int.Parse(TbTable.Rows[i]["MaSV"].ToString())
                        };
                        _listXepPhong.Add(hsxp);
                        TbTable.Rows[i]["PhongThi"] = row.Cells["TenPhong"].Text;
                    }
                    var hspp = new KTPhong()
                    {
                        IdKyThi = _idKythi,
                        IdPhong = idphong,
                        SiSo = _tongsv - bd
                    };
                    _listPhanPhongs.Add(hspp);
                    break;
                }
            }
            InsertData.XepPhong(_listXepPhong);
            InsertData.KtPhong(_listPhanPhongs);
            MessageBox.Show(@"Sinh viên đã được xếp phòng");
            Close();
        }

        private void btnLuu_Click(object sender, System.EventArgs e)
        {
            Xepphong();
        }
    }
}
