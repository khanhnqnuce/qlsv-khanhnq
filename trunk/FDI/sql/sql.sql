-- Update khoa
CREATE TYPE [dbo].[KhoaType] AS TABLE(
      [ID] [int] NULL,
      [TenKhoa] [nvarchar](MAX) NULL   
)
GO

CREATE PROCEDURE [dbo].[sp_UpdateKhoas]
      @tbl KhoaType READONLY
AS
BEGIN
      SET NOCOUNT ON;
      --UPDATE EXISTING RECORDS
      UPDATE KHOA
      SET TenKhoa = c2.TenKhoa      
      FROM KHOA c1
      INNER JOIN @tbl c2
      ON c1.ID = c2.ID     
END

-- Sinh vien
CREATE TYPE [dbo].[SinhVienType] AS TABLE(
    [MaSV] [int]  NULL,
	[HoSV] [nvarchar](255)  NULL,
	[TenSV] [nvarchar](255)  NULL,
	[NgaySinh] [varchar](50) NULL,
	[Lop] [varchar](50) NULL   
)
GO

CREATE PROCEDURE [dbo].[sp_CheckSV]
      @tbl SinhVienType READONLY
AS
BEGIN
      select c1.* from @tbl c1 where NOT EXISTS(select c2.MaSV from SINHVIEN c2 where c1.MaSV = c2.MaSV)
END

GO

CREATE PROCEDURE [dbo].[sp_InsertSV]
      @tbl SinhVienType READONLY
AS
BEGIN
      INSERT INTO SINHVIEN(MaSV,HoSV,TenSV,NgaySinh,IdLop)
      SELECT MasV, HoSV, TenSV,NgaySinh, IdLop
      FROM (Select c1.MaSV,c1.HoSV,c1.TenSV,c1.NgaySinh,c2.ID as [IdLop] from @tbl c1 join LOP c2 on c1.Lop = c2.MaLop) c3
      WHERE c3.MaSV not in (SELECT MaSV FROM SINHVIEN)
END

GO

CREATE PROCEDURE [dbo].[sp_CheckLOP]
      @tbl SinhVienType READONLY
AS
BEGIN
      select c1.* from @tbl c1 where c1.Lop NOT IN (select MaLop from LOP)
END

-- Lop

CREATE TYPE [dbo].[LopType] AS TABLE(
      [MaLop] [varchar] (10) NULL,
      [IdKhoa] [int] NULL   
)
GO
CREATE PROCEDURE [dbo].[sp_InsertLop]
	@tbl LopType READONLY
AS
BEGIN
	INSERT INTO LOP(MaLop,IdKhoa)
	SELECT MaLop, IdKhoa
	FROM @tbl c
	WHERE c.MaLop not in (SELECT MaLop FROM LOP)
		
END

-- Phong thi

CREATE TYPE [dbo].[PhongThiType] AS TABLE(
      [ID] [int] NULL,
	  [TenPhong] [varchar] (10) NULL,
      [SucChua] [int] NULL,
	  [GhiChu] [nvarchar] (255) NULL   
)

GO

CREATE PROCEDURE [dbo].[sp_InsertPhong]
	@tbl PhongThiType READONLY
AS
BEGIN	
	
		  INSERT INTO PHONGTHI(TenPhong,SucChua,GhiChu)
		  SELECT c.TenPhong, c.SucChua, c.GhiChu
		  FROM @tbl c
		  WHERE c.TenPhong not in (SELECT TenPhong FROM PHONGTHI)		
END

GO

CREATE PROCEDURE [dbo].[sp_UpdatePhong]
	@tbl PhongThiType READONLY
AS
BEGIN
		  UPDATE PHONGTHI
			SET TenPhong = c2.TenPhong,SucChua = c2.SucChua, GhiChu = c2.GhiChu
			FROM PHONGTHI c1
			INNER JOIN @tbl c2
			ON c1.ID = c2.ID		
