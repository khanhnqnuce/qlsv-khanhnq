use QLSV;
go
Create table Taikhoan
(
	ID int identity(1,1) ,
	TaiKhoan nvarchar(100) not null,
	MatKhau nvarchar(255),
	HoTen nvarchar(255),
	Quyen nvarchar(50) not null,
	constraint UQ_TaiKhoan unique (TaiKhoan),
	CONSTRAINT PK_TaiKhoan  primary key(ID),
);
go
INSERT [dbo].[Taikhoan] ([TaiKhoan], [MatKhau], [HoTen], [Quyen]) VALUES (N'admin', N'e10adc3949ba59abbe56e057f20f883e', N'admin', N'quantri')
go
Create table Kythi
(
	ID int identity(1,1),
	MaKyThi nvarchar(255) not null,
	TenKyThi nvarchar(255),
	NgayThi DateTime not null,
	ThoiGianLamBai int not null,
	ThoiGianBatDau nvarchar(255) not null,
	ThoiGianKetThuc nvarchar(255)
	CONSTRAINT PK_KyThi  primary key(ID),
)
go
Create table PhongThi
(
	ID int identity(1,1),
	TenPhong nvarchar(255) not null,
	SucChua int not null,
	SoLuong int,
	GhiChu nvarchar(255),
	--constraint UQ_MaPhong unique (TenPhong),
	CONSTRAINT PK_PhongThi  primary key(ID),
)
go
Create table Khoa
(
	ID int identity(1,1),
	MaKhoa nvarchar(255),
	TenKhoa nvarchar(255) not null,
	CONSTRAINT PK_Khoa  primary key(ID),
)
go
Create table Lop
(
	ID int identity(1,1),
	MaLop nvarchar(255)not null,
	GhiChu nvarchar(255),
	IdKhoa int not null,
	CONSTRAINT PK_Lop  primary key(ID),
	constraint UQ_MaLop unique(MaLop),
	CONSTRAINT FK_Lop_Khoa FOREIGN KEY (IdKhoa) REFERENCES Khoa(ID)
)
go
Create table SinhVien
(
	ID int identity(1,1),
	MaSinhVien nvarchar(255) not null,
	HoSinhVien nvarchar(255) not null,
	TenSinhVien nvarchar(255) not null,
	IdLop int not null,
	CONSTRAINT PK_SinhVien  primary key(ID),
	CONSTRAINT UQ_MaSinhVien unique(MaSinhVien),
	CONSTRAINT FK_SinhVien_Lop FOREIGN KEY (IdLop) REFERENCES Lop(ID)
)
go
Create table XepPhong
(
	IdSV int not null,	
	IdPhong int not null,
	IdKyThi int not null,
	CONSTRAINT PK_XepPhong  primary key(IdSV,IdPhong,IdKyThi),
	CONSTRAINT FK_XepPhong_SinhVien FOREIGN KEY (IdSV) REFERENCES SinhVien(ID),
	CONSTRAINT FK_XepPhong_PhongThi FOREIGN KEY (IdPhong) REFERENCES PhongThi(ID),
	CONSTRAINT FK_XepPhong_KyThi FOREIGN KEY (IdKyThi) REFERENCES KyThi(ID)
)
go
CREATE TABLE DapAn
(
	ID int identity(1,1),
	IdKyThi int not null,
	MaMon nvarchar(255) not null,
	MaDe nvarchar(255) not null,
	CauHoi nvarchar(255) not null,
	DapAn nvarchar(255) not null,
	ThangDiem int,
	CONSTRAINT PK_DapAn  primary key(ID),
	CONSTRAINT FK_DapAn_KyThi FOREIGN KEY (IdKyThi) REFERENCES Kythi(ID),
)
go
CREATE TABLE KetQua
(
	ID int identity(1,1),
	IdKyThi int not null,
	MaSinhVien nvarchar(255) not null,
	MaDe nvarchar(255) not null,
	KetQua nvarchar(255) not null,
	CONSTRAINT PK_KetQua  primary key(ID),
	CONSTRAINT FK_KetQua_KyThi FOREIGN KEY (IdKyThi) REFERENCES Kythi(ID),
)

TRUNCATE TABLE SinhVien
ALTER TABLE XepPhong ADD CONSTRAINT PK_XepPhong primary key(IdSV,IdPhong,IdKyThi)
ALTER TABLE XepPhong ADD CONSTRAINT FK_XepPhong_SinhVien FOREIGN KEY (IdSV) REFERENCES SinhVien(ID)