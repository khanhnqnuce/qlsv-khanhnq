use QLSV;
go
Create table Taikhoan
(
	ID int identity(1,1) primary key,
	TaiKhoan nvarchar(100),
	MatKhau nvarchar(255),
	HoTen nvarchar(255),
	Quyen nvarchar(50),
	constraint UQ_TaiKhoan unique (TaiKhoan)
);
go
INSERT [dbo].[Taikhoan] ([TaiKhoan], [MatKhau], [HoTen], [Quyen]) VALUES (N'admin', N'e10adc3949ba59abbe56e057f20f883e', N'admin', N'quantri')
go
Create table Kythi
(
	ID int identity(1,1) primary key,
	MaKyThi nvarchar(255),
	TenKyThi nvarchar(255),
	NgayThi DateTime,
	ThoiGianLamBai int,
	ThoiGianBatDau nvarchar(255),
	ThoiGianKetThuc nvarchar(255)
)
go
Create table PhongThi
(
	ID int identity(1,1) primary key,
	TenPhong nvarchar(255),
	SucChua int,
	GhiChu nvarchar(255),
	constraint UQ_MaPhong unique (MaPhong)
)
go
Create table SinhVien
(
	ID int identity(1,1) primary key,
	MaSinhVien nvarchar(50),
	HoSinhVien nvarchar(255),
	TenSinhVien nvarchar(255),
	MaLop nvarchar(255),	
	constraint UQ_MaSinhVien unique(MaSinhVien)
)
go
Create table Khoa
(
	ID int identity(1,1) primary key,
	MaKhoa nvarchar(50),
	TenKhoa nvarchar(255),	
	constraint UQ_MaKhoa unique(MaKhoa)
)
go
Create table Lop
(
	ID int identity(1,1) primary key,
	MaLop nvarchar(50),
	MaKhoa nvarchar(255),
	GhiChu nvarchar(255),	
	constraint UQ_MaLop unique(MaLop)
)
