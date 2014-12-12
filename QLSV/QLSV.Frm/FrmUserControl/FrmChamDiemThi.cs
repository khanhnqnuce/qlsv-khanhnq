using System.Windows.Forms;
using QLSV.Core.Domain;
using QLSV.Core.Service;

namespace QLSV.Frm.FrmUserControl
{
    public partial class FrmChamDiemThi : UserControl
    {
        public FrmChamDiemThi()
        {
            InitializeComponent();
        }

        private void ChamThi()
        {
            var list = QlsvSevice.Load<DapAn>();
        }
    }
}
