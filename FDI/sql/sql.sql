CREATE TYPE [dbo].[KhoaType] AS TABLE(
      [ID] [int] NULL,
      [TenKhoa] [nvarchar](MAX) NULL,      
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
      select c1.* from @tbl c1 where NOT EXISTS(select c2.ID from LOP c2 where c1.Lop = c2.MaLop)
END

GO
