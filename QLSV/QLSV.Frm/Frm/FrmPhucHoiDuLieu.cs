using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using QLSV.Core.LINQ;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Frm
{
    public partial class FrmPhucHoiDuLieu : Form
    {
        private string _str = "";
        private readonly Connect _conn = new Connect();

        public FrmPhucHoiDuLieu()
        {
            InitializeComponent();
        }

        private void txtPathBackup_EditorButtonClick(object sender,
            Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
        {
            try
            {
                var openfiledialog = new OpenFileDialog
                {
                    Filter = @"Excel Files|*.bak;*bak",
                    Multiselect = false,
                    Title = @"Chọn file bak",
                    CheckFileExists = true,
                    CheckPathExists = true
                };
                if (openfiledialog.ShowDialog() == DialogResult.OK)
                {
                    _str = openfiledialog.FileName;
                    txtPathBackup.Text = _str;
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(_str))
                {
                    if (
                        MessageBox.Show(@"Bạn có chắc chắn muốn khôi phục lại?", @"Thông báo", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) != DialogResult.Yes) return;
                    //Connect SQL----------------------

                    var connect = _conn.GetConnect();
                    connect.Open();
                    
                    //Excute SQL-----------------------

                    var command = new SqlCommand("use master", connect);
                    command.ExecuteNonQuery();
                    var sql = "Alter Database " + _conn.Database + " set SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                    sql += @"restore database " + _conn.Database + " from disk = '" + _str + "' WITH REPLACE;";
                    command = new SqlCommand(sql, connect);
                    command.ExecuteNonQuery();
                    //---------------------------------
                    connect.Close();

                    MessageBox.Show(@"CSDL đã được khôi phục", @"Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show(
                        @"Chọn sai file hoặc đường dẫn chính xác)",
                        @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception exp)
            {
                Log2File.LogExceptionToFile(exp);
            }

        }
    }
}
