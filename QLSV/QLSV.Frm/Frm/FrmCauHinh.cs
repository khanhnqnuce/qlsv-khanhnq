using System;
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
                Log2File.LogExceptionToFile(ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
