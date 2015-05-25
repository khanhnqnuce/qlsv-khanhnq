GO
/****** Object:  Table [dbo].[TAIKHOAN]    Script Date: 01/25/2015 22:53:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAIKHOAN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TaiKhoan] [nvarchar](100) NOT NULL,
	[MatKhau] [nvarchar](255) NULL,
	[HoTen] [nvarchar](255) NULL,
	[Quyen] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TaiKhoan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_TaiKhoan] UNIQUE NONCLUSTERED 
(
	[TaiKhoan] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PHONGTHI]    Script Date: 01/25/2015 22:53:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHONGTHI](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenPhong] [nvarchar](255) NOT NULL,
	[SucChua] [int] NOT NULL,
	[GhiChu] [nvarchar](255) NULL,
 CONSTRAINT [PK_PhongThi] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NAMHOC]    Script Date: 01/25/2015 22:53:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NAMHOC](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NamHoc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_NAMHOC] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KHOA]    Script Date: 01/25/2015 22:53:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHOA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenKhoa] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Khoa] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KYTHI]    Script Date: 01/25/2015 22:53:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KYTHI](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MaKT] [nvarchar](255) NOT NULL,
	[TenKT] [nvarchar](255) NULL,
	[NgayThi] [varchar](50) NOT NULL,
	[TGLamBai] [nvarchar](50) NOT NULL,
	[TGBatDau] [nvarchar](255) NOT NULL,
	[TGKetThuc] [nvarchar](255) NULL,
	[GhiChu] [nvarchar](255) NULL,
	[TrangThai] [bit] NULL,
 CONSTRAINT [PK_KyThi] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DAPAN]    Script Date: 01/25/2015 22:53:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DAPAN](
	[IdKyThi] [int] NOT NULL,
	[MaMon] [varchar](50) NOT NULL,
	[MaDe] [varchar](50) NOT NULL,
	[CauHoi] [int] NOT NULL,
	[DapAn] [varchar](5) NOT NULL,
	[ThangDiem] [int] NULL,
 CONSTRAINT [PK_DAPAN] PRIMARY KEY CLUSTERED 
(
	[IdKyThi] ASC,
	[MaMon] ASC,
	[MaDe] ASC,
	[CauHoi] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BAILAM]    Script Date: 01/25/2015 22:53:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BAILAM](
	[IdKyThi] [int] NOT NULL,
	[MaSV] [int] NOT NULL,
	[MaDe] [nvarchar](255) NOT NULL,
	[KetQua] [varchar](255) NOT NULL,
	[DiemThi] [int] NULL,
 CONSTRAINT [PK_BaiLam] PRIMARY KEY CLUSTERED 
(
	[IdKyThi] ASC,
	[MaSV] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KT_PHONG]    Script Date: 01/25/2015 22:53:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KT_PHONG](
	[IdPhong] [int] NOT NULL,
	[IdKyThi] [int] NOT NULL,
	[SiSo] [int] NULL,
 CONSTRAINT [PK_KT_PHONG] PRIMARY KEY CLUSTERED 
(
	[IdPhong] ASC,
	[IdKyThi] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LOP]    Script Date: 01/25/2015 22:53:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MaLop] [nvarchar](255) NOT NULL,
	[IdKhoa] [int] NOT NULL,
 CONSTRAINT [PK_Lop] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_MaLop] UNIQUE NONCLUSTERED 
(
	[MaLop] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SINHVIEN]    Script Date: 01/25/2015 22:53:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SINHVIEN](
	[MaSV] [int] NOT NULL,
	[HoSV] [nvarchar](255) NOT NULL,
	[TenSV] [nvarchar](255) NOT NULL,
	[NgaySinh] [varchar](50) NULL,
	[IdLop] [int] NOT NULL,
 CONSTRAINT [PK_SinhVien] PRIMARY KEY CLUSTERED 
(
	[MaSV] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[XEPPHONG]    Script Date: 01/25/2015 22:53:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XEPPHONG](
	[IdSV] [int] NOT NULL,
	[IdPhong] [int] NULL,
	[IdKyThi] [int] NOT NULL,
 CONSTRAINT [PK_XEPPHONG_1] PRIMARY KEY CLUSTERED 
(
	[IdSV] ASC,
	[IdKyThi] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DIEMTHI]    Script Date: 01/25/2015 22:53:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DIEMTHI](
	[MaSV] [int] NOT NULL,
	[IdNamHoc] [int] NOT NULL,
	[HocKy] [varchar](50) NOT NULL,
	[Diem] [int] NULL,
 CONSTRAINT [PK_DIEMTHI] PRIMARY KEY CLUSTERED 
(
	[MaSV] ASC,
	[IdNamHoc] ASC,
	[HocKy] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_DAPAN_KYTHI]    Script Date: 01/25/2015 22:53:55 ******/
