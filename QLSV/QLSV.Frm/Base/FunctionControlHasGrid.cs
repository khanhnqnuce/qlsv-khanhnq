using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Base
{
    public partial class FunctionControlHasGrid : UserControl
    {
        protected IList<int> IdDelete = new List<int>();
        protected readonly object LockTotal = new object();
        protected bool B = false;

        #region event delegate

        public delegate void CustomHandler(object sender, string msg);
        public event CustomHandler ShowDialog = null;
        protected virtual void OnShowDialog(string msg)
        {
            var handler = ShowDialog;
            if (handler != null) handler(this, msg);
        }

        public delegate void CustomHandler1(object sender);
        public event CustomHandler1 CloseDialog = null;
        protected virtual void OnCloseDialog()
        {
            var handler = CloseDialog;
            if (handler != null) handler(this);
        }

        public delegate void CustomHandler2(object sender, string msg);
        public event CustomHandler2 UpdateDialog = null;
        protected virtual void OnUpdateDialog(string msg)
        {
            var handler = UpdateDialog;
            if (handler != null) handler(this, msg);
        }

        #endregion

        public void uG_InsertRow()
        {
            InsertRow();
        }

        protected virtual void InsertRow() { }

        protected void InsertRow(UltraGrid grid,string column1 = null, string column2 = null)
        {
            try
            {
                var row = grid.DisplayLayout.Bands[0].AddNew();
                var stt = grid.Rows.Count;
                if (column1 != null) row.Cells[column1].Value = stt;
                if (column2 == null) return;
                row.Cells[column2].Activate();
                grid.PerformAction(UltraGridAction.EnterEditMode);
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected virtual void LoadFormDetail(){}

        public void LoadForm(){LoadFormDetail();}

        public void uG_DeleteRow()
        {
            DeleteRow();
        }

        protected virtual void DeleteRow(){}

        protected void DeleteRowGrid(UltraGrid grid, string columnId, string columnname)
        {
            try
            {
                if (grid.Selected.Rows.Count > 0)
                {
                    if (DialogResult.Yes ==
                        MessageBox.Show(FormResource.msgHoixoa, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question))
                    {
                        foreach (var row in grid.Selected.Rows)
                        {
                            var id = row.Cells[columnId].Text;
                            if (!string.IsNullOrEmpty(id))
                            {
                                IdDelete.Add(int.Parse(id));
                            }
                        }
                        grid.DeleteSelectedRows(false);
                    }
                }
                else if (grid.ActiveRow != null)
                {
                    var index = grid.ActiveRow.Index;
                    if (string.IsNullOrEmpty(grid.ActiveRow.Cells[columnname].Text)
                        && string.IsNullOrEmpty(grid.ActiveRow.Cells[columnId].Text))
                    {
                        B = true;
                        grid.ActiveRow.Delete(false);
                        if (index <= 0) return;
                        grid.Rows[index - 1].Cells[2].Activate();
                        //grid.PerformAction(UltraGridAction.EnterEditMode);
                        return;
                    }
                    if (DialogResult.Yes ==
                        MessageBox.Show(FormResource.msgHoixoa, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question))
                    {
                        B = true;
                        var idStr = grid.ActiveRow.Cells[columnId].Text;
                        if (!string.IsNullOrEmpty(idStr))
                            IdDelete.Add(int.Parse(idStr));
                        grid.ActiveRow.Delete(false);
                        if (index > 0)
                            grid.Rows[index - 1].Cells[columnname].Activate();
                        else
                            grid.Rows[index].Cells[columnname].Activate();
                        //grid.PerformAction(UltraGridAction.EnterEditMode);
                    }
                }
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Save()
        {
            Invoke((Action)SaveDetail);
        }

        protected virtual void SaveDetail(){}

        public void Xoa()
        {
            if (DialogResult.Yes ==
                    MessageBox.Show(FormResource.msgHoixoa, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question))
            {
                XoaDetail();
            }
           
        }

        protected virtual void XoaDetail() {}
    }
}
