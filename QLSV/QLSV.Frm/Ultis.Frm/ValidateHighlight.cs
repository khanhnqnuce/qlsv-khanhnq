using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using QLSV.Data.Utils.Data;

namespace QLSV.Frm.Ultis.Frm
{
    internal class ValidateHighlight
    {
        private IList<ToolTip> _toolTips;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        public ValidateHighlight()
        {
            _toolTips = new List<ToolTip>();
        }

        private static void HighlightGridCellClear(UltraGridCell cell)
        {
            cell.Appearance.ResetBackColor();
            cell.Appearance.ResetForeColor();
            cell.ToolTipText = string.Empty;
        }

        private static void HighlightGridCellClear(UltraGridRow row)
        {
            foreach (var cell in row.Cells)
            {
                HighlightGridCellClear(cell);
            }
        }

        /// <summary>
        /// Báo lỗi
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="tooltip"></param>
        private static void HighlightGridCellSet(UltraGridCell cell, string tooltip)
        {
            if (!string.IsNullOrEmpty(tooltip))
            {
                cell.Appearance.BackColor = Color.FromArgb(255, 160, 154);
                cell.Appearance.ForeColor = Color.White;
                cell.ToolTipText = tooltip;
            }
        }

        /// <summary>
        /// Highight ô có dữ liệu không hợp lệ
        /// </summary>
        /// <param name="row">Dòng cần kiểm tra</param>
        /// <param name="inputTypes">Danh sách kiểu tương ứng với từng cột</param>
        /// <returns>True nếu có lỗi</returns>
        public static bool UltraGrid(UltraGridRow row, IList<InputType> inputTypes)
        {
            var result = false;
            HighlightGridCellClear(row);
            var listInputParam = new List<InputParam>();
            for (var i = 0; i < row.Cells.Count; i++)
            {
                if (inputTypes[i] == InputType.NgayThang)
                {
                    var ngaythang = DateTime.Parse(row.Cells[i].Value.ToString()).ToString("dd/MM/yyyy");
                    listInputParam.Add(new InputParam
                    {
                        Input = ngaythang,
                        InputType = inputTypes[i]
                    });
                }
                else
                {
                    listInputParam.Add(new InputParam
                    {
                        Input = row.Cells[i].Value.ToString(),
                        InputType = inputTypes[i]
                    });
                }
            }
            var validate = new ValidateData();
            validate.ValDataList(listInputParam);
            if (validate.Errors.Count > 0)
            {
                for (var i = 0; i < row.Cells.Count; i++)
                {
                    if (!validate.Output[i].Result)
                    {
                        HighlightGridCellSet(row.Cells[i], validate.Output[i].MsgError);
                    }
                }
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Highight ô có dữ liệu không hợp lệ
        /// </summary>
        /// <param name="grid">UltraGrid cần kiểm tra</param>
        /// <param name="inputTypes">Danh sách kiểu tương ứng với từng cột</param>
        /// <returns>True nếu có lỗi</returns>
        public static bool UltraGrid(UltraGrid grid, IList<InputType> inputTypes)
        {
            var result = false;
            foreach (var row in grid.Rows)
            {
                if (!result)
                    result = UltraGrid(row, inputTypes);
                else
                    UltraGrid(row, inputTypes);
            }
            return result;
        }

        private static void HighlightTextBoxClear(Control textBox)
        {
            textBox.ResetBackColor();
            textBox.ResetForeColor();
        }

        private static void HighlightTextBoxSet(Control textBox, ToolTip tooltip, string captionTooltip)
        {
            textBox.BackColor = Color.Red;
            textBox.ForeColor = Color.White;
            tooltip.SetToolTip(textBox, captionTooltip);
        }

        /// <summary>
        /// Kiểm tra list các textbox Highight text có dữ liệu không hợp lệ
        /// </summary>
        /// <param name="input">Từ điển textbox với kiểu dữ liệu tương ứng</param>
        /// <returns>True nếu có lỗi</returns>
        public bool TextBox(IDictionary<TextBox, InputType> input)
        {
            var result = false;
            var listInputParam = new List<InputParam>();
            ClearTooltip();
            _toolTips = new List<ToolTip>();
            for (var i = 0; i < input.Count; i++)
            {
                _toolTips.Add(new ToolTip
                {
                    IsBalloon = true,
                    InitialDelay = 0,
                    ShowAlways = true,
                    ToolTipIcon = ToolTipIcon.Error,
                    ToolTipTitle = "Lỗi"
                });
            }
            foreach (var inputValue in input)
            {
                HighlightTextBoxClear(inputValue.Key);
                listInputParam.Add(new InputParam
                {
                    Input = inputValue.Key.Text.Trim(),
                    InputType = inputValue.Value
                });
            }
            var validate = new ValidateData();
            validate.ValDataList(listInputParam);
            if (validate.Errors.Count > 0)
            {
                var index = 0;
                foreach (var item in input)
                {
                    if (!validate.Output[index].Result)
                    {
                        HighlightTextBoxSet(item.Key, _toolTips[index], validate.Output[index].MsgError);
                    }
                    index++;
                }
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Kiểm tra 1 textbox truyền vào có hợp lệ không
        /// </summary>
        /// <param name="txtname">truyền vào 1 textbox</param>
        /// <param name="input">truyền vào kiểu kiểm tra</param>
        /// <returns></returns>
        public InputParam CheckTextbox(TextBox txtname, InputType input)
        {
            var inputparam = new InputParam
            {
                Input = txtname.Text,
                InputType = input
            };
            var validate = new ValidateData();
            return ValidateData.ValDataOne(inputparam);
        }

        /// <summary>
        /// Hàm hủy
        /// </summary>
        private void ClearTooltip()
        {
            if (_toolTips.Count > 0)
            {
                foreach (var toolTip in _toolTips)
                {
                    toolTip.Dispose();
                }
            }
        }

        ~ValidateHighlight()
        {
            ClearTooltip();
        }
    }
}