END

-- Xep Phong

CREATE TYPE [dbo].[XepPhongType] AS TABLE(
      [IdSV] [int] NULL,	  
      [IdKyThi] [int] NULL,
	  [IdPhong] [int] NULL
)

CREATE PROCEDURE [dbo].[sp_UpdateXepPhong]
      @tbl XepPhongType READONLY
AS
BEGIN
      UPDATE XEPPHONG
      SET IdPhong = c2.IdPhong      
      FROM XEPPHONG c1
      INNER JOIN @tbl c2
      ON (c1.IdKyThi = c2.IdKyThi and c1.IdSV = c2.IdSV)

END

GO

CREATE PROCEDURE [dbo].[sp_InsertXepPhong]
      @tbl XepPhongType READONLY
AS
BEGIN
      INSERT INTO XEPPHONG(IdSV,IdKyThi)
      SELECT c.IdSV,c.IdKyThi
      FROM @tbl c
      WHERE Not Exists (select IdKyThi,IdSV from XEPPHONG c2 where c1.IdSV = c2.IdSV and c1.IdKyThi = c2.IdKyThi)
END

Go

CREATE PROCEDURE [dbo].[sp_Ud_XepPhong_KTPhong]
      @tbl XepPhongType READONLY
AS
BEGIN
      Update XEPPHONG set IdPhong = NULL from XEPPHONG c1 inner join @tbl c2 on (c1.IdKyThi = c2.IdKyThi and c1.IdSV = c2.IdSV)
	  Update KT_PHONG set KT_PHONG.SiSo = (c3.SiSo - c4.Tong) from KT_PHONG c3 inner join (select IdKyThi,IdPhong,COUNT(*) as[Tong] from @tbl group by Idkythi,Idphong) c4
		on (c3.IdKyThi = c4.IdKyThi and c3.IdPhong = c4.IdPhong)
END

-- KT phong
CREATE TYPE [dbo].[ChonPhongType] AS TABLE(
      [IdPhong] [int] NULL,	  
      [IdKyThi] [int] NULL,
	  [SiSo] [int] NULL
)

GO

CREATE PROCEDURE [dbo].[sp_InsertKTPhong]
      @tbl ChonPhongType READONLY
AS
BEGIN
      INSERT INTO KT_PHONG(IdPhong,IdKyThi,SiSo)
      SELECT c.IdPhong,c.IdKyThi, c.SiSo
      FROM @tbl c      
END

GO

CREATE PROCEDURE [dbo].[sp_UpdateKTPhong]
      @tbl ChonPhongType READONLY
AS
BEGIN
      UPDATE KT_PHONG
      SET SiSo = c2.SiSo      
      FROM KT_PHONG c1
      INNER JOIN @tbl c2
      ON (c1.IdKyThi = c2.IdKyThi and c1.IdPhong = c2.IdPhong)

END

-- Khi update IdPhong bảng XEPPHONG thì update siso bang KT_PHONG
CREATE TRIGGER trg_XepPhong_update_IdPhong
	ON XEPPHONG
	FOR UPDATE 
	AS
		IF UPDATE(IdPhong)
		UPDATE KT_PHONG
		SET KT_PHONG.SiSo = c1.SiSo + c2.Tong
		FROM KT_PHONG c1 inner join (select IdKyThi,IdPhong,count(*) as [Tong] from inserted group by IdKyThi,IdPhong) c2
		on (c1.IdKyThi = c2.IdKyThi and c1.IdPhong = c2.IdPhong)
		

	--INSERT INTO SINHVIEN(MaSV,HoSV,TenSV,NgaySinh,IdLop) SELECT '123', '123', '123','2015/02/06', 93
	--declare @tbl XepPhongType
	--insert into @tbl(IdSV,IdKyThi)  SELECT '2',3
	--insert into @tbl(IdSV,IdKyThi)  SELECT '3',3
	--exec sp_InsertXepPhong @tbl