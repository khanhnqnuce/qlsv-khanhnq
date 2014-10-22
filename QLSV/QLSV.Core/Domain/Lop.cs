namespace QLSV.Core.Domain
{
    public class Lop
    {
        public virtual int ID { get; set; }
        public virtual string MaLop { get; set; }
        public virtual string MaKhoa { get; set; }
        public virtual string GhiChu { get; set; }

        //public virtual int IDKhoa { get; set; }

        //public Lop() { }
        //public virtual Khoa ThongTinKhoa { get; set; }
    }
}
