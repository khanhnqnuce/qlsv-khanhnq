namespace QLSV.Core.Domain
{
    public class SinhVien
    {
        public virtual int ID { get; set; }
        public virtual string MaSinhVien { get; set; }
        public virtual string HoSinhVien { get; set; }
        public virtual string TenSinhVien { get; set; }
        public virtual string MaLop { get; set; }
    }
}
