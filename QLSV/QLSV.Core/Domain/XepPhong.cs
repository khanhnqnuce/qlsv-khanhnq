namespace QLSV.Core.Domain
{
    public class XepPhong
    {
        public virtual int IdSV { get; set; }
        public virtual int IdPhong { get; set; }
        public virtual PhongThi PhongThi { get; set; }
    }
}
