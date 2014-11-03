using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using QLSV.Core.Utils.Core;

namespace QLSV.Frm.Base
{
    public partial class FunctionControlHasGrid : UserControl
    {
        protected IList<int> IdDelete = new List<int>();

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
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                else
                    MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        protected virtual DataTable GetTable()
        {
            return null;
        }

        protected virtual void LoadGrid(){}

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
                            var id = row.Cells[columnId].Value.ToString();
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
                    var id = grid.ActiveRow.Index;
                    if (string.IsNullOrEmpty(grid.ActiveRow.Cells[columnname].Text)
                        && string.IsNullOrEmpty(grid.ActiveRow.Cells[columnId].Text))
                    {
                        grid.ActiveRow.Delete(false);
                        if (id <= 0) return;
                        grid.Rows[id - 1].Cells[2].Activate();
                        grid.PerformAction(UltraGridAction.EnterEditMode);
                        return;
                    }
                    if (DialogResult.Yes ==
                        MessageBox.Show(FormResource.msgHoixoa, FormResource.MsgCaption, MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question))
                    {
                        var idStr = grid.ActiveRow.Cells[columnId].Value.ToString();
                        if (!string.IsNullOrEmpty(idStr))
                            IdDelete.Add(int.Parse(idStr));
                        grid.ActiveRow.Delete(false);
                        if (id <= 0) return;
                        grid.Rows[id - 1].Cells[2].Activate();
                        grid.PerformAction(UltraGridAction.EnterEditMode);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(FormResource.msgLostConnect))
                {
                    MessageBox.Show(FormResource.txtLoiDB);
                }
                else
                    MessageBox.Show(ex.Message);
                Log2File.LogExceptionToFile(ex);
            }
        }

        public void Save()
        {
            SaveDetail();
        }

        protected virtual void SaveDetail(){}

        public void Xoa()
        {
            XoaDetail();
        }

        protected virtual void XoaDetail() {}

        private static void Stt(UltraGrid grid, string columnname)
        {
            for (var i = 0; i < grid.Rows.Count; i++)
            {
                grid.Rows[i].Cells[columnname].Value = i + 1;
            }
        }
    }
}
