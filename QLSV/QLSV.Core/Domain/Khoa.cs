using System.Collections.Generic;

namespace QLSV.Core.Domain
{
    public class Khoa
    {
        public virtual int ID { get; set; }
        public virtual string MaKhoa { get; set; }
        public virtual string TenKhoa { get; set; }

        //public virtual ISet<Lop> ThongTinLop { get; set; }
        //public Khoa()
        //{
        //    ThongTinLop = new HashSet<Lop>();
        //}
    }
}
