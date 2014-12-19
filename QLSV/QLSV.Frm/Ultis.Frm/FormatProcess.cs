using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinMaskedEdit;
using QLSV.Core.Domain;
using QLSV.Core.LINQ;
using QLSV.Core.Service;
namespace QLSV.Frm.Ultis.Frm
{
    public static class FormatProcess
    {
        /// <summary>
        /// Hàm Format định dạng số theo mẫu cho Grid
        /// </summary>
        /// <param name="column">UltraGridColumn</param>
        public static void FormatNumberic(this UltraGridColumn @column)
        {
            //@column.Format = @"n";
            @column.MaskInput = @"{LOC}nnnnnn";
            @column.PromptChar = ' ';
            //@column.MaskDataMode = MaskMode.IncludeLiteralsWithPadding;
            //@column.MaskDisplayMode = MaskMode.IncludeBoth;
            //@column.MaskClipMode = MaskMode.IncludeLiterals;
            //@column.CellAppearance.TextHAlign = HAlign.Center;
        }

        public static void FormatInteger(this UltraGridColumn @column)
        {
            @column.Format = @"n";
            @column.MaskInput = @"{LOC}-nnn,nnn,nnn,nnn,nnn";
            @column.MaskDataMode = MaskMode.IncludeLiteralsWithPadding;
            @column.MaskDisplayMode = MaskMode.IncludeBoth;
            @column.MaskClipMode = MaskMode.IncludeLiterals;
            @column.PromptChar = ' ';
        }
        public static void FormatMonney(this UltraGridColumn @column)
        {
            @column.Format = @"n";
            @column.MaskInput = @"{LOC}-nnn,nnn,nnn,nnn,nnn.nn";
            @column.MaskDataMode = MaskMode.IncludeLiteralsWithPadding;
            @column.MaskDisplayMode = MaskMode.IncludeBoth;
            @column.MaskClipMode = MaskMode.IncludeLiterals;
            @column.PromptChar = ' ';
        }

        public static void Loadcbokhoa(this UltraCombo @ultraCombo)
        {
            @ultraCombo = new UltraCombo
            {
                DataSource = QlsvSevice.Load<Khoa>(),
                ValueMember = "ID",
                DisplayMember = "TenKhoa"
            };
            @ultraCombo.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
            @ultraCombo.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
            @ultraCombo.Rows.Band.Columns["ID"].Hidden = true;
            @ultraCombo.Rows.Band.Columns["MaKhoa"].Hidden = true;
            @ultraCombo.Rows.Band.Columns["TenKhoa"].Width = 400;
            @ultraCombo.DisplayLayout.Bands[0].Columns["TenKhoa"].Header.Caption = @"Khoa";
            @ultraCombo.DisplayLayout.Bands[0].Columns["TenKhoa"].SortIndicator = SortIndicator.Ascending;
            //@ultraCombo.DropDownWidth = 0;
            //@column.Style = ColumnStyle.DropDownList;
        }

        public static void FormatMobile(this UltraGridColumn @column)
        {
            @column.MaskInput = @"{LOC}####-###-####";
            @column.MaskDataMode = MaskMode.IncludeLiteralsWithPadding;
            @column.MaskDisplayMode = MaskMode.IncludeBoth;
            @column.MaskClipMode = MaskMode.IncludeLiterals;
            @column.PromptChar = ' ';
        }
        public static void FormatTime(this UltraGridColumn @column)
        {
            @column.Format = @"hh:mm";
            @column.MaskInput = @"{LOC}hh:mm";
            @column.MaskDataMode = MaskMode.IncludeLiteralsWithPadding;
            @column.MaskDisplayMode = MaskMode.IncludeBoth;
            @column.MaskClipMode = MaskMode.IncludeLiterals;
            @column.PromptChar = ' ';
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="column">cột của grid</param>
        public static void Loadcbokhoa(this UltraGridColumn @column)
        {
            var ultraCombo = new UltraCombo
            {
                DataSource = LoadData.Load(15),
                ValueMember = "ID",
                DisplayMember = "TenKhoa"
            };
            ultraCombo.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
            ultraCombo.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
            ultraCombo.Rows.Band.Columns["STT"].Hidden = true;
            ultraCombo.Rows.Band.Columns["ID"].Hidden = true;
            ultraCombo.Rows.Band.Columns["MaKhoa"].Hidden = true;
            ultraCombo.Rows.Band.Columns["TenKhoa"].Width = 350;
            ultraCombo.DisplayLayout.Bands[0].Columns["TenKhoa"].Header.Caption = @"Khoa";
            ultraCombo.DisplayLayout.Bands[0].Columns["TenKhoa"].SortIndicator = SortIndicator.Ascending;
            ultraCombo.DropDownWidth = 0;
            @column.EditorComponent = ultraCombo;
            //@column.Style = ColumnStyle.DropDownList;
        }

        public static void LoadcboLop(this UltraGridColumn @column)
        {
            var ultraCombo = new UltraCombo
            {
                DataSource = QlsvSevice.Load<Lop>(),
                ValueMember = "ID",
                DisplayMember = "MaLop"
            };
            ultraCombo.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
            ultraCombo.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
            ultraCombo.Rows.Band.Columns["ID"].Hidden = true;
            ultraCombo.Rows.Band.Columns["IdKhoa"].Hidden = true;
            ultraCombo.Rows.Band.Columns["GhiChu"].Hidden = true;
            ultraCombo.Rows.Band.Columns["Khoa"].Hidden = true;
            ultraCombo.Rows.Band.Columns["MaLop"].Width = 200;
            ultraCombo.DropDownWidth = 0;
            ultraCombo.DisplayLayout.Bands[0].Columns["MaLop"].Header.Caption = @"Lớp";
            ultraCombo.DisplayLayout.Bands[0].Columns["MaLop"].SortIndicator = SortIndicator.Ascending;
            @column.EditorComponent = ultraCombo;
            //var a = (UltraCombo)@column.EditorComponent;
            //a.DisplayLayout.Bands[0].ColumnFilters["ID"].FilterConditions.Add(FilterComparisionOperator.Equals, 2);
            //a.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
        }
    }
}
