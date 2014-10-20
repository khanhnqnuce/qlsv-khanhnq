using System;

namespace QLSV.Core.Domain
{
    public class Kythi
    {
        public virtual int ID { get; set; }

        public virtual string MaKyThi { get; set; }

        public virtual string TenKyThi { get; set; }

        private DateTime _NgayThi;

        public virtual DateTime NgayThi
        {
            get { return _NgayThi; }
            set { _NgayThi = value; }
        }

        public virtual int ThoiGianLamBai { get; set; }

        public virtual string ThoiGianBatDau { get; set; }

        public virtual string ThoiGianKetThuc { get; set; }
    }
}
