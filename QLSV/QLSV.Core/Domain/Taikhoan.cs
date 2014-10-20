namespace QLSV.Core.Domain
{
    public class Taikhoan
    {
        public virtual int ID { get; set; }

        public virtual string TaiKhoan { get; set; }

        public virtual string HoTen { get; set; }

        public virtual string MatKhau { get; set; }

        public virtual string Quyen { get; set; }
    }
}
