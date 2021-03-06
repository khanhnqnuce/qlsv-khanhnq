123
USE [QLSV_TEST]
GO
/****** Object:  Table [dbo].[TAIKHOAN]    Script Date: 01/22/2015 08:55:47 ******/
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
SET IDENTITY_INSERT [dbo].[TAIKHOAN] ON
INSERT [dbo].[TAIKHOAN] ([ID], [TaiKhoan], [MatKhau], [HoTen], [Quyen]) VALUES (5, N'admin', N'e10adc3949ba59abbe56e057f20f883e', N'admin', N'quantri')
INSERT [dbo].[TAIKHOAN] ([ID], [TaiKhoan], [MatKhau], [HoTen], [Quyen]) VALUES (7, N'nd1', N'e10adc3949ba59abbe56e057f20f883e', N'người dùng 1', N'nguoidung')
SET IDENTITY_INSERT [dbo].[TAIKHOAN] OFF
/****** Object:  Table [dbo].[PHONGTHI]    Script Date: 01/22/2015 08:55:47 ******/
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
SET IDENTITY_INSERT [dbo].[PHONGTHI] ON
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (1, N'21h2', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (3, N'22h2', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (4, N'23h2', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (5, N'24h2', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (6, N'25h2', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (7, N'26h2', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (8, N'101h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (9, N'102h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (10, N'103h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (11, N'104h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (12, N'105h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (13, N'106h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (14, N'107h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (15, N'201h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (16, N'202h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (17, N'203h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (18, N'204h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (19, N'205h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (20, N'206h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (21, N'301h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (22, N'302h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (23, N'303h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (24, N'304h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (25, N'305h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (26, N'306h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (27, N'401h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (28, N'402h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (29, N'403h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (30, N'404h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (31, N'405h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (32, N'406h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (33, N'501h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (34, N'502h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (35, N'503h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (36, N'504h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (37, N'505h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (38, N'506h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (39, N'601h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (40, N'602h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (41, N'603h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (42, N'604h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (43, N'605h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (44, N'606h1', 40, N'')
INSERT [dbo].[PHONGTHI] ([ID], [TenPhong], [SucChua], [GhiChu]) VALUES (45, N'607h1', 40, N'')
SET IDENTITY_INSERT [dbo].[PHONGTHI] OFF
/****** Object:  Table [dbo].[NAMHOC]    Script Date: 01/22/2015 08:55:47 ******/
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
SET IDENTITY_INSERT [dbo].[NAMHOC] ON
INSERT [dbo].[NAMHOC] ([ID], [NamHoc]) VALUES (15, N'2014 - 2015')
INSERT [dbo].[NAMHOC] ([ID], [NamHoc]) VALUES (16, N'2015 - 2016')
SET IDENTITY_INSERT [dbo].[NAMHOC] OFF
/****** Object:  Table [dbo].[KHOA]    Script Date: 01/22/2015 08:55:47 ******/
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
SET IDENTITY_INSERT [dbo].[KHOA] ON
INSERT [dbo].[KHOA] ([ID], [TenKhoa]) VALUES (14, N'Cầu đường')
INSERT [dbo].[KHOA] ([ID], [TenKhoa]) VALUES (15, N'Cơ khí xây dựng')
INSERT [dbo].[KHOA] ([ID], [TenKhoa]) VALUES (16, N'Công nghệ thông tin')
INSERT [dbo].[KHOA] ([ID], [TenKhoa]) VALUES (17, N'Công trình thủy')
INSERT [dbo].[KHOA] ([ID], [TenKhoa]) VALUES (18, N'Kiến trúc và quy hoạch')
INSERT [dbo].[KHOA] ([ID], [TenKhoa]) VALUES (19, N'Kinh tế và quản lý xây dựng')
INSERT [dbo].[KHOA] ([ID], [TenKhoa]) VALUES (20, N'Vật liệu xây dựng')
INSERT [dbo].[KHOA] ([ID], [TenKhoa]) VALUES (21, N'Xây dựng dân dụng và công nghiệp')
SET IDENTITY_INSERT [dbo].[KHOA] OFF
/****** Object:  Table [dbo].[KYTHI]    Script Date: 01/22/2015 08:55:47 ******/
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
SET IDENTITY_INSERT [dbo].[KYTHI] ON
INSERT [dbo].[KYTHI] ([ID], [MaKT], [TenKT], [NgayThi], [TGLamBai], [TGBatDau], [TGKetThuc], [GhiChu], [TrangThai]) VALUES (11, N'tak59', N'Tiếng anh đầu vào k59', N'18/01/2015', N'60', N'2h', N'3h', N'Ca 1', 1)
INSERT [dbo].[KYTHI] ([ID], [MaKT], [TenKT], [NgayThi], [TGLamBai], [TGBatDau], [TGKetThuc], [GhiChu], [TrangThai]) VALUES (14, N'tahkIk59', N'Kiểm tra tiếng anh cuối kỳ k59', N'19/01/2015', N'60', N'3h', N'4h', N'Ca 2', 1)
SET IDENTITY_INSERT [dbo].[KYTHI] OFF
/****** Object:  Table [dbo].[DAPAN]    Script Date: 01/22/2015 08:55:47 ******/
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
/****** Object:  Table [dbo].[BAILAM]    Script Date: 01/22/2015 08:55:47 ******/
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
/****** Object:  Table [dbo].[KT_PHONG]    Script Date: 01/22/2015 08:55:47 ******/
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
INSERT [dbo].[KT_PHONG] ([IdPhong], [IdKyThi], [SiSo]) VALUES (1, 11, 0)
/****** Object:  Table [dbo].[LOP]    Script Date: 01/22/2015 08:55:47 ******/
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
SET IDENTITY_INSERT [dbo].[LOP] ON
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (36, N'58BDS', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (37, N'58CB1', 17)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (38, N'58CB2', 17)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (39, N'58CD1', 14)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (40, N'58CD2', 14)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (41, N'58CD3', 14)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (42, N'58CD4', 14)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (43, N'58CD5', 14)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (44, N'58CG1', 15)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (45, N'58CG2', 15)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (46, N'58DT', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (47, N'58HK', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (48, N'58KD1', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (49, N'58KD2', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (50, N'58KD3', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (51, N'58KD4', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (52, N'58KD5', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (53, N'58KD6', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (54, N'58KD7', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (55, N'58KD8', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (56, N'58KG1', 15)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (57, N'58KM1', 15)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (58, N'58KT1', 19)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (59, N'58KT2', 19)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (60, N'58KT3', 19)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (61, N'58KT4', 19)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (62, N'58KT5', 19)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (63, N'58MN1', 20)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (64, N'58MN2', 20)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (65, N'58PM1', 16)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (66, N'58PM2', 16)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (67, N'58PM3', 16)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (68, N'58QD1', 20)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (69, N'58QD2', 20)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (70, N'58QH1', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (71, N'58QH2', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (72, N'58TH1', 16)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (73, N'58TH2', 16)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (74, N'58TL1', 17)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (75, N'58TL2', 17)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (76, N'58TRD', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (77, N'58VL1', 20)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (78, N'58VL2', 20)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (79, N'58XD1', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (80, N'58XD2', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (81, N'58XD3', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (82, N'58XD4', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (83, N'58XD5', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (84, N'58XD6', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (85, N'58XD7', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (86, N'58XD8', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (87, N'58XD9', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (88, N'59CG1', 15)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (89, N'59CG2', 15)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (90, N'59PM1', 16)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (91, N'59PM2', 16)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (92, N'59PM3', 16)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (93, N'59XD1', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (94, N'59XD2', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (95, N'59XD3', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (96, N'59XD4', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (97, N'59XD5', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (98, N'59XD6', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (99, N'59XD7', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (100, N'59XD8', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (101, N'59XD9', 21)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (102, N'59BDS', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (103, N'59DT1', 19)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (104, N'59DT2', 19)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (105, N'59KG1', 15)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (106, N'59KG2', 15)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (107, N'59KT1', 19)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (108, N'59KT2', 19)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (109, N'59KT3', 19)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (110, N'59KT4', 19)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (111, N'59KT5', 19)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (112, N'59KT6', 19)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (113, N'59MN1', 20)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (114, N'59MN2', 20)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (115, N'59QD1', 20)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (116, N'59QD2', 20)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (117, N'59VL1', 20)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (118, N'59VL2', 20)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (119, N'59KD3', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (120, N'59KD4', 18)
INSERT [dbo].[LOP] ([ID], [MaLop], [IdKhoa]) VALUES (121, N'59KD5', 18)
SET IDENTITY_INSERT [dbo].[LOP] OFF
/****** Object:  Table [dbo].[SINHVIEN]    Script Date: 01/22/2015 08:55:47 ******/
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
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1058, N'Trần Phương', N'Anh', N'04/24/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1158, N'Nguyễn Việt', N'Tùng', N'03/01/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1458, N'Phùng Phương', N'Anh', N'08/16/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2358, N'Nguyễn Thu', N'Hoài', N'11/19/95', 63)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3158, N'Nguyễn Trần', N'Duy', N'01/06/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3259, N'Bùi Công', N'Hiếu', N'04/23/96', 116)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3758, N'Đỗ Tiến', N'Nam', N'10/24/95', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4058, N'Nguyễn Văn', N'Bút', N'10/27/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4458, N'Dương Đức', N'Minh', N'09/13/95', 44)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4759, N'Nguyễn Duy', N'Lương', N'08/12/96', 98)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (5458, N'Đặng Thành', N'Đức', N'06/15/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (6558, N'Đoàn Thu', N'Thảo', N'10/22/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (6958, N'Trần Nam', N'Anh', N'10/09/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (7159, N'Trần Hữu Trung', N'Kiên', N'08/05/96', 102)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (7258, N'Nguyễn Minh', N'Nhật', N'08/26/95', 63)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (7458, N'Lê Hồng', N'Trang', N'09/28/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (7658, N'Phùng Tuấn', N'Minh', N'02/05/95', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (10258, N'Lê Hoàng', N'Minh', N'10/26/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (10659, N'Nguyễn Tuấn', N'Minh', N'10/24/96', 116)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (11758, N'Phạm Đức', N'Hoàng', N'06/30/95', 72)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (12658, N'Đỗ Tiến', N'Đạt', N'12/16/95', 87)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (13458, N'Nguyễn Trọng', N'Bách', N'06/07/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (13659, N'Đàm Quang', N'Hiếu', N'05/28/96', 88)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (14159, N'Nguyễn Bình', N'Minh', N'12/07/96', 114)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (14258, N'Lương Mai', N'Trang', N'02/25/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (15759, N'Trần Minh', N'Anh', N'06/14/96', 111)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (16058, N'Võ Nhân', N'Nghĩa', N'08/25/95', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (16158, N'Hoàng Nguyên', N'Trung', N'12/03/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (16459, N'Nguyễn Hoàng', N'Lan', N'11/19/96', 116)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (16659, N'Vũ Đức', N'Kiên', N'08/10/96', 115)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (19258, N'Dương Sỹ', N'Lượng', N'03/26/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (19358, N'Nguyễn Đức', N'Huy', N'06/14/95', 87)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (19659, N'Vũ Huy', N'Anh', N'07/07/96', 110)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (19758, N'Trịnh Hải', N'Yến', N'04/10/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (19858, N'Nguyễn Duy', N'Anh', N'04/26/95', 72)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (19959, N'Trần Thị Ngọc', N'Anh', N'06/04/96', 111)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (21058, N'Phan Quang', N'Anh', N'07/16/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (21259, N'Trần Hồng', N'Quân', N'11/29/96', 97)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (21859, N'Vũ Trung', N'Đức', N'11/10/96', 96)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (22759, N'Nguyễn Hồng', N'Ngọc', N'12/24/96', 116)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (23158, N'Nguyễn Văn', N'Bình', N'05/19/94', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (23259, N'Nguyễn Hoàng', N'Lê', N'05/15/96', 104)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (24058, N'Nguyễn Hữu', N'Thanh', N'10/10/95', 76)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (25158, N'Nguyễn Tuấn', N'Minh', N'05/18/95', 37)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (25658, N'Nguyễn Minh', N'Hiếu', N'06/02/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (26159, N'Phạm Phương', N'Anh', N'04/29/96', 112)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (27058, N'Lê Minh', N'Đức', N'08/10/95', 41)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (28259, N'Trương Sơn', N'Trung', N'03/18/96', 107)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (29958, N'Nguyễn Anh', N'Tuấn', N'02/06/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (30358, N'Vũ Hoàng', N'Hà', N'08/04/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (32158, N'Phạm Quốc', N'Huy', N'07/25/95', 77)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (32958, N'Nguyễn Trung', N'Sơn', N'04/30/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (33058, N'Phạm Thanh', N'Tùng', N'02/07/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (33158, N'Nguyễn Lâm', N'Vũ', N'04/06/95', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (35559, N'Vũ Tiến', N'Thịnh', N'05/01/96', 111)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (37358, N'Nguyễn Mạnh', N'Hùng', N'10/20/95', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (39458, N'Lục Đức', N'Hùng', N'07/06/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (40258, N'Trần Đức', N'Chung', N'01/28/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (43358, N'Đàm Duy', N'Hiếu', N'05/22/95', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (43658, N'Đinh Phương', N'Thảo', N'04/04/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (44058, N'Đàm Văn', N'Cường', N'09/05/95', 72)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (44858, N'Nguyễn Văn', N'Hùng', N'01/14/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (45058, N'Đinh Mạnh', N'Tưởng', N'10/05/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (45158, N'Phạm Chí', N'Thành', N'03/29/95', 83)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (45658, N'Vũ Thị Kim', N'Tươi', N'07/28/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (47758, N'Bùi Quang', N'Huỳnh', N'07/01/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (48058, N'Nguyễn Tuấn', N'Mạnh', N'10/19/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (48158, N'Nguyễn Văn', N'Anh', N'07/26/95', 77)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (50559, N'Phạm Tiến', N'Thịnh', N'09/17/96', 103)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (51658, N'Phan Thị Trúc', N'Quỳnh', N'02/12/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (51758, N'Đặng Viết', N'Long', N'09/26/95', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (54658, N'Nguyễn Đắc', N'Việt', N'01/28/95', 81)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (55358, N'Nguyễn Xuân', N'Hùng', N'07/29/95', 87)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (56358, N'Hứa Kiều', N'Linh', N'12/09/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (58658, N'Phùng Công', N'Dũng', N'07/31/95', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (60959, N'Nguyễn Thiên Hưng', N'Thịnh', N'08/03/96', 98)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (61158, N'Hà Quốc', N'Việt', N'09/06/95', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (61358, N'Nguyễn Văn', N'Quân', N'03/14/95', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (62158, N'Tăng Quốc', N'Việt', N'08/12/95', 85)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (62558, N'Lê Thành', N'Công', N'08/19/95', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (63458, N'Đỗ Đình', N'Mạnh', N'07/15/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (65258, N'Nguyễn Văn', N'Quyết', N'04/08/95', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (71058, N'Đinh Văn', N'Lộc', N'08/30/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (76958, N'Nguyễn Đăng', N'Quang', N'07/30/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (77958, N'Nguyễn Chí', N'Anh', N'07/06/94', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (79358, N'Ngô Trường', N'Giang', N'10/26/95', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (80258, N'Nguyễn Thị', N'Hiền', N'12/09/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (82558, N'Nguyễn Hữu', N'Đức', N'05/09/95', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (82758, N'Bùi Đức', N'Thịnh', N'06/01/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (84658, N'Đỗ Thị Hồng', N'Nhung', N'05/19/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (88358, N'Phạm Anh', N'Vũ', N'09/06/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (88858, N'Nguyễn Tiến', N'Phong', N'11/08/95', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (90158, N'Trần Huy', N'Anh', N'12/21/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (90259, N'Nguyễn Văn', N'Dũng', N'05/22/96', 111)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (91958, N'Lê Thị Vân', N'Anh', N'07/24/95', 63)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (94858, N'Lê Trung', N'Đạt', N'10/29/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (95158, N'Lê Quang', N'Thắng', N'07/04/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (96058, N'Nguyễn Thị', N'Len', N'10/11/95', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (96158, N'Nguyễn Xuân', N'Quỳnh', N'10/28/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (96658, N'Đặng Văn', N'Sơn', N'08/01/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (98358, N'Hoàng Trung', N'Hiến', N'10/06/95', 39)
GO
print 'Processed 100 total records'
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (100958, N'Trịnh Hoài', N'Nam', N'01/12/95', 85)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (101058, N'Phạm Tùng', N'Lâm', N'02/26/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (101158, N'Trịnh Việt', N'Hùng', N'11/19/94', 81)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (103358, N'Nguyễn Thái', N'Sơn', N'12/30/95', 86)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (103658, N'Phan Huy', N'Minh', N'04/11/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (107458, N'Vũ Đức', N'Nguyên', N'12/14/95', 87)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (108058, N'Đỗ Thị', N'Hòa', N'02/18/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (108258, N'Đỗ Văn', N'Tư', N'01/28/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (108558, N'Trần Việt', N'Anh', N'10/20/94', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (108858, N'Đặng Thị Hồng', N'Giang', N'01/14/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (109358, N'Vũ Đức', N'Bản', N'10/21/95', 77)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (111758, N'Mai Văn', N'Tôn', N'03/23/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (111958, N'Nguyễn Vũ Mai', N'Hương', N'06/28/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (112059, N'Tăng Lê Xuân', N'Sơn', N'08/22/96', 114)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (114058, N'Trần Nhật', N'Anh', N'10/12/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (114659, N'Nguyễn Vương', N'Nguyên', N'01/28/96', 113)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (119158, N'Tạ Xuân', N'Tùng', N'12/02/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (120458, N'Nguyễn Linh', N'Chi', N'02/01/95', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (121358, N'Đỗ Tuấn', N'Anh', N'01/22/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (121758, N'Phan Thanh', N'Hưng', N'11/28/95', 77)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (123258, N'Nguyễn Tiến', N'Hoàn', N'10/25/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (124058, N'Lê Văn', N'Thi', N'04/13/95', 83)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (125958, N'Mai Trọng', N'Hoàng', N'09/06/95', 41)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (126058, N'Hoàng Thị', N'Trang', N'03/15/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (126458, N'Bùi Trọng', N'Lân', N'03/02/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (129558, N'Trần Công', N'Minh', N'08/04/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (131758, N'Lương Triệu Quỳnh', N'Trang', N'05/22/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (135058, N'Trần Thị', N'Hằng', N'10/18/95', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (136658, N'Nguyễn Anh', N'Tuấn', N'06/20/94', 63)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (136858, N'Hoàng Bảo', N'Long', N'10/25/95', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (137058, N'Nguyễn Tùng', N'Long', N'05/13/95', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (137258, N'Nguyễn Xuân', N'Trường', N'12/18/95', 87)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (138059, N'Lê Quang', N'Hợp', N'12/29/96', 105)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (138858, N'Nguyễn Trung', N'Kiên', N'09/19/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (140758, N'Lê Thanh', N'Tú', N'08/11/95', 81)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (142358, N'Trịnh Xuân', N'Sáng', N'06/21/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (142458, N'Nguyễn Văn', N'Nguyên', N'05/08/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (142658, N'Nguyễn Đức', N'Thành', N'03/02/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (143058, N'Lường Văn', N'Công', N'01/19/95', 81)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (143158, N'Đồng Huy', N'Vũ', N'02/21/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (143758, N'Lê Tiến', N'Anh', N'09/05/95', 72)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (143959, N'Lê Anh', N'Quân', N'01/17/96', 98)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (144758, N'Cầm Lan', N'Chi', N'07/19/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (147658, N'Đàm Hữu', N'Huy', N'02/11/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (149158, N'Nguyễn Tuấn', N'Hoàng', N'11/14/94', 79)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (151158, N'Đào Mạnh', N'Tuấn', N'09/26/94', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (151758, N'Dương Thị', N'Phi', N'09/10/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (155758, N'Hoàng Văn', N'Thích', N'07/26/94', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (157758, N'Nguyễn Thị Hiền', N'Lương', N'10/28/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (158758, N'Nguyễn Thị Phương', N'Thảo', N'10/31/95', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (159658, N'Đinh Trung', N'Dũng', N'05/24/95', 77)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (161058, N'Nguyễn Đức', N'Quang', N'05/28/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (164158, N'Nguyễn Thị Minh', N'Hòa', N'08/24/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (164458, N'Nguyễn Tuấn', N'Anh', N'01/15/94', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (169358, N'Nguyễn Đình', N'Trọng', N'10/19/94', 81)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (171458, N'Nguyễn Tùng', N'Linh', N'10/20/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (171858, N'Nguyễn Ngọc', N'Anh', N'05/21/95', 87)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (172058, N'Lê Hoàng', N'Anh', N'09/09/95', 86)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (174858, N'Trần Đăng', N'Lượng', N'06/05/95', 41)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (175758, N'Nguyễn Văn', N'Cường', N'09/08/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (176358, N'Lê Tùng', N'Dương', N'04/18/95', 74)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (176458, N'Nguyễn Anh', N'Phương', N'01/10/95', 83)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (176658, N'Lê Bá', N'Thái', N'05/07/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (182158, N'Trần Duy', N'Hưng', N'11/17/95', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (182458, N'Phạm Hoàng', N'Vũ', N'09/01/95', 56)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (182459, N'Đặng Kim', N'Chi', N'11/11/96', 109)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (183558, N'Bùi Đức', N'Dương', N'09/26/95', 43)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (183658, N'Đồng Thị Thanh', N'Hoài', N'04/16/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (184858, N'Đoàn', N'Quyết', N'04/27/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (184958, N'Chu Thị', N'Phương', N'10/11/95', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (187358, N'Nguyễn Thị Mỹ', N'Duyên', N'06/08/95', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (187458, N'Nguyễn Thái', N'Hà', N'09/09/95', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (189058, N'Hoàng Mạnh', N'Hùng', N'02/20/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (190158, N'Hoàng Ngọc', N'Tuyền', N'06/10/95', 83)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (190958, N'Lê Đại Hồng', N'Hoàng', N'11/11/95', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (191158, N'Phan Trung', N'Kiên', N'01/25/95', 85)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (191258, N'Nguyễn Thúy', N'An', N'09/23/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (191358, N'Lê Tiến', N'Mạnh', N'12/24/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (194558, N'Nịnh Thị', N'Đức', N'02/20/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (197858, N'Đoàn Trung', N'Hiếu', N'04/15/95', 79)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (198058, N'Nguyễn Trí', N'Dũng', N'07/28/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (198658, N'Nguyễn Tuấn', N'Tiến', N'06/23/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (198958, N'Lê Đức', N'Việt', N'02/18/95', 79)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (199658, N'Nguyễn Tuấn', N'Hòa', N'05/14/94', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (199959, N'Phạm Thị Hà', N'Trang', N'07/28/96', 112)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (200858, N'Dương Văn', N'Huỳnh', N'11/04/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (201158, N'Nguyễn Hữu', N'Tùng', N'04/17/95', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (204958, N'Chu Quang', N'Duy', N'11/11/95', 83)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (205358, N'Trần Thị Ngân', N'Thanh', N'06/14/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (205958, N'Bùi Quang', N'Đại', N'02/14/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (207158, N'Trần Thị Hải', N'Hà', N'08/03/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (208158, N'Nguyễn Thị Thùy', N'Dung', N'08/15/95', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (210458, N'Phạm Đức', N'Duy', N'06/22/94', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (211958, N'Nguyễn Thị', N'Nhiên', N'05/24/95', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (213658, N'Phan Đình', N'Phát', N'06/02/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (213758, N'Hoàng Kim', N'Hưng', N'07/27/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (214158, N'Nguyễn Hữu', N'Minh', N'02/06/95', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (216358, N'Nguyễn Thanh', N'Huyền', N'09/10/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (217158, N'Nguyễn Văn', N'Lợi', N'01/05/95', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (217558, N'Nguyễn Đình', N'Khánh', N'02/05/95', 79)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (217658, N'Nguyễn Văn', N'Dân', N'08/26/95', 81)
GO
print 'Processed 200 total records'
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (217858, N'Nguyễn Thanh', N'Xuân', N'06/28/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (217958, N'Trần Thị', N'Duyên', N'12/05/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (220858, N'Nguyễn Trọng', N'Lưu', N'03/30/95', 86)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (221058, N'Nguyễn Đình', N'Trường', N'02/21/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (226358, N'Vương Văn', N'Thuận', N'06/20/94', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (227659, N'Nguyễn Thị Phương', N'Thảo', N'02/01/96', 108)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (229858, N'Nguyễn Anh', N'Tú', N'01/11/95', 86)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (229958, N'Đặng Thế', N'Anh', N'02/23/95', 85)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (230358, N'Nguyễn Văn', N'Dương', N'12/16/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (230858, N'Phạm Thanh', N'Sơn', N'09/07/95', 37)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (233359, N'Nguyễn Thị', N'Hà', N'03/17/96', 112)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (235658, N'Nguyễn Văn', N'Quang', N'11/06/95', 87)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (237758, N'Ngô Bá', N'Công', N'04/27/95', 77)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (237958, N'Phạm Hồng', N'Anh', N'09/02/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (238758, N'Phạm Thế', N'Tiệp', N'09/26/95', 77)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (238958, N'Dương Văn', N'Khánh', N'01/31/95', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (240558, N'Nguyễn Văn', N'Tuấn', N'02/20/95', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (240559, N'Nguyễn Quang', N'Thịnh', N'08/09/96', 103)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (241758, N'Nguyễn Trọng', N'Đại', N'10/05/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (245859, N'Nguyễn Đức', N'Thắng', N'04/11/96', 117)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (246758, N'Đoàn Văn', N'Dũng', N'03/01/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (248258, N'Nguyễn Đình', N'Quyết', N'02/18/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (255058, N'Nguyễn Quang', N'Khải', N'05/02/95', 79)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (256858, N'Nguyễn Mạnh', N'Cường', N'09/13/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (259258, N'Lê Hữu', N'Đức', N'08/31/95', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (260258, N'Nguyễn Đức', N'Tường', N'08/21/95', 86)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (265258, N'Đỗ Thị Thu', N'Huyền', N'09/03/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (265558, N'Đàm Quang', N'Thế', N'06/25/94', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (270658, N'Nguyễn Tiến', N'Đạt', N'11/04/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (270758, N'Nguyễn Trường', N'Giang', N'10/24/95', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (270958, N'Đào Văn', N'Hải', N'03/23/94', 79)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (272658, N'Dương Văn', N'Đình', N'10/15/94', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (274058, N'Vũ Ngọc', N'Linh', N'04/10/95', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (274558, N'Hoàng Thu', N'Hiền', N'03/19/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (278458, N'Nguyễn Thị', N'Ngọc', N'12/30/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (285158, N'Nguyễn Văn', N'Luận', N'04/01/94', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (286758, N'Ngô Quang', N'Tuân', N'11/10/95', 56)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (287658, N'Ngô Tấn', N'Nghĩa', N'12/25/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (288458, N'Nguyễn Văn', N'Ninh', N'07/20/95', 74)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (292458, N'Dương Chí', N'Cường', N'10/26/95', 85)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (292558, N'Đỗ Duy', N'Khánh', N'11/29/95', 74)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (293258, N'Phạm Thị', N'Thu', N'10/10/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (294758, N'Đỗ Ngọc', N'Tú', N'11/10/95', 79)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (295858, N'Nguyễn Văn', N'Trịnh', N'11/10/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (296058, N'Nguyễn Thanh', N'Hiển', N'12/13/95', 81)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (298358, N'Nguyễn Minh', N'Hiếu', N'06/17/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (298658, N'Trần Thị Hà', N'Phương', N'08/14/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (299458, N'Đặng Việt', N'Hùng', N'09/21/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (299858, N'Trần Ngọc', N'Hà', N'08/15/95', 37)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (301858, N'Trần Thị', N'Nga', N'05/14/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (304958, N'Vũ Ngọc', N'ánh', N'11/09/95', 87)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (305058, N'Phạm Phương', N'Linh', N'10/26/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (305258, N'Trịnh Quang', N'Thanh', N'02/16/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (305858, N'Đỗ Hải', N'Long', N'12/11/95', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (306158, N'Nguyễn Hà', N'Anh', N'06/26/95', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (306458, N'Lê Huyền', N'Trang', N'10/25/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (306658, N'Phạm Huy', N'Hoàng', N'08/30/95', 39)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (307758, N'Vũ Lê', N'Thành', N'01/22/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (308358, N'Vũ Thị Thanh', N'Hoa', N'10/28/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (308458, N'Bùi Thị Kim', N'Liên', N'12/06/95', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (308658, N'Nguyễn Mạnh', N'Cường', N'11/18/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (309258, N'Lưu Mạnh', N'Tuấn', N'07/10/95', 87)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (309458, N'Nguyễn Sơn', N'Tùng', N'03/31/95', 79)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (310558, N'Vũ Tuyên', N'Hoàng', N'02/15/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (311658, N'Nguyễn Xuân', N'Đạo', N'09/29/95', 43)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (312958, N'Nguyễn Quang', N'Huy', N'12/07/94', 63)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (313258, N'Đỗ Văn', N'Trân', N'06/16/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (313358, N'Nguyễn Kim', N'Chi', N'08/25/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (314259, N'Trương Văn', N'Dũng', N'07/13/96', 118)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (314458, N'Đỗ Trung', N'Đức', N'11/28/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (314658, N'Bùi Đức', N'Lưu', N'04/03/95', 40)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (314958, N'Phạm Văn', N'Hà', N'12/20/95', 41)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (315458, N'Vũ Thị', N'Duyên', N'10/24/95', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (316958, N'Trần Quốc', N'Vương', N'09/04/95', 85)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (317758, N'Phạm Quang', N'Vũ', N'05/15/95', 41)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (318258, N'Hoàng Văn', N'Hiếu', N'10/16/95', 41)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (318558, N'Doãn Vũ', N'Thư', N'12/02/95', 39)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (319358, N'Trịnh Xuân', N'Huy', N'02/16/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (320958, N'Trần Văn', N'Trung', N'02/01/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (323858, N'Nguyễn Xuân', N'Toán', N'03/27/95', 39)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (323958, N'Nguyễn Thị', N'Thêm', N'05/05/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (324958, N'Nguyễn Văn', N'Tam', N'06/23/95', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (325358, N'Phạm Văn', N'Trọng', N'01/16/95', 83)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (326958, N'Đỗ Hoàng', N'Anh', N'10/21/95', 42)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (327258, N'Phan Thị Thu', N'Huyền', N'11/28/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (328058, N'Phạm Văn', N'Chiến', N'03/03/95', 85)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (329258, N'Vũ Thị', N'Huế', N'03/15/94', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (330258, N'Nguyễn Hồng', N'Sơn', N'03/31/95', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (330458, N'Phạm Việt', N'Hùng', N'08/15/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (330658, N'Trần Đình', N'Chiều', N'02/28/95', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (330758, N'Trần Thị', N'Hiên', N'03/21/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (331359, N'Lê Thị Nhật', N'Lệ', N'11/07/96', 111)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (331459, N'Mai Trung', N'Hiếu', N'01/17/96', 102)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (331758, N'Đào Huy', N'Doanh', N'10/12/95', 40)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (332058, N'Ngô Duy', N'Thành', N'11/22/95', 44)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (332458, N'Nguyễn Văn', N'Đô', N'12/03/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (334458, N'Đoàn Tiến', N'Triển', N'09/25/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (334858, N'Nguyễn Thị', N'Duyên', N'06/06/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (337758, N'Trần Trung', N'Hiếu', N'06/12/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (338058, N'Nguyễn Thị Bích', N'Ngọc', N'08/23/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (340258, N'Lã Quyết', N'Thắng', N'07/21/95', 74)
GO
print 'Processed 300 total records'
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (342558, N'Đỗ Xuân', N'Hiểu', N'12/14/95', 42)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (342958, N'Lưu Quang', N'Luân', N'05/20/95', 86)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (343559, N'Vũ Thị Ngọc', N'Trâm', N'10/29/96', 111)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (343858, N'Nguyễn Văn', N'Thành', N'06/10/95', 85)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (344058, N'Nguyễn Thị Việt', N'Chinh', N'02/26/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (345858, N'Hoàng Văn', N'Lộc', N'01/13/95', 77)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (346658, N'Vũ Thị', N'Xuân', N'03/29/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (347358, N'Vũ Văn', N'Đức', N'10/14/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (350058, N'Lâm Ngọc', N'Quyền', N'10/11/95', 83)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (350858, N'Trần Tuấn', N'Anh', N'09/23/95', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (350958, N'Phạm Văn', N'Tới', N'09/26/95', 44)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (353458, N'Nguyễn Văn', N'Minh', N'10/21/95', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (353658, N'Đào Văn', N'Thức', N'08/20/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (354758, N'Phạm Bích', N'Diệp', N'08/02/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (357958, N'Bùi Văn', N'Nam', N'02/06/94', 83)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (358358, N'Nguyễn Thanh', N'Giang', N'10/01/95', 37)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (360058, N'Đỗ Văn', N'Giang', N'05/09/94', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (360358, N'Trương Văn', N'Tài', N'03/11/92', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (363558, N'Nguyễn Quang', N'Tuấn', N'04/22/95', 83)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (364558, N'Đoàn Thị', N'Hiền', N'12/24/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (365758, N'Nguyễn Xuân', N'Trường', N'07/02/95', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (366558, N'Bùi Gia', N'Thành', N'02/28/95', 87)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (366958, N'Nguyễn Khoa Hải', N'Đường', N'01/04/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (367058, N'Hoàng Thành', N'Lộc', N'03/06/95', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (367558, N'Nguyễn Trường', N'Phi', N'05/15/95', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (368458, N'Đỗ Quang', N'Đại', N'06/26/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (371858, N'Lương Thị Khánh', N'Huyền', N'02/28/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (374858, N'Đinh Văn', N'Cảnh', N'01/16/95', 79)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (374958, N'Nguyễn Quang', N'Phương', N'03/02/95', 41)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (376858, N'Lê Thị', N'Hà', N'03/06/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (377259, N'Lại Ngọc', N'Mai', N'08/08/96', 113)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (377358, N'Nguyễn Văn', N'Chiến', N'06/11/95', 86)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (378058, N'Hoàng Thị', N'Phương', N'01/10/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (380358, N'Đặng Quang', N'Dương', N'08/19/95', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (380758, N'Nguyễn Hữu', N'Hào', N'01/20/95', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (382658, N'Lê Văn', N'Hùng', N'02/08/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (383158, N'Nguyễn Thị Thu', N'Thương', N'09/22/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (383159, N'Trần Văn', N'Huy', N'08/20/96', 90)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (385558, N'Hoàng Văn', N'Hưng', N'04/21/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (385858, N'Phạm Tiến', N'Long', N'10/17/95', 81)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (385958, N'Phạm Quang', N'Trung', N'05/12/95', 85)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (386258, N'Hoàng Minh', N'Giang', N'02/23/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (386758, N'Hà Thị', N'Thơm', N'08/01/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (391958, N'Đỗ Duy', N'Kiêm', N'06/09/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (396258, N'Phạm Văn', N'Đức', N'12/31/95', 86)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (397358, N'Nguyễn Thị', N'Dung', N'07/12/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (401959, N'Nguyễn Danh', N'Sang', N'02/19/96', 115)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (404458, N'Bùi Danh', N'Thông', N'02/01/95', 79)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (406158, N'Vũ Đình', N'An', N'07/14/95', 87)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (407358, N'Nguyễn Thanh', N'Mai', N'07/30/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (409758, N'Nguyễn Tùng', N'Diệp', N'04/22/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (410158, N'Nguyễn Việt', N'Quốc', N'08/14/95', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (413958, N'Dương Văn', N'Dũng', N'02/07/92', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (415058, N'Trần Việt', N'An', N'11/20/95', 43)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (415158, N'Đàm Ngọc', N'Du', N'10/23/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (415458, N'Đỗ Thị', N'Loan', N'01/09/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (421558, N'Đặng Ngọc', N'Thắng', N'10/30/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (421758, N'Nguyễn Thế', N'Cường', N'10/24/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (421858, N'Hà Huy', N'Đạt', N'06/19/95', 86)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (425758, N'Vũ Văn', N'Miền', N'02/28/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (425858, N'Trần Huy', N'Hoàn', N'09/05/95', 83)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (426358, N'Mai Thị', N'Nhâm', N'05/16/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (426658, N'Phạm Văn', N'Ngọc', N'05/26/95', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (427058, N'Trần Thanh', N'Long', N'08/07/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (429858, N'Đỗ Thị Thùy', N'Dung', N'05/01/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (430359, N'Nguyễn Thanh', N'Tùng', N'02/13/96', 111)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (434458, N'Hà Văn', N'Dương', N'10/05/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (434958, N'Nguyễn Xuân', N'Trường', N'11/24/94', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (441658, N'Doãn Đình', N'Chung', N'09/27/94', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (448558, N'Đoàn Đình', N'Quân', N'01/05/90', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (452058, N'Mai Hùng', N'Sơn', N'10/29/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (452758, N'Lê Hoàng', N'Phương', N'07/10/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (455258, N'Trần Lê Anh', N'Phương', N'07/05/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (455358, N'Nguyễn Thị', N'Hương', N'10/25/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (456159, N'Nguyễn Thị Hồng', N'Vân', N'06/02/96', 111)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (456658, N'Lê Trường', N'Lộc', N'09/20/95', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (458258, N'Nguyễn Lê', N'Hương', N'09/14/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (458358, N'Phạm Văn', N'Huy', N'01/23/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (468358, N'Nguyễn Văn', N'Mạnh', N'02/10/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (472458, N'Nguyễn Văn', N'Quang', N'12/01/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (473658, N'Lê Thị', N'Hà', N'11/23/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (474658, N'Đặng Thị Mỹ', N'Linh', N'01/11/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (474758, N'Trần Minh', N'Thùy', N'03/06/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (478058, N'Phùng Ngọc', N'Ân', N'02/06/95', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (482959, N'Vũ Văn', N'Sự', N'03/06/96', 97)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (487658, N'Bùi Hồng', N'Nam', N'02/01/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (496058, N'Hồ Thị Hương', N'Giang', N'12/06/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (501858, N'Nguyễn Thị Quỳnh', N'Anh', N'02/20/94', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (502458, N'Huỳnh Hữu', N'Thiện', N'12/13/94', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (502958, N'Nguyễn Bá', N'Thành', N'11/20/95', 44)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (505558, N'Trần Ngọc', N'Hiếu', N'08/20/95', 44)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (508358, N'Phan Văn', N'Quân', N'08/01/95', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (510958, N'Đào Văn', N'Hiếu', N'11/02/95', 81)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (515658, N'Hoàng Thị', N'Thùy', N'03/27/95', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (517058, N'Văn Huy', N'Sỹ', N'08/25/95', 85)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (519058, N'Nguyễn Hữu', N'Biểu', N'06/15/95', 85)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (520158, N'Nguyễn Đình', N'Hưng', N'12/25/95', 83)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (520558, N'Phan Thị', N'Dung', N'01/10/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (520658, N'Nguyễn Nam', N'Khánh', N'12/09/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (525458, N'Vũ Văn', N'Dũng', N'08/16/95', 85)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (527958, N'Trần Văn', N'Quân', N'09/29/95', 59)
GO
print 'Processed 400 total records'
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (528158, N'Trần Trung', N'Dũng', N'02/16/95', 86)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (528358, N'Thái Gia', N'Lực', N'08/12/95', 77)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (531458, N'Nguyễn Hữu', N'Sơn', N'03/21/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (532058, N'Lê Thị', N'Trang', N'07/13/94', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (532858, N'Nguyễn Văn', N'Tiến', N'06/06/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (538158, N'Nguyễn Bá', N'Cấm', N'10/01/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (541758, N'Nguyễn Tiến', N'Dũng', N'09/24/95', 59)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (542258, N'Nguyễn Xuân', N'Hòa', N'03/17/94', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (543258, N'Phan Văn', N'Bằng', N'05/08/95', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (545258, N'Võ Huy', N'Lộc', N'08/23/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (545858, N'Nguyễn Trọng', N'Cầm', N'03/21/95', 85)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (546558, N'Nguyễn Công', N'Hải', N'08/23/95', 77)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (550558, N'Võ Thị Thùy', N'Dương', N'04/02/94', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (553258, N'Trần Chí', N'Cường', N'08/02/95', 86)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (554958, N'Trương Văn', N'Thắng', N'12/20/95', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (555758, N'Nguyễn Anh', N'Dũng', N'06/20/95', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (556858, N'Trần Minh', N'Tuấn', N'12/10/95', 44)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (557358, N'Nguyễn Văn', N'Duẫn', N'02/04/95', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (565859, N'Đậu Thị Nguyệt', N'Anh', N'09/21/96', 109)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (567358, N'Nguyễn Văn', N'Ninh', N'11/28/95', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (571258, N'Nguyễn Công', N'Thành', N'09/20/94', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (572858, N'Nguyễn Văn', N'Đạt', N'05/26/91', 81)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (573158, N'Đặng Tuấn', N'Anh', N'06/08/91', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (574958, N'Trần Thị', N'Quyên', N'05/09/94', 77)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (575058, N'Cao Thanh Thuỳ', N'My', N'25/09/94', 69)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (587058, N'Phạm Đức', N'Cường', N'03/23/91', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (588758, N'Ngô Tiến', N'Thành', N'10/08/94', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (589058, N'Phan Thị', N'Huyền', N'03/26/94', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (589458, N'Nguyễn Trường', N'An', N'03/06/1993', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (601358, N'Tống Thị', N'Dung', N'02/28/94', 44)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (602158, N'Hoàng Văn', N'Sứng', N'04/09/94', 37)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (603859, N'Nguyễn Linh', N'Ngọc', N'02/10/96', 112)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (606158, N'Chu Anh', N'Cường', N'10/12/94', 47)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (606958, N'Nguyễn Đăng', N'Quỳnh', N'11/14/94', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (612458, N'Nguyễn Toàn ', N'Trung', N'14/11/94', 44)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (613958, N'Nguyễn Đức', N'Hải', N'01/08/94', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (619658, N'Hà Thị Việt', N'Trinh', N'09/20/93', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (620358, N'Trần Tuấn', N'Anh', N'08/03/93', 72)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (624058, N'Nguyễn Thu', N'Thế', N'12/10/94', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (627058, N'Vũ Quốc', N'Phòng', N'01/02/94', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (629059, N'Nguyễn Thị', N'Hà', N'11/13/95', 108)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (636858, N'Nguyễn Văn', N'Đạt', N'12/07/94', 85)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (642958, N'Đinh Trọng', N'Bách', N'10/31/94', 68)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (651058, N'Tống Khánh', N'Hợp', N'04/12/94', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (652158, N'Lý Phương', N'Bá', N'01/11/94', 82)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (652258, N'Nguyễn Anh', N'Minh', N'04/02/93', 87)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (652558, N'Đặng Nông Hải', N'Toàn', N'05/22/94', 79)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (653658, N'Lê Mạnh', N'Cường', N'08/09/94', 80)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (654158, N'Nguyễn Xuân', N'Anh', N'09/18/94', 81)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (654358, N'Cao Thị Ngọc', N'Trâm', N'12/16/94', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (654758, N'Dương Tuấn', N'Anh', N'04/21/94', 79)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (656558, N'Hoàng Thị Quỳnh', N'Mai', N'01/19/94', 58)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (658558, N'John', N'Srey Po', N'06/03/93', 72)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (658658, N'Pen', N'Sopha', N'11/23/93', 72)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (659058, N'Tanhthouangse', N'Phetsav', N'10/07/93', 87)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (659358, N'Nguyễn Trần', N'Tiến', N'02/14/95', 79)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (709159, N'Thân Hoài', N'Chung', N'  /  /', 107)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (710759, N'Nguyễn Sơn', N'Lâm', N'  /  /', 111)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1501058, N'Bùi Trung', N'Hiếu', N'12/14/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1503958, N'Nguyễn Nhật', N'Ninh', N'03/26/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1504559, N'Hà Bảo', N'Khánh', N'10/17/96', 90)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1504658, N'Nguyễn Thị Ngọc Ho', N'Phương', N'12/31/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1504858, N'Trần Anh', N'Tuấn', N'03/27/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1505058, N'Vũ', N'Hoàng', N'05/06/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1505358, N'Phạm Ngân', N'Hà', N'12/08/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1508658, N'Trần Minh', N'Quân', N'07/02/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1510558, N'Trần Sơn', N'Tùng', N'03/25/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1511858, N'Lê Đức', N'Lợi', N'11/02/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1512858, N'Thiều Đình', N'Anh', N'10/24/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1516158, N'Hoàng Quý', N'Cường', N'04/05/91', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1517158, N'Nguyễn Thị', N'Tươi', N'04/10/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1518258, N'Đặng Ngọc Bình', N'An', N'09/04/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1519858, N'Hoàng Quốc', N'Toàn', N'01/24/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1520058, N'Lã Ngọc', N'Hiếu', N'09/14/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1520358, N'Nguyễn Trọng Hoàng', N'Long', N'09/06/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1522258, N'Nguyễn Văn', N'Bắc', N'08/26/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1524258, N'Ngô Anh', N'Đức', N'11/03/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1525058, N'Nguyễn Thị', N'Nga', N'12/02/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1527258, N'Mai Thị Lan', N'Anh', N'06/08/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1528058, N'Nguyễn Thị Thu', N'Hà', N'02/05/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1528059, N'Hoàng Văn', N'Đại', N'07/22/96', 90)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1528758, N'Nguyễn Huy', N'Hùng', N'06/22/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1528958, N'Phạm Thị Thu', N'Thùy', N'05/22/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1529758, N'Nguyễn Thị', N'Trang', N'02/10/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1533358, N'Nguyễn Văn', N'Quang', N'02/06/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1534358, N'Ninh Đức', N'Nguyên', N'10/19/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1536158, N'Trịnh Minh', N'Chiến', N'12/12/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1536259, N'Bùi Thị', N'Hảo', N'07/15/96', 92)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1536458, N'Trần Văn', N'Bảo', N'09/03/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1536558, N'Lưu Công', N'Đông', N'02/24/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1536858, N'Nguyễn Thị', N'Thanh', N'05/07/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1540358, N'Đoàn Văn', N'An', N'12/12/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1540559, N'Bùi Quang', N'Đức', N'06/26/96', 90)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1541158, N'Nguyễn Ngọc', N'Trìu', N'03/28/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1541858, N'Hà Quang', N'Đại', N'10/24/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1542858, N'Đỗ Thùy', N'Dung', N'04/03/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1543258, N'Đào Thị', N'Thu', N'09/14/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1543758, N'Phạm Ngọc', N'Kha', N'03/05/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1544058, N'Khiếu Thị', N'Xuân', N'08/16/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1544458, N'Vũ Văn', N'Hoàn', N'05/11/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1547358, N'Phạm Thị Thu', N'Hiền', N'02/10/95', 66)
GO
print 'Processed 500 total records'
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1550558, N'Trần Gia', N'Hiệp', N'06/15/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1551058, N'Phan Thị Hương', N'Trà', N'05/21/95', 65)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1551559, N'Nguyễn Văn', N'Đại', N'04/04/96', 90)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1552158, N'Hoàng Công', N'Sơn', N'10/23/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1553658, N'Lê Thị', N'Ngọc', N'04/20/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1554258, N'Trần Văn', N'Quyết', N'07/09/95', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1556958, N'Nguyễn Thùy', N'Linh', N'04/03/94', 66)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (1561759, N'Nguyễn Tiến', N'Đạt', N'11/06/96', 90)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2000758, N'Nguyễn Việt', N'Anh', N'12/03/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2000858, N'Chu Ngọc', N'Thành', N'04/20/94', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2001158, N'Nguyễn Hồng', N'Quân', N'11/12/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2001358, N'Nguyễn Đăng', N'Chiến', N'07/17/95', 51)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2001558, N'Tô Bảo', N'Long', N'01/04/96', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2001559, N'Nguyễn Ngọc', N'Quỳnh', N'11/06/96', 121)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2002058, N'Phạm Gia', N'Thịnh', N'12/14/95', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2002259, N'Trần Hoàng', N'My', N'21/12/96', 121)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2002958, N'Đặng Lê', N'Hoàng', N'07/30/95', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2003858, N'Vũ Duy', N'Anh', N'05/31/95', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2004459, N'Tiến Công ', N'Minh', N'10/04/96', 120)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2005158, N'Nguyễn Bích', N'Thủy', N'11/23/94', 55)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2006158, N'Nguyễn', N'Công', N'06/04/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2007658, N'Lại Vĩ', N'Đại', N'03/15/95', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2007758, N'Bùi Anh', N'Tú', N'08/27/94', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2007858, N'Trần Phương', N'Linh', N'02/17/94', 51)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2008758, N'Hoàng Xuân', N'Phương', N'06/04/95', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2009158, N'Đái Xuân', N'Tùng', N'07/05/95', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2010158, N'Trần Ngọc', N'Long', N'09/28/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2011258, N'Hoàng Mạnh', N'Hùng', N'01/05/95', 51)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2011859, N'Thạch Quang', N'Minh', N'22/01/96', 119)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2012758, N'Phùng Chí', N'Kiên', N'01/11/95', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2013258, N'Nguyễn Tùng', N'Lâm', N'08/28/95', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2014058, N'Nguyễn Tuấn', N'Anh', N'06/11/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2014158, N'Hoàng Đức', N'Thắng', N'06/04/95', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2015358, N'Đoàn Anh', N'Tuấn', N'09/19/95', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2015558, N'Nguyễn Đức', N'Huy', N'01/08/95', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2016358, N'Nguyễn Đình', N'Ngọc', N'02/28/95', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2016458, N'Lưu Viết', N'Nam', N'06/24/94', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2016958, N'Trần Hồng', N'Liêm', N'12/24/95', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2017258, N'Đào Văn', N'Thiều', N'11/05/94', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2018258, N'Nguyễn Thị', N'Thúy', N'01/02/94', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2019058, N'Nguyễn Xuân', N'Nam', N'06/22/95', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2019758, N'Chung Thị Thanh', N'Tâm', N'04/08/95', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2021358, N'Dương Văn', N'Thành', N'08/23/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2022158, N'Nguyễn Văn', N'Phương', N'02/01/95', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2022258, N'Phí Thị Linh', N'Trang', N'06/05/95', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2022358, N'Nguyễn Hữu', N'Huy', N'07/01/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2022758, N'Phùng Khắc Hoàng', N'Anh', N'07/14/94', 55)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2026358, N'Nguyễn Văn', N'Minh', N'07/15/94', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2030258, N'Vũ Hoàng', N'Long', N'06/07/95', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2030558, N'Nguyễn Vũ Hoàng', N'Nam', N'04/07/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2030658, N'Nguyễn Trang', N'Ngân', N'02/23/95', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2031158, N'Nguyễn Quang', N'Huy', N'08/15/95', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2031358, N'Nguyễn Thị Minh', N'Hằng', N'10/07/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2031558, N'Nguyễn Minh', N'Cường', N'11/07/95', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2036458, N'Trần Quang', N'Đại', N'10/03/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2037558, N'Lưu Trọng', N'Huy', N'08/06/95', 51)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2039758, N'Nguyễn Thị Kim', N'Dung', N'09/22/95', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2040058, N'Nguyễn Trần', N'Nam', N'09/15/95', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2040858, N'Hà Văn', N'ánh', N'10/06/95', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2041558, N'Vũ Thành', N'Long', N'10/16/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2042058, N'Trần Hữu', N'Toại', N'01/20/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2043358, N'Nguyễn Mỹ', N'Linh', N'06/27/95', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2043858, N'Phạm Thị Thu', N'Hà', N'07/07/95', 51)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2044358, N'Nguyễn Thị', N'Thơm', N'10/20/95', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2045358, N'Lưu Thị Hải', N'Yến', N'04/18/94', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2047758, N'Đỗ Hải', N'Hưng', N'05/22/95', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2049358, N'Trần Doãn', N'Yên', N'07/23/95', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2050358, N'Đàm', N'Y', N'01/26/95', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2051858, N'Bùi Thanh', N'Tùng', N'07/17/95', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2053658, N'Nguyễn Thị Hương', N'Giang', N'09/03/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2054059, N'Nguyễn Hồng', N'Phúc', N'01/08/96', 120)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2055258, N'Đàm Thủy', N'Tiên', N'12/14/93', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2057058, N'Hoàng Minh', N'Tuấn', N'09/24/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2062058, N'Nguyễn Việt', N'Cường', N'10/17/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2065358, N'Vũ Quang', N'Vinh', N'01/15/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2065558, N'Trần Thị', N'Hà', N'11/07/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2065559, N'Bùi Thu ', N'Huyền', N'03/08/96', 120)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2069158, N'Nguyễn Thành', N'Đạt', N'08/04/95', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2070558, N'Bùi Quang', N'Cường', N'10/28/95', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2074958, N'Đỗ Ngọc', N'Quý', N'04/08/95', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2077358, N'Trần Đức', N'Lợi', N'01/18/95', 51)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2077658, N'Dương Thị', N'Duyên', N'08/22/95', 55)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2080258, N'Vương Mạnh', N'Linh', N'04/24/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2083058, N'Nguyễn Thị', N'Lý', N'10/13/95', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2083958, N'Nguyễn Thị Thu', N'Trang', N'08/19/95', 55)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2086058, N'Nguyễn Thế', N'Tùng', N'05/02/94', 55)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2087058, N'Nguyễn Danh', N'Tuyên', N'04/23/95', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2087158, N'Vũ Ngọc', N'Bảo', N'09/11/95', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2088258, N'Cao', N'Cường', N'05/15/95', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2088858, N'Nguyễn Đức', N'Thành', N'06/30/95', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2089558, N'Trần Trung', N'Hiếu', N'10/18/95', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2090458, N'Phạm Ngọc', N'Mai', N'02/28/95', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2090558, N'Nguyễn Thị Minh', N'Phương', N'11/07/95', 55)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2092558, N'Nguyễn Thị', N'Hương', N'12/27/95', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2092958, N'Nguyễn Phước Quý', N'Hoàn', N'02/17/95', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2093858, N'Đào Trọng', N'Nghĩa', N'08/06/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2097158, N'Đỗ Đình', N'Trung', N'02/14/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2097658, N'Nguyễn Thế', N'Quyết', N'08/09/95', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2098158, N'Nguyễn Thị Huyền', N'Trang', N'04/25/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2098258, N'Nguyễn Hải', N'Yến', N'12/01/95', 55)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2098858, N'Nguyễn Trường', N'Tuân', N'02/16/95', 52)
GO
print 'Processed 600 total records'
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2099658, N'Nguyễn Thu', N'Phương', N'05/19/95', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2100858, N'Nguyễn Thị', N'Lan', N'11/12/95', 51)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2102958, N'Nguyễn Thị', N'Phượng', N'03/07/95', 51)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2103458, N'Hứa Đức', N'Đạt', N'10/15/94', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2104058, N'Vũ Trung', N'Hiếu', N'09/13/95', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2107258, N'Trần Thị', N'Thùy', N'12/02/94', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2108358, N'Lương Thị', N'Dung', N'12/10/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2108458, N'Nguyễn Văn', N'Hậu', N'06/25/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2109158, N'Trần Văn', N'Minh', N'10/29/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2109558, N'Trần Văn', N'Cường', N'05/10/95', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2109658, N'Phạm Văn', N'Chính', N'09/11/95', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2109858, N'Nguyễn Văn', N'Vương', N'03/28/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2112158, N'Phạm Văn', N'Tuyên', N'03/12/95', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2112258, N'Nguyễn Thị', N'Huệ', N'12/21/95', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2113058, N'Trần Văn', N'Tuấn', N'04/04/93', 51)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2113758, N'Đỗ Quốc', N'Sơn', N'11/21/95', 55)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2114558, N'Tô Thị', N'Ngọc', N'01/20/95', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2114958, N'Nguyễn Văn', N'Ngọc', N'04/13/95', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2116258, N'Đinh Thị', N'Hương', N'06/28/95', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2116958, N'Nguyễn Thị Lan', N'Hương', N'05/06/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2117758, N'Đinh Thế', N'Hùng', N'03/13/94', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2117858, N'Mai Văn', N'Dũng', N'04/04/94', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2118558, N'Nguyễn Quốc', N'Trưởng', N'08/27/93', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2118858, N'Phạm Thị', N'Lương', N'02/10/93', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2119758, N'Nguyễn Văn', N'Tâm', N'07/30/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2119858, N'Đỗ Thị', N'Hoàn', N'08/13/95', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2120358, N'Phạm Thị', N'Nhị', N'07/30/95', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2121058, N'Nguyễn Văn', N'Đại', N'09/10/95', 51)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2121458, N'Nguyễn Văn', N'Cường', N'07/27/95', 55)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2123958, N'Nguyễn Đức', N'Thiện', N'12/02/95', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2126958, N'Hoàng Ngọc', N'Cẩn', N'07/01/94', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2127258, N'Nguyễn Quang', N'Phát', N'10/18/95', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2127858, N'Nguyễn Việt', N'Anh', N'05/01/95', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2128658, N'Nguyễn Mạnh', N'Cường', N'03/21/95', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2129558, N'Trần Đức', N'Thắng', N'11/11/95', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2132058, N'Đinh Thế', N'Đạt', N'11/10/94', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2134758, N'Bùi Văn', N'Mạnh', N'06/05/95', 51)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2135658, N'Lưu Mạnh', N'Hiến', N'08/28/95', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2136058, N'Nguyễn Anh', N'Tú', N'05/24/95', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2137258, N'Nguyễn Thị', N'Nga', N'02/03/95', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2137858, N'Phạm Đức', N'Việt', N'22/08/95', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2138358, N'Nguyễn Hải', N'Đăng', N'03/18/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2138758, N'Tạ Ngọc', N'Nam', N'10/02/95', 55)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2139558, N'Trương Văn', N'Công', N'08/01/95', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2140258, N'Hoàng Nguyễn Công', N'Tiến', N'25/01/94', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2141258, N'Nguyễn Thị', N'Yến', N'07/17/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2144258, N'Trần Đại', N'Nghĩa', N'10/11/95', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2144558, N'Nguyễn Thị Mai', N'Phương', N'02/02/95', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2145058, N'Phạm Thái', N'Sơn', N'09/26/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2146358, N'Nguyễn Thị Vân', N'Anh', N'05/26/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2147358, N'Vũ Thị Mai', N'Phương', N'08/27/95', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2147458, N'Đinh Thị Thùy', N'Trang', N'09/22/95', 51)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2148258, N'Trần Nhật', N'Quân', N'08/01/93', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2148358, N'Nguyễn Dương', N'Thuận', N'10/20/94', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2149358, N'Nguyễn Cường', N'Mạnh', N'05/19/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2154658, N'Nguyễn Thị', N'Thêm', N'02/15/95', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2154758, N'Đường Thị', N'Trang', N'10/09/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2155358, N'Đặng Nhật', N'Trường', N'09/16/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2156458, N'Trần Hoàng', N'Sơn', N'01/02/95', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2160458, N'Lê Hùng', N'Cường', N'01/06/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2163158, N'Mai Quốc', N'Thắng', N'06/01/94', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2163758, N'Phan Đình', N'Quang', N'06/28/94', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2164058, N'Trịnh Thị Quỳnh', N'Anh', N'08/19/95', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2164458, N'Trần Thị Thu', N'Trà', N'02/28/95', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2164858, N'Võ Thành', N'Vinh', N'04/19/95', 55)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2166158, N'Lý Hồng', N'Đức', N'04/29/95', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2168858, N'Hồ Thị', N'Hiền', N'04/20/95', 51)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2170458, N'Cao Xuân', N'Trường', N'12/01/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2171958, N'Nguyễn Công', N'Tiến', N'05/15/94', 55)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2172258, N'Nguyễn Thị Phương', N'Thảo', N'12/04/94', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2180658, N'Trịnh Văn', N'Đại', N'07/18/93', 55)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2181358, N'Triệu Quang', N'Hòa', N'10/23/94', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2181858, N'Phạm Thanh', N'Tuyển', N'03/24/94', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2182458, N'Nguyễn Phú', N'Thịnh', N'08/25/94', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2182858, N'Hoàng Bảo', N'Long', N'10/12/94', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2184258, N'Nguyễn Thu', N'Vân', N'11/08/94', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2184958, N'Lê Đức', N'Hiếu', N'08/08/93', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2185958, N'Đỗ Văn', N'Đức', N'05/13/94', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2186158, N'Vũ Ngọc', N'Sơn', N'02/07/93', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2186958, N'Cao Văn', N'Khánh', N'12/15/94', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2188358, N'Nguyễn Xuân', N'Hiếu', N'05/26/95', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2189258, N'Nguyễn Nam', N'Khánh', N'10/16/95', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2190958, N'Nguyễn Thị Hồng', N'Minh', N'03/06/94', 48)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2194158, N'Dương Thị', N'Liên', N'09/10/94', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2194558, N'Nguyễn Thị', N'Huệ', N'11/05/93', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2195458, N'Muộn Văn', N'Tuấn', N'07/03/94', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2195758, N'Vũ Phương', N'Linh', N'09/02/94', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2196058, N'Trương Diệu', N'Ly', N'12/17/94', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2196358, N'Nguyễn Văn', N'Ước', N'07/31/95', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2197058, N'Phạm Quang', N'Huy', N'06/25/94', 52)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2197458, N'Nguyễn Thành', N'Lâm', N'10/22/94', 51)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2200958, N'Phạm Thị Vân', N'Anh', N'06/22/94', 51)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2201858, N'Trần Thị', N'Phượng', N'03/08/94', 54)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2202058, N'Nguyễn Sĩ Trường', N'Thịnh', N'10/20/95', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2202558, N'Nguyễn Thị Thu', N'Hoài', N'08/27/93', 49)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2202758, N'Đỗ Tú', N'Phương', N'09/23/94', 53)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2204958, N'Bùi Ngọc', N'Kiều', N'05/18/93', 50)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2205358, N'Chu Quang', N'Tuấn', N'08/21/93', 71)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (2206158, N'Nguyễn Thị', N'Dung', N'11/16/93', 70)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3000858, N'Doãn Văn', N'Đại', N'02/12/94', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3005658, N'Nguyễn Xuân', N'Trường', N'12/28/93', 57)
GO
print 'Processed 700 total records'
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3009058, N'Cấn Thu', N'Huyền', N'08/07/95', 76)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3011958, N'Nguyễn Hữu', N'Huy', N'05/03/92', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3012958, N'Nguyễn Thiện', N'Việt', N'05/27/95', 78)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3025258, N'Đỗ Văn', N'Xuân', N'04/08/95', 76)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3047658, N'Vũ Mạnh', N'Linh', N'02/25/95', 63)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3058059, N'Nguyễn Đức', N'Anh', N'12/26/96', 89)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3058958, N'Hà Ngọc', N'Sơn', N'02/12/95', 38)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3067058, N'Vũ Việt', N'Huy', N'06/23/95', 83)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3071758, N'Nguyễn Anh', N'Tú', N'02/02/95', 76)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3072858, N'Trịnh Minh', N'Đức', N'02/08/95', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3074158, N'Cao Thành', N'Đạt', N'02/18/94', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3075958, N'Đỗ Minh', N'Hải', N'06/09/95', 78)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3079658, N'Tôn Thị', N'Yến', N'07/14/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3080358, N'Lê Sỹ', N'Vũ', N'11/02/95', 38)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3081258, N'Thái Tùng', N'Dương', N'07/03/95', 45)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3099958, N'Trần Văn', N'Cử', N'03/14/95', 75)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3102658, N'Vũ Trường', N'Sơn', N'10/20/95', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3108758, N'Trương Thùy', N'Dung', N'08/09/95', 76)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3129858, N'Lại Cao', N'Noel', N'12/25/95', 83)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3130758, N'Hồ Kim', N'Sáng', N'11/04/92', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3137458, N'Nguyễn Thị', N'Hiền', N'02/16/94', 78)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3140958, N'Bùi Đức', N'Trung', N'02/09/95', 78)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3142158, N'Mai Thị', N'Phương', N'06/27/95', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3146258, N'Nguyễn Thị', N'Ngọc', N'11/12/94', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3150158, N'Vũ Thị', N'Quyên', N'04/14/95', 76)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3154858, N'Nguyễn Thị', N'Linh', N'19/05/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3177358, N'Nguyễn Thị Thu', N'Hiền', N'04/15/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3180258, N'Phan Thị Mai', N'Hương', N'11/18/95', 56)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3181358, N'Đào Thị', N'Huệ', N'07/08/95', 60)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3188258, N'Nguyễn Quỳnh', N'Trang', N'11/14/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3206058, N'Nguyễn Khánh', N'Duy', N'09/04/95', 45)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3216458, N'Nguyễn Thị', N'Thảo', N'12/11/95', 76)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3237758, N'Phạm Thị Phương', N'Thảo', N'11/12/95', 76)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3245558, N'Bùi Đình', N'Trọng', N'09/20/95', 83)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3245758, N'Nguyễn Phương', N'Lan', N'04/25/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3256858, N'Cao Huy', N'Công', N'07/06/94', 45)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3262958, N'Trần Công', N'Minh', N'11/21/95', 64)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3271458, N'Nguyễn Chí', N'Minh', N'11/09/95', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3274858, N'Trần Nhật', N'Hải', N'12/25/95', 76)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3278358, N'Nguyễn Minh', N'Hoàng', N'07/19/95', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3281058, N'Hoàng Anh', N'Tuấn', N'08/14/95', 64)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3284158, N'Nguyễn Thành', N'Luân', N'01/01/95', 45)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3303858, N'Lê Quang', N'Quân', N'10/07/95', 86)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3305058, N'Nguyễn Ngọc', N'Sơn', N'10/12/95', 56)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3307358, N'Đặng Công', N'Sơn', N'01/03/95', 56)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3311058, N'Thái Giang', N'Long', N'02/06/95', 56)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3326158, N'Vũ Phương', N'Thảo', N'08/25/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3330558, N'Vũ Văn', N'Minh', N'08/06/95', 45)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3333258, N'Lê Minh', N'Tiến', N'12/25/93', 73)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3338458, N'Vũ Xuân Duy', N'Khánh', N'03/10/95', 56)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3340458, N'Nguyễn Tiến', N'Mạnh', N'02/15/95', 73)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3342458, N'Vũ Thị', N'Huệ', N'03/04/95', 73)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3345658, N'Nguyễn Danh', N'Cường', N'09/07/95', 84)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3346558, N'Trần Huy', N'Nam', N'05/15/95', 45)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3361858, N'Bùi Hoàng', N'Anh', N'10/13/95', 63)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3363058, N'Đặng Quốc', N'Trọng', N'05/28/94', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3371558, N'Nguyễn Thị', N'Yến', N'10/09/94', 78)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3373758, N'Trần Văn', N'Thịnh', N'01/05/95', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3374458, N'Bùi Ngọc', N'Khoa', N'06/10/95', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3378558, N'Nguyễn Nhật', N'Minh', N'10/31/95', 78)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3381158, N'Đồng Lan', N'Hương', N'01/09/95', 76)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3384958, N'Phạm Thị Như', N'Thúy', N'01/05/95', 45)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3385458, N'Vũ Thị', N'Hiền', N'07/17/95', 73)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3390158, N'Trương Thanh', N'Lam', N'06/30/95', 76)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3390458, N'Lê Thị', N'Hoa', N'09/16/95', 78)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3390958, N'Lê Hồng', N'Nhung', N'06/10/94', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3391958, N'Nguyễn Văn', N'Mạnh', N'01/28/93', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3392658, N'Nguyễn Thị', N'Liên', N'11/07/95', 62)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3395958, N'Thân Văn', N'Trung', N'04/19/95', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3398358, N'Trần Thị', N'Minh', N'12/11/95', 56)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3401758, N'Bùi Quang', N'Đức', N'05/15/93', 78)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3409858, N'Phạm Nguyễn Thùy', N'Trang', N'11/03/95', 76)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3409958, N'Lương Việt', N'Anh', N'02/01/94', 75)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3410258, N'Nguyễn Anh', N'Dũng', N'12/25/95', 76)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3410958, N'Trần Thị Thu', N'Hiền', N'09/22/94', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3414158, N'Ngô Văn', N'Huy', N'02/26/95', 73)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3414458, N'Đinh Thị Hà', N'My', N'07/02/95', 73)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3414958, N'Lê Thị Tú', N'Anh', N'07/17/94', 46)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3418058, N'Nguyễn Thị', N'Lê', N'01/05/95', 36)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3419058, N'Nguyễn Thị Ngọc', N'Yến', N'07/29/95', 78)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3423158, N'Trần Tuấn', N'Đạt', N'02/14/95', 76)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3425658, N'Lê Thị Phương', N'Thảo', N'05/09/95', 73)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3429158, N'Trần Tôn', N'Hải', N'01/21/95', 78)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3429258, N'Nguyễn Văn', N'Hùng', N'08/15/92', 78)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3436258, N'Đỗ Đức', N'Minh', N'09/25/95', 38)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3438558, N'Nguyễn Quang', N'Hợp', N'02/17/94', 73)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3440658, N'Lương Duy', N'Thái', N'05/18/95', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3444558, N'Phạm Thị ánh', N'Ngọc', N'03/23/95', 61)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3529558, N'Vũ Anh', N'Vũ', N'06/08/94', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3542058, N'Chu Thị', N'Hương', N'01/09/95', 73)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (3563758, N'Ngô Văn', N'Cảnh', N'09/08/94', 57)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4000758, N'Đàm Nhật', N'Linh', N'09/04/95', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4003858, N'Nguyễn Thanh', N'Tùng', N'03/31/95', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4007658, N'Trần Duy', N'Toàn', N'08/30/94', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4009458, N'Dương Thế', N'Vĩnh', N'10/29/95', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4010858, N'Nguyễn Thanh', N'Tú', N'03/07/95', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4011558, N'Quách Thị Hồng', N'Nhung', N'04/10/95', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4015958, N'Trương Văn', N'Chinh', N'02/27/92', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4017158, N'Trần Văn', N'Duy', N'07/10/94', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4018958, N'Nguyễn Văn', N'Hùng', N'02/04/95', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4030258, N'Nguyễn Văn', N'Nam', N'10/01/95', 67)
GO
print 'Processed 800 total records'
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4126058, N'Nguyễn Văn', N'Thắng', N'02/10/94', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4128858, N'Nguyễn Phương', N'Nhung', N'02/02/95', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4224358, N'Nguyễn Thị', N'Nhung', N'12/09/95', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4283558, N'Đỗ Minh', N'Đức', N'03/26/94', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4361958, N'Phạm Đăng Duy', N'Anh', N'05/23/95', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4533058, N'Lương Tuyết', N'Trinh', N'01/29/95', 67)
INSERT [dbo].[SINHVIEN] ([MaSV], [HoSV], [TenSV], [NgaySinh], [IdLop]) VALUES (4577258, N'Nguyễn Thị', N'Dung', N'05/11/95', 67)
/****** Object:  Table [dbo].[XEPPHONG]    Script Date: 01/22/2015 08:55:47 ******/
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
/****** Object:  Table [dbo].[DIEMTHI]    Script Date: 01/22/2015 08:55:47 ******/
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
/****** Object:  ForeignKey [FK_BaiLam_KyThi]    Script Date: 01/22/2015 08:55:47 ******/
ALTER TABLE [dbo].[BAILAM]  WITH CHECK ADD  CONSTRAINT [FK_BaiLam_KyThi] FOREIGN KEY([IdKyThi])
REFERENCES [dbo].[KYTHI] ([ID])
GO
ALTER TABLE [dbo].[BAILAM] CHECK CONSTRAINT [FK_BaiLam_KyThi]
GO
/****** Object:  ForeignKey [FK_DAPAN_KYTHI]    Script Date: 01/22/2015 08:55:47 ******/
ALTER TABLE [dbo].[DAPAN]  WITH CHECK ADD  CONSTRAINT [FK_DAPAN_KYTHI] FOREIGN KEY([IdKyThi])
REFERENCES [dbo].[KYTHI] ([ID])
GO
ALTER TABLE [dbo].[DAPAN] CHECK CONSTRAINT [FK_DAPAN_KYTHI]
GO
/****** Object:  ForeignKey [FK_DIEMTHI_NAMHOC]    Script Date: 01/22/2015 08:55:47 ******/
ALTER TABLE [dbo].[DIEMTHI]  WITH CHECK ADD  CONSTRAINT [FK_DIEMTHI_NAMHOC] FOREIGN KEY([IdNamHoc])
REFERENCES [dbo].[NAMHOC] ([ID])
GO
ALTER TABLE [dbo].[DIEMTHI] CHECK CONSTRAINT [FK_DIEMTHI_NAMHOC]
GO
/****** Object:  ForeignKey [FK_ThongKe_SinhVien]    Script Date: 01/22/2015 08:55:47 ******/
ALTER TABLE [dbo].[DIEMTHI]  WITH CHECK ADD  CONSTRAINT [FK_ThongKe_SinhVien] FOREIGN KEY([MaSV])
REFERENCES [dbo].[SINHVIEN] ([MaSV])
GO
ALTER TABLE [dbo].[DIEMTHI] CHECK CONSTRAINT [FK_ThongKe_SinhVien]
GO
/****** Object:  ForeignKey [FK_KT_PHONG_KyThi]    Script Date: 01/22/2015 08:55:47 ******/
ALTER TABLE [dbo].[KT_PHONG]  WITH CHECK ADD  CONSTRAINT [FK_KT_PHONG_KyThi] FOREIGN KEY([IdKyThi])
REFERENCES [dbo].[KYTHI] ([ID])
GO
ALTER TABLE [dbo].[KT_PHONG] CHECK CONSTRAINT [FK_KT_PHONG_KyThi]
GO
/****** Object:  ForeignKey [FK_KT_PHONG_PhongThi]    Script Date: 01/22/2015 08:55:47 ******/
ALTER TABLE [dbo].[KT_PHONG]  WITH CHECK ADD  CONSTRAINT [FK_KT_PHONG_PhongThi] FOREIGN KEY([IdPhong])
REFERENCES [dbo].[PHONGTHI] ([ID])
GO
ALTER TABLE [dbo].[KT_PHONG] CHECK CONSTRAINT [FK_KT_PHONG_PhongThi]
GO
/****** Object:  ForeignKey [FK_Lop_Khoa]    Script Date: 01/22/2015 08:55:47 ******/
ALTER TABLE [dbo].[LOP]  WITH CHECK ADD  CONSTRAINT [FK_Lop_Khoa] FOREIGN KEY([IdKhoa])
REFERENCES [dbo].[KHOA] ([ID])
GO
ALTER TABLE [dbo].[LOP] CHECK CONSTRAINT [FK_Lop_Khoa]
GO
/****** Object:  ForeignKey [FK_SinhVien_Lop]    Script Date: 01/22/2015 08:55:47 ******/
ALTER TABLE [dbo].[SINHVIEN]  WITH CHECK ADD  CONSTRAINT [FK_SinhVien_Lop] FOREIGN KEY([IdLop])
REFERENCES [dbo].[LOP] ([ID])
GO
ALTER TABLE [dbo].[SINHVIEN] CHECK CONSTRAINT [FK_SinhVien_Lop]
GO
/****** Object:  ForeignKey [FK_XepPhong_KyThi]    Script Date: 01/22/2015 08:55:47 ******/
ALTER TABLE [dbo].[XEPPHONG]  WITH CHECK ADD  CONSTRAINT [FK_XepPhong_KyThi] FOREIGN KEY([IdKyThi])
REFERENCES [dbo].[KYTHI] ([ID])
GO
ALTER TABLE [dbo].[XEPPHONG] CHECK CONSTRAINT [FK_XepPhong_KyThi]
GO
/****** Object:  ForeignKey [FK_XepPhong_SinhVien]    Script Date: 01/22/2015 08:55:47 ******/
ALTER TABLE [dbo].[XEPPHONG]  WITH CHECK ADD  CONSTRAINT [FK_XepPhong_SinhVien] FOREIGN KEY([IdSV])
REFERENCES [dbo].[SINHVIEN] ([MaSV])
GO
ALTER TABLE [dbo].[XEPPHONG] CHECK CONSTRAINT [FK_XepPhong_SinhVien]
GO
