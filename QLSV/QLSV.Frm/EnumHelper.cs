namespace QLSV.Frm
{
    public class InputParamExcel
    {
        public string Ten { get; set; }
        public int ViTri { get; set; }
        public InputTypeExcel ThuocTinh { get; set; }
    }

    public enum InputTypeExcel
    {
        Double,
        Integer,
        Boolean,
        DateTime,
        String
    }
}
