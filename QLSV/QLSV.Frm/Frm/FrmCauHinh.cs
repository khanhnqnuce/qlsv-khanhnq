using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Frm
{
    public partial class FrmCauHinh : Form
    {
        public FrmCauHinh()
        {
            InitializeComponent();
        }

        private void FrmCauHinh_Load(object sender, EventArgs e)
        {
            try
            {
                var logPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
                var filename = logPath + @"\Connection.xml";
                if(!File.Exists(filename)) return;
                var xmlread = new XmlDocument();
                xmlread.Load("Connection.xml");
                var xmlelement = xmlread.DocumentElement;
                if (xmlelement == null) return;
                var serverNode = xmlelement.SelectSingleNode("server");
                if (serverNode != null)
                    txtserver.Text = serverNode.InnerText;

                var databaseNode = xmlelement.SelectSingleNode("database");
                if (databaseNode != null)
                    txtdatabase.Text = databaseNode.InnerText;

                var usernameNode = xmlelement.SelectSingleNode("username");
                if (usernameNode != null)
                    txtusername.Text = usernameNode.InnerText;

                var passwordNode = xmlelement.SelectSingleNode("password");
                if (passwordNode != null)
                    txtpassword.Text = passwordNode.InnerText;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtserver.Text))
                {
                    txtserver.Focus();
                }else if (string.IsNullOrEmpty(txtdatabase.Text))
                {
                    txtdatabase.Focus();
                }
                else
                {
                    var xdoc = new XDocument(
                        new XElement("config",
                            new XElement("server", txtserver.Text),
                            new XElement("database", txtdatabase.Text),
                            new XElement("username", txtusername.Text),
                            new XElement("password", txtpassword.Text)
                            )
                        );
                    xdoc.Save("Connection.xml");
                    MessageBox.Show(@"Lưu lại thành công", @"Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //Log2File.LogExceptionToFile(ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                string conString;
                if (string.IsNullOrEmpty(txtusername.Text) || string.IsNullOrEmpty(txtpassword.Text))
                    conString = @"Data Source = " + txtserver.Text + ";Initial Catalog = " + txtdatabase.Text + ";Integrated Security=SSPI";
                else
                    conString = @"Data Source=" + txtserver.Text + ";Initial Catalog=" + txtdatabase.Text + ";User Id=" + txtusername.Text + ";Password=" + txtpassword.Text + "";
                var conn = new SqlConnection(conString);
                if (CheckConn(conn))
                    MessageBox.Show(@"Kết nối CSDL thành công.", @"Thông báo");
                else
                    MessageBox.Show(@"Không thể kết nối CSDL.", @"Thông báo");
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        private static bool CheckConn(SqlConnection conn)
        {
            try
            {
                conn.Open();
                return conn.State == ConnectionState.Open;
            }
            catch
            {
                return false;
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
