using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;

namespace QLSV.Frm.Base
{
    public partial class FunctionControlHasGrid : Form
    {
        protected List<int> IdDelete = new List<int>();

        protected virtual void InsertRow() { }

        protected void InsertRow(UltraGrid grid,string column1 = null, string column2 = null)
        {
            var row = grid.DisplayLayout.Bands[0].AddNew();
            var stt = grid.Rows.Count;
            if (column1 != null) row.Cells[column1].Value = stt;
            if (column2 == null) return;
            row.Cells[column2].Activate();
            grid.PerformAction(UltraGridAction.EnterEditMode);
        }

        protected virtual DataTable GetTable()
        {
            return null;
        }

        protected virtual void LoadGrid(){}

        protected virtual void DeleteRow(){}

        protected void DeleteRowGrid(UltraGrid grid, string columnId, string columnname)
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
                    Stt(grid,"STT");
                }
            }
            else if (grid.ActiveRow != null)
            {
                var id = grid.ActiveRow.Index;
                if (string.IsNullOrEmpty(grid.ActiveRow.Cells[columnname].Text)
                    && string.IsNullOrEmpty(grid.ActiveRow.Cells[columnId].Text))
                {
                    grid.ActiveRow.Delete(false);
                    //Stt(grid, "STT");
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
                    //Stt(grid, "STT");
                    if (id <= 0) return;
                    grid.Rows[id - 1].Cells[2].Activate();
                    grid.PerformAction(UltraGridAction.EnterEditMode);
                }
            }
        }

        protected virtual void Save(){}

        protected virtual void Xoa() {}

        private static void Stt(UltraGrid grid, string columnname)
        {
            for (var i = 0; i < grid.Rows.Count; i++)
            {
                grid.Rows[i].Cells[columnname].Value = i + 1;
            }
        }
    }
}
