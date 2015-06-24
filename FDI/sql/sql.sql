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
      [TenPhong] [varchar] (10) NULL,
      [SucChua] [int] NULL,
	  [GhiChu] [nvarchar] (255) NULL   
)

GO

CREATE PROCEDURE [dbo].[sp_InsertPhong]
      @tbl SinhVienType READONLY
AS
BEGIN
      INSERT INTO SINHVIEN(MaSV,HoSV,TenSV,NgaySinh,IdLop)
      SELECT MasV, HoSV, TenSV,NgaySinh, IdLop
      FROM (Select c1.MaSV,c1.HoSV,c1.TenSV,c1.NgaySinh,c2.ID as [IdLop] from @tbl c1 join LOP c2 on c1.Lop = c2.MaLop) c3
      WHERE c3.MaSV not in (SELECT MaSV FROM SINHVIEN)
END

	--INSERT INTO SINHVIEN(MaSV,HoSV,TenSV,NgaySinh,IdLop) SELECT '123', '123', '123','2015/02/06', 93
	declare @tbl LopType
	insert into @tbl(MaLop,IdKhoa)  SELECT '59XD10',1
	exec sp_InsertLop @tbl