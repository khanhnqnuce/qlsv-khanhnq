using System;
using System.Collections.Generic;
using System.Linq;
using QLSV.Core.Domain;
using QLSV.Core.Utils.Core;

namespace QLSV.Core.LINQ
{
    public static class UpdateData
    {
        private static readonly Connect Conn = new Connect();

        /// <summary>
        /// Update thông tin 1 tài khoản
        /// </summary>
        /// <returns>true</returns>
        public static bool UpdateTaiKhoan(Taikhoan item)
        {
            try
            {
                Conn.ExcuteQuerySql("Update TAIKHOAN set HoTen = N'" + item.HoTen + "', Quyen = N'" + item.Quyen + "' where ID = " + item.ID + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Update thông tin nhiều tài khoản
        /// </summary>
        /// <param name="list"></param>
        /// <returns>true</returns>
        public static bool UpdateTaiKhoan(IList<Taikhoan> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdateTaiKhoan(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Update mật khẩu cho 1 tk
        /// </summary>
        /// <returns>true</returns>
        public static bool UpdateMatKhau(Taikhoan item)
        {
            try
            {
                Conn.ExcuteQuerySql("Update TAIKHOAN set MatKhau = N'" + item.MatKhau + "' where ID = " + item.ID + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Update mật khẩu cho nhiều tk
        /// </summary>
        /// <param name="list"></param>
        /// <returns>true</returns>
        public static bool UpdateMatKhau(IList<Taikhoan> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdateMatKhau(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Update Thông tin khoa
        /// </summary>
        /// <param name="list"></param>
        /// <returns>true</returns>
        public static bool UpdateKhoa(Khoa item)
        {
            try
            {
                Conn.ExcuteQuerySql("UPDATE KHOA set MaKhoa = N'" + item.MaKhoa + "', TenKhoa = N'" + item.TenKhoa + "' where ID = " + item.ID + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Update Thông tin khoa
        /// </summary>
        /// <param name="list"></param>
        /// <returns>true</returns>
        public static bool UpdateKhoa(IList<Khoa> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdateKhoa(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Update Thông tin lop
        /// </summary>
        /// <returns>true</returns>
        public static bool UpdateLop(Lop item)
        {
            try
            {
                Conn.ExcuteQuerySql("UPDATE LOP set MaLop = N'" + item.MaLop + "', IdKhoa = " + item.IdKhoa + ", GhiChu = N'" + item.GhiChu + "' where ID = " + item.ID + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Update Thông tin lop
        /// </summary>
        /// <param name="list"></param>
        /// <returns>true</returns>
        public static bool UpdateLop(IList<Lop> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdateLop(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Sửa thông tin 1 sinh viên
        /// </summary>
        /// <returns>true</returns>
        public static bool UpdateSV(SinhVien item)
        {
            try
            {
                Conn.ExcuteQuerySql("update SINHVIEN set HoSV = N'" + item.HoSV + "',TenSV = N'" + item.TenSV + "',NgaySinh = '" + item.NgaySinh + "',IdLop = " + item.IdLop + " WHERE MaSV = " + item.MaSV + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Sửa thông tin của nhiều sinh viên
        /// </summary>
        /// <returns>true</returns>
        public static bool UpdateSV(IList<SinhVien> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdateSV(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Sửa thông tin 1 kỳ thi
        /// </summary>
        /// <returns>true</returns>
        public static bool UpdateKyThi(Kythi item)
        {
            try
            {
                Conn.ExcuteQuerySql("update KYTHI set MaKT = N'" +
                    item.MaKT + "',TenKT = N'" + item.TenKT + "',NgayThi = '" +
                    item.NgayThi + "',TGLamBai = " + item.TGLamBai + ",TGBatDau = N'" +
                    item.TGBatDau + "' ,TGKetThuc = N'" +
                    item.TGKetThuc + "' WHERE ID = " + item.ID + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Sửa thông tin của nhiều kỳ thi
        /// </summary>
        /// <returns>true</returns>
        public static bool UpdateKyThi(IList<Kythi> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdateKyThi(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Sửa thông tin 1 phòng thi
        /// </summary>
        /// <returns>true</returns>
        public static bool UpdatePhongThi(PhongThi item)
        {
            try
            {
                Conn.ExcuteQuerySql("update PHONGTHI set TenPhong = N'" +
                    item.TenPhong + "',SucChua = " + item.SucChua + ",GhiChu = N'" +
                    item.GhiChu + "' WHERE ID = " + item.ID + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Sửa thông tin của nhiều Phòng thi
        /// </summary>
        /// <returns>true</returns>
        public static bool UpdatePhongThi(IList<PhongThi> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdatePhongThi(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Sửa lại đáp án đúng của câu hỏi
        /// </summary>
        /// <returns>true</returns>
        public static bool UpdateDapAn(DapAn item)
        {
            try
            {
                Conn.ExcuteQuerySql("UPDATE DAPAN SET MaMon = N'" + item.MaMon + "',MaDe = N'" + item.MaDe + "'," +
                                   "CauHoi = N'" + item.CauHoi + "',Dapan = N'" + item.Dapan + "' WHERE ID = " + item.ID + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }
        
        /// <summary>
        /// Sửa lại đáp án đúng của câu hỏi
        /// </summary>
        /// <param name="list">danh sách đối tượng đáp án được sửa</param>
        /// <returns>true</returns>
        public static bool UpdateDapAn(IList<DapAn> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdateDapAn(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Sửa lại đáp án đúng của câu hỏi
        /// </summary>
        /// <param name="list">danh sách đối tượng đáp án được sửa</param>
        /// <returns>true</returns>
        public static bool UpdateThangDiem(IList<DapAn> list)
        {
            try
            {
                foreach (
                    var sql in
                        list.Select(
                            item => "UPDATE DapAn SET ThangDiem = "+item.ThangDiem+" WHERE ID = " + item.ID + ""))
                {
                    Conn.ExcuteQuerySql(sql);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        //public static bool UpdateBaiLam(IList<BaiLam> list)
        //{
            //try
            //{
            //    foreach (
            //        var sql in
            //            list.Select(
            //                item => "UPDATE BaiLam SET MaSV = N'" + item.MaSV + "',MaDe = N'" + item.MaDe + "'," +
            //                        "KetQua = N'" + item.KetQua + "' WHERE ID = " + item.ID + ""))
            //    {
            //        Conn.ExcuteQuerySql(sql);
            //    }
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    Log2File.LogExceptionToFile(ex);
            //    return false;
            //}
        //}

        /// <summary>
        /// chuyển phòng thi cho sinh viên
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public static bool UpdateXepPhong(XepPhong hs)
        {
            try
            {
                Conn.ExcuteQuerySql("update XEPPHONG set IdPhong = " + hs.IdPhong + " where IdSV = " + hs.IdSV + " and IdKyThi =" + hs.IdKyThi + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// giảm số lượng sinh viên lên 1 khi xếp 1 sinh viên vào phòng
        /// </summary>
        /// <returns>true nếu thành công</returns>
        public static bool UpdateTangSiSo(int idphong, int idkt)
        {
            try
            {
                Conn.ExcuteQuerySql("update KT_PHONG set SiSo = ((select SiSo from KT_PHONG where IdPhong = " + idphong + " and IdKyThi = " + idkt + ") + " + 1 + ") where IdPhong = " + idphong + " and IdKyThi = " + idkt + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// giảm số lượng sinh viên lên 1 khi xếp 1 sinh viên vào phòng
        /// </summary>
        /// <returns>true nếu thành công</returns>
        public static bool UpdateGiamSiSo(int idphong, int idkt)
        {
            try
            {
                Conn.ExcuteQuerySql("update KT_PHONG set SiSo = ((select SiSo from KT_PHONG where IdPhong = " + idphong + " and IdKyThi = " + idkt + ") - " + 1 + ") where IdPhong = " + idphong + " and IdKyThi = " + idkt + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// giảm số lượng sinh viên lên 1 khi xếp 1 sinh viên vào phòng
        /// </summary>
        /// <returns>true nếu thành công</returns>
        public static bool UpdateKtPhong(int idphong1, int idphong2, int idkt)
        {
            try
            {
                UpdateTangSiSo(idphong1, idkt);
                UpdateGiamSiSo(idphong2, idkt);
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }
        
        /// <summary>
        /// Sau khi xóa tất cả sinh viên đã xếp phòng thì sĩ số phòng giảm về 0
        /// </summary>
        /// <returns>true</returns>
        public static bool ResetPhongThi()
        {
            try
            {
                Conn.ExcuteQuerySql("update PhongThi set SoLuong = " + 0 + " ");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Sửa mã sinh viên trong bảng bài làm
        /// </summary>
        /// <param name="masv1">Mã sinh viên ban đầu</param>
        /// <param name="masv2">Mã sv đã sửa</param>
        /// <returns>true</returns>
        public static bool UpdateMaSinhVien(int masv1, int masv2)
        {
            try
            {
                Conn.ExcuteQuerySql("update BaiLam set MaSV = " + masv2 + " WHERE ID = " + masv1 + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// Chấm điểm thi cho bài làm của sinh viên
        /// </summary>
        /// <returns>true</returns>
        public static bool UpdateDiemThi(IList<BaiLam> list)
        {
            try
            {
                foreach (var item in list)
                {
                    Conn.ExcuteQuerySql("update BaiLam set DiemThi = " + item.DiemThi + " WHERE MaSV = " + item.MaSV + "");
                }
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }
    }
}
