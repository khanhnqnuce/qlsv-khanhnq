using System;

namespace QLSV.Core.Domain
{
    public class Kythi
    {
        public virtual int ID { get; set; }

        public virtual string MaKyThi { get; set; }

        public virtual string TenKyThi { get; set; }

        public virtual string NgayThi { get; set; }

        public virtual int ThoiGianLamBai { get; set; }

        public virtual string ThoiGianBatDau { get; set; }

        public virtual string ThoiGianKetThuc { get; set; }
    }
}
