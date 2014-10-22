using System.Collections.Generic;

namespace QLSV.Core.Domain
{
    public class Khoa
    {
        public virtual int ID { get; set; }
        public virtual string MaKhoa { get; set; }
        public virtual string TenKhoa { get; set; }
        public virtual ISet<SinhVien> SinhVien { get; set; }
        public virtual ISet<Lop> Lop { get; set; }
        public Khoa()
        {
            SinhVien = new HashSet<SinhVien>();
            Lop = new HashSet<Lop>();
        }
    }
}
