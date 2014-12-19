using System;

namespace QLSV.Core.Domain
{
    public class SinhVien
    {
        public virtual int MaSV { get; set; }
        public virtual string HoSV { get; set; }
        public virtual string TenSV { get; set; }
        public virtual string NgaySinh { get; set; }
        public virtual int IdLop { get; set; }
    }
}
