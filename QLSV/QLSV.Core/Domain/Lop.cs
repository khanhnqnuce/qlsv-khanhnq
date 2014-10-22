namespace QLSV.Core.Domain
{
    public class Lop
    {
        public virtual int ID { get; set; }
        public virtual string MaLop { get; set; }
        public virtual string GhiChu { get; set; }
        public virtual int IdKhoa { get; set; }
        public virtual Khoa Khoa { get; set; }
    }
}
