using System;
using System.Collections.Generic;
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
                Conn.ExcuteQuerySql("Update TAIKHOAN set HoTen = N'" + item.HoTen + "', Quyen = N'" + item.Quyen +
                                    "' where ID = " + item.ID + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

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

        public static bool UpdateMatKhau(string taikhoan, string matkhau)
        {
            try
            {
                Conn.ExcuteQuerySql("Update TAIKHOAN set MatKhau = N'" + matkhau + "' where TaiKhoan = N'" + taikhoan +
                                    "'");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

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
        /// <returns>true</returns>
        public static bool UpdateKhoa(Khoa item)
        {
            try
            {
                Conn.ExcuteQuerySql("UPDATE KHOA set MaKhoa = N'" + item.MaKhoa + "', TenKhoa = N'" + item.TenKhoa +
                                    "' where ID = " + item.ID + "");
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
                Conn.ExcuteQuerySql("UPDATE LOP set MaLop = N'" + item.MaLop + "', IdKhoa = " + item.IdKhoa +
                                    ", GhiChu = N'" + item.GhiChu + "' where ID = " + item.ID + "");
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
        public static bool UpdateSv(SinhVien item)
        {
            try
            {
                Conn.ExcuteQuerySql("update SINHVIEN set HoSV = N'" + item.HoSV + "',TenSV = N'" + item.TenSV +
                                    "',NgaySinh = '" + item.NgaySinh + "',IdLop = " + item.IdLop + " WHERE MaSV = " +
                                    item.MaSV + "");
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
        public static bool UpdateSv(IList<SinhVien> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdateSv(item);
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
        public static bool UpdateTrangThaiKt(Kythi item)
        {
            try
            {
                Conn.ExcuteQuerySql("update KYTHI set TrangThai = '"+item.TrangThai+"' WHERE ID = " + item.ID + "");
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
                Conn.ExcuteQuerySql("update KYTHI set TenKT = N'" + item.TenKT + "',NgayThi = '" +
                                    item.NgayThi + "',TGLamBai = N'" + item.TGLamBai + "',TGBatDau = N'" +
                                    item.TGBatDau + "' ,TGKetThuc = N'" +
                                    item.TGKetThuc + "', GhiChu = N'"+item.GhiChu+"' WHERE ID = " + item.ID + "");
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
                Conn.ExcuteQuerySql("UPDATE DAPAN SET Dapan = '" + item.Dapan + "' " +
                                    "WHERE MaMon = '" + item.MaMon + "' and MaDe = '" + item.MaDe + "' " +
                                    "and CauHoi = " + item.CauHoi + " and IdKyThi= " + item.IdKyThi + "");
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
        /// Sửa lại đáp án đúng của 1 câu hỏi
        /// </summary>
        /// <returns>true</returns>
        public static bool UpdateThangDiem(DapAn item)
        {
            try
            {
                Conn.ExcuteQuerySql("UPDATE DAPAN SET ThangDiem = " + item.ThangDiem + " " +
                                    "WHERE MaMon = '" + item.MaMon + "' and MaDe = '" + item.MaDe + "' " +
                                    "and CauHoi =" + item.CauHoi + " and IdKyThi= " + item.IdKyThi + "");
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
                foreach (var item in list)
                {
                    UpdateThangDiem(item);
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
        /// sửa phòng thi cho sinh viên
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public static bool UpdateXepPhong(XepPhong hs)
        {
            try
            {
                Conn.ExcuteQuerySql("update XEPPHONG set IdPhong = " + hs.IdPhong + " where IdSV = " + hs.IdSV +
                                    " and IdKyThi =" + hs.IdKyThi + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// sửa phòng thi cho sinh viên
        /// </summary>
        /// <returns></returns>
        public static bool UpdateXepPhong(IList<XepPhong> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdateXepPhong(item);
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
        /// khi xóa bảng KT_PHONG thi Update idphong bảng XEPPHONG thành NULL
        /// </summary>
        /// <returns></returns>
        public static bool UpdateXepPhongNull(int idkythi)
        {
            try
            {
                Conn.ExcuteQuerySql(" update XEPPHONG set IdPhong = null where IdKyThi = " + idkythi + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// khi xóa bảng KT_PHONG thi Update idphong bảng XEPPHONG thành NULL
        /// </summary>
        /// <returns></returns>
        public static bool UpdateXepPhongNull(XepPhong item)
        {
            try
            {
                Conn.ExcuteQuerySql(" update XEPPHONG set IdPhong = null where IdPhong = " + item.IdPhong +
                                    " and IdKyThi = " + item.IdKyThi + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// khi xóa bảng KT_PHONG thi Update idphong bảng XEPPHONG thành NULL
        /// </summary>
        /// <returns></returns>
        public static bool UpdateXepPhongNull(IList<XepPhong> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdateXepPhongNull(item);
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
        /// xóa 1 sinh viên đã được xếp phòng
        /// </summary>
        /// <returns></returns>
        public static bool UpdateXP_Null(XepPhong item)
        {
            try
            {
                Conn.ExcuteQuerySql("update XEPPHONG set IdPhong = null where IdSV = " + item.IdSV + " and IdKyThi = " +
                                    item.IdKyThi + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// khi xóa bảng KT_PHONG thi Update idphong bảng XEPPHONG thành NULL
        /// </summary>
        /// <returns></returns>
        public static bool UpdateXP_Null(IList<XepPhong> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdateXP_Null(item);
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
        /// giảm số lượng sinh viên lên 1 khi xếp 1 sinh viên vào phòng
        /// </summary>
        /// <returns>true nếu thành công</returns>
        public static bool UpdateTangSiSo(int idphong, int idkt)
        {
            try
            {
                Conn.ExcuteQuerySql("update KT_PHONG set SiSo = ((select SiSo from KT_PHONG where IdPhong = " + idphong +
                                    " and IdKyThi = " + idkt + ") + " + 1 + ") where IdPhong = " + idphong +
                                    " and IdKyThi = " + idkt + "");
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
                Conn.ExcuteQuerySql("update KT_PHONG set SiSo = ((select SiSo from KT_PHONG where IdPhong = " + idphong +
                                    " and IdKyThi = " + idkt + ") - " + 1 + ") where IdPhong = " + idphong +
                                    " and IdKyThi = " + idkt + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        public static bool UpdateGiamSiSo(IList<KTPhong> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdateGiamSiSo(item.IdPhong, item.IdKyThi);
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
        /// sửa lại SiSo trong bảng KT_PHONG khi xếp phòng xong
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool UpdateKtPhong(KTPhong item)
        {
            try
            {
                Conn.ExcuteQuerySql("update KT_PHONG set SiSo = " + item.SiSo + " where IdPhong = " + item.IdPhong +
                                    " and IdKyThi =" + item.IdKyThi + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        public static bool UpdateKtPhong(IList<KTPhong> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdateKtPhong(item);
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
        /// khi xóa bảng XEPPHONG thì cho sĩ số phòng về 0
        /// </summary>
        /// <returns>true nếu thành công</returns>
        public static bool UpdateKtPhong(int idkythi)
        {
            try
            {
                Conn.ExcuteQuerySql(" update KT_PHONG set SiSo = 0 where IdKyThi = " + idkythi + "");
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
        /// <param name="masv1">ma sv mới</param>
        /// <param name="masv2">Mã sv cần sửa </param>
        /// <param name="idkythi"></param>
        /// <param name="made"></param>
        /// <returns>true</returns>
        public static bool UpdateMaSinhVien(int masv1, int masv2, int idkythi, string made)
        {
            try
            {
                Conn.ExcuteQuerySql("update BAILAM set MaSV = " + masv1 + " WHERE MaSV = " + masv2 + " and IdKyThi = " +
                                    idkythi + " and MaDe = N'" + made + "'");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// sửa lại điểm thi trong bản BAILAM
        /// </summary>
        /// <returns>true</returns>
        public static bool UpdateDiemThi(BaiLam item)
        {
            try
            {
                Conn.ExcuteQuerySql("update BAILAM set DiemThi = " + item.DiemThi + " WHERE MaSV = " + item.MaSV +
                                    " and IdKyThi = " + item.IdKyThi + "");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        /// <summary>
        /// sửa lại điểm thi trong bản BAILAM
        /// </summary>
        /// <returns>true</returns>
        public static bool UpdateDiemThi(IList<BaiLam> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdateDiemThi(item);
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
        /// sửa lại năm học
        /// </summary>
        /// <returns>true</returns>
        public static bool UpdateNamHoc(NamHoc item)
        {
            try
            {
                Conn.ExcuteQuerySql("update NAMHOC set NamHoc = '" + item.namhoc + "' WHERE ID = "+item.ID+"");
                return true;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return false;
            }
        }

        public static bool UpdateNamHoc(IList<NamHoc> list)
        {
            try
            {
                foreach (var item in list)
                {
                    UpdateNamHoc(item);
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
