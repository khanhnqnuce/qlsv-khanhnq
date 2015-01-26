using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using QLSV.Core.LINQ;

namespace QLSV.Frm.Frm
{
    public partial class FrmSaoLuuDuLieu : Form
    {
        readonly FolderBrowserDialog _fbdChoosePath = new FolderBrowserDialog();
        private readonly Connect _conn = new Connect();
        public FrmSaoLuuDuLieu()
        {
            InitializeComponent();
        }

        private void txtPathBackup_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
        {
            _fbdChoosePath.Description = @"Chọn thư mục lưu file backup";
            _fbdChoosePath.ShowNewFolderButton = true;
            if (_fbdChoosePath.ShowDialog() == DialogResult.OK)
            {
                txtPathBackup.Text = _fbdChoosePath.SelectedPath;
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPathBackup.Text))
            {
                txtPathBackup.Focus();
            }
            else if (string.IsNullOrEmpty(txtNameFile.Text))
            {
                txtNameFile.Focus();
            }
            else
            {
                var bBackUpStatus = true;
                Cursor.Current = Cursors.WaitCursor;
                var forder = txtPathBackup.Text + @"\QLSV_SqlBackup";
                var filename = forder + @"\" + txtNameFile.Text + @".bak";
                if (Directory.Exists(forder))
                {

                    if (File.Exists(filename))
                    {
                        if (MessageBox.Show(@"File đã tồn tại bạn có muốn thay thế nó?", @"Thống báo",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            File.Delete(filename);
                        }
                        else
                            bBackUpStatus = false;
                    }
                }
                else
                    Directory.CreateDirectory(forder);

                if (!bBackUpStatus) return;
                //Connect to DB
                
                var connect = _conn.GetConnect();
                connect.Open();
                //----------------------------------------------------------------------------------------------------

                //Execute SQL---------------
                var str = @"backup database " + _conn.Database + " to disk ='" + filename + "' with init, stats=10";
                var command = new SqlCommand(str, connect);
                command.ExecuteNonQuery();
                //-------------------------------------------------------------------------------------------------------------------------------

                connect.Close();

                MessageBox.Show(@"CSDL đã được sao lưu thành công", @"Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
