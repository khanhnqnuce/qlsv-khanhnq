namespace QLSV.Core.Domain
{
    public class BaiLam
    {
        public virtual int IdKyThi { get; set; }

        public virtual int MaSV { get; set; }

        public virtual string MaDe { get; set; }

        public virtual string KetQua { get; set; }

        public virtual string MaHoiDong { get; set; }
        public virtual string MaLoCham { get; set; }
        public virtual string TenFile { get; set; }

        public virtual double DiemThi { get; set; }
    }
}