ALTER TABLE [dbo].[DAPAN]  WITH CHECK ADD  CONSTRAINT [FK_DAPAN_KYTHI] FOREIGN KEY([IdKyThi])
REFERENCES [dbo].[KYTHI] ([ID])
GO
ALTER TABLE [dbo].[DAPAN] CHECK CONSTRAINT [FK_DAPAN_KYTHI]
GO
/****** Object:  ForeignKey [FK_BaiLam_KyThi]    Script Date: 01/25/2015 22:53:55 ******/
ALTER TABLE [dbo].[BAILAM]  WITH CHECK ADD  CONSTRAINT [FK_BaiLam_KyThi] FOREIGN KEY([IdKyThi])
REFERENCES [dbo].[KYTHI] ([ID])
GO
ALTER TABLE [dbo].[BAILAM] CHECK CONSTRAINT [FK_BaiLam_KyThi]
GO
/****** Object:  ForeignKey [FK_KT_PHONG_KyThi]    Script Date: 01/25/2015 22:53:55 ******/
ALTER TABLE [dbo].[KT_PHONG]  WITH CHECK ADD  CONSTRAINT [FK_KT_PHONG_KyThi] FOREIGN KEY([IdKyThi])
REFERENCES [dbo].[KYTHI] ([ID])
GO
ALTER TABLE [dbo].[KT_PHONG] CHECK CONSTRAINT [FK_KT_PHONG_KyThi]
GO
/****** Object:  ForeignKey [FK_KT_PHONG_PhongThi]    Script Date: 01/25/2015 22:53:55 ******/
ALTER TABLE [dbo].[KT_PHONG]  WITH CHECK ADD  CONSTRAINT [FK_KT_PHONG_PhongThi] FOREIGN KEY([IdPhong])
REFERENCES [dbo].[PHONGTHI] ([ID])
GO
ALTER TABLE [dbo].[KT_PHONG] CHECK CONSTRAINT [FK_KT_PHONG_PhongThi]
GO
/****** Object:  ForeignKey [FK_Lop_Khoa]    Script Date: 01/25/2015 22:53:55 ******/
ALTER TABLE [dbo].[LOP]  WITH CHECK ADD  CONSTRAINT [FK_Lop_Khoa] FOREIGN KEY([IdKhoa])
REFERENCES [dbo].[KHOA] ([ID])
GO
ALTER TABLE [dbo].[LOP] CHECK CONSTRAINT [FK_Lop_Khoa]
GO
/****** Object:  ForeignKey [FK_SinhVien_Lop]    Script Date: 01/25/2015 22:53:55 ******/
ALTER TABLE [dbo].[SINHVIEN]  WITH CHECK ADD  CONSTRAINT [FK_SinhVien_Lop] FOREIGN KEY([IdLop])
REFERENCES [dbo].[LOP] ([ID])
GO
ALTER TABLE [dbo].[SINHVIEN] CHECK CONSTRAINT [FK_SinhVien_Lop]
GO
/****** Object:  ForeignKey [FK_XepPhong_KyThi]    Script Date: 01/25/2015 22:53:55 ******/
ALTER TABLE [dbo].[XEPPHONG]  WITH CHECK ADD  CONSTRAINT [FK_XepPhong_KyThi] FOREIGN KEY([IdKyThi])
REFERENCES [dbo].[KYTHI] ([ID])
GO
ALTER TABLE [dbo].[XEPPHONG] CHECK CONSTRAINT [FK_XepPhong_KyThi]
GO
/****** Object:  ForeignKey [FK_XepPhong_SinhVien]    Script Date: 01/25/2015 22:53:55 ******/
ALTER TABLE [dbo].[XEPPHONG]  WITH CHECK ADD  CONSTRAINT [FK_XepPhong_SinhVien] FOREIGN KEY([IdSV])
REFERENCES [dbo].[SINHVIEN] ([MaSV])
GO
ALTER TABLE [dbo].[XEPPHONG] CHECK CONSTRAINT [FK_XepPhong_SinhVien]
GO
/****** Object:  ForeignKey [FK_DIEMTHI_NAMHOC]    Script Date: 01/25/2015 22:53:55 ******/
ALTER TABLE [dbo].[DIEMTHI]  WITH CHECK ADD  CONSTRAINT [FK_DIEMTHI_NAMHOC] FOREIGN KEY([IdNamHoc])
REFERENCES [dbo].[NAMHOC] ([ID])
GO
ALTER TABLE [dbo].[DIEMTHI] CHECK CONSTRAINT [FK_DIEMTHI_NAMHOC]
GO
/****** Object:  ForeignKey [FK_ThongKe_SinhVien]    Script Date: 01/25/2015 22:53:55 ******/
ALTER TABLE [dbo].[DIEMTHI]  WITH CHECK ADD  CONSTRAINT [FK_ThongKe_SinhVien] FOREIGN KEY([MaSV])
REFERENCES [dbo].[SINHVIEN] ([MaSV])
GO
ALTER TABLE [dbo].[DIEMTHI] CHECK CONSTRAINT [FK_ThongKe_SinhVien]
GO
