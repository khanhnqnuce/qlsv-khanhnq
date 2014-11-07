using System;
using System.Windows.Forms;
using System.Drawing;

namespace QLSV.Frm.Frm
{
    public partial class FrmLoadding : Form
    {
        private bool _mouseDowned;
        private Point _positionMouse;
        public event CancelLoading Cancel;

        public FrmLoadding()
        {
            InitializeComponent();
            _mouseDowned = false;
        }

        private void MoveWindow_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDowned = false;
        }

        private void MoveWindow_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDowned = true;
            _positionMouse = e.Location;
        }

        private void MoveWindow_MouseMove(object sender, MouseEventArgs e)
        {
            MoveForm(e);
        }

        private void MoveForm(MouseEventArgs e)
        {
            if (!_mouseDowned) return;
            int xDiff = _positionMouse.X - e.Location.X;
            int yDiff = _positionMouse.Y - e.Location.Y;
            int x = Location.X - xDiff;
            int y = Location.Y - yDiff;
            Location = new Point(x, y);
        }

    }

    public delegate void CancelLoading(object sender, EventArgs e);
}

