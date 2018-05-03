﻿DROP DATABASE QLBH
CREATE DATABASE QLBH
GO
--------------
USE QLBH
GO
SET DATEFORMAT mdy
---------------
--DON DAT HANG
CREATE TABLE DONDATHANG(
MADONDATHANG varchar(6) primary key,
MANHACUNGCAP char(8),
MACUAHANG char(6),
NGAYDATHANG smalldatetime,
NGAYGIAOHANG smalldatetime,
MAHINHTHUCTHANHTOAN INT
)
select * from DONDATHANG
insert into DONDATHANG values('DDH001','123','FM01','3/10/2018','4/2/2018','1')

-- HINHTHUCTHANHTOAN
CREATE TABLE HINHTHUCTHANHTOAN(
MAHINHTHUCTHANHTOAN INT identity(1,1) primary key,
TENHINHTHUCTHANHTOAN VARCHAR(10)
)
select * from HINHTHUCTHANHTOAN
INSERT INTO HINHTHUCTHANHTOAN VALUES('TIEN MAT')
INSERT INTO HINHTHUCTHANHTOAN VALUES('CARD')


--CHITIETDONDATHANG
CREATE TABLE CHITIETDONDATHANG(
MACHITIETDONDATHANG int identity(1,1) primary key,
MADONDATHANG varchar(6),
MAHANG varchar(8),
SOLUONGNHAP int NOT NULL,
TONGTIENCHITIET int
)
drop table CHITIETDONDATHANG
select * from CHITIETDONDATHANG



--NHACUNGCAP
CREATE TABLE NHACUNGCAP(
MANHACUNGCAP char(8) not null primary key,
TENNHACUNGCAP varchar(30)
)
insert into NHACUNGCAP values ('123',N'CONGTY CP GN')
insert into NHACUNGCAP values ('148',N'CONGTY TNHH ')


--CUAHANG
CREATE TABLE CUAHANG(
MACUAHANG char(6) not null primary key,
DIACHI varchar(50)
)
insert into CUAHANG values('FM01',N'134 Pasteur , P Ben nghe , Q1')


--HANG
CREATE TABLE HANG(
MAHANG varchar(8) not null,
TENHANG varchar(15),
DONGIA money,
SOLUONGTON int
constraint pk_H primary key(MAHANG)
)
select * from HANG
insert into hang values('0123456','sting',10000,20)
insert into hang values('0123486','bánh',10000,10)
insert into hang values('0128647','keo',2000,10)
insert into hang values('0129542','mi goi',5000,10)
insert into hang values('0125436','nuoc suoi',6000,10)
insert into hang values('0121462','banh mi',12000,10)
insert into hang values('0176581','coffee',12000,10)

--HOADONBANHANG
CREATE TABLE HOADONBH(
MAHOADONBH varchar(6) primary key,
NGAYLAPHOADON smalldatetime,
MACUAHANG char(6),
MANHANVIEN VARCHAR(4)
)
select * from HOADONBH
insert into HOADONBH values ('HD0001','4/7/2018','FM01','NV01')
--CHITIETHOADON
CREATE TABLE CHITIETHOADON(
MACHITIETHOADON INT identity(1,1) primary key,
MAHOADONBH varchar(6),
MAHANG varchar(8),
SOLUONGBAN int,
TONGTIEN int
)
select* from CHITIETHOADON

--THAMSO
CREATE TABLE THAMSO(
MATHAMSO INT PRIMARY KEY,
SOLUONGNHAPTOITHIEU int,
SOLUONGTONTOIDADUOCNHAP int,
SOLUONGTONTOITHIEUDUOCBAN int
)
insert into THAMSO values(1,10,0,1)


--khoangoaichobang DONDATHANG
ALTER TABLE DONDATHANG ADD CONSTRAINT fk01_DDH FOREIGN KEY(MANHACUNGCAP) REFERENCES NHACUNGCAP(MANHACUNGCAP)
ALTER TABLE DONDATHANG ADD CONSTRAINT fk02_DDH FOREIGN KEY(MACUAHANG) REFERENCES CUAHANG(MACUAHANG)
ALTER TABLE DONDATHANG ADD CONSTRAINT fk03_DDH FOREIGN KEY(MAHINHTHUCTHANHTOAN) REFERENCES HINHTHUCTHANHTOAN(MAHINHTHUCTHANHTOAN)
--khoangoaichobang CHITIETDONDATHANG
ALTER TABLE CHITIETDONDATHANG ADD CONSTRAINT fk01_CTDDH FOREIGN KEY(MADONDATHANG) REFERENCES DONDATHANG(MADONDATHANG)
ALTER TABLE CHITIETDONDATHANG ADD CONSTRAINT fk02_CTDDH FOREIGN KEY(MAHANG) REFERENCES HANG(MAHANG)


--khoangoaichobang HOADONBH
ALTER TABLE HOADONBH ADD CONSTRAINT fk01_HDBH FOREIGN KEY(MACUAHANG) REFERENCES CUAHANG(MACUAHANG)
--khoangoaichobang CHITIETHOADON
ALTER TABLE CHITIETHOADON ADD CONSTRAINT fk01_CTHD FOREIGN KEY(MAHOADONBH) REFERENCES HOADONBH(MAHOADONBH)
ALTER TABLE CHITIETHOADON ADD CONSTRAINT fk02_CTHD FOREIGN KEY(MAHANG) REFERENCES HANG(MAHANG)

create table NHANVIEN
(
	MANHANVIEN varchar(4) primary key,
	TENNHANVIEN VARCHAR(20),
	LOAINHANVIEN VARCHAR(10),
)
INSERT INTO NHANVIEN VALUES ('NV01','NGUYEN VAN A','QUAN LY')
INSERT INTO NHANVIEN VALUES ('NV02','NGUYEN VAN B','QUAN LY')
INSERT INTO NHANVIEN VALUES ('NV03','NGUYEN VAN C','NHAN VIEN')
CREATE TABLE TAIKHOAN 
(
	MATAIKHOAN INT identity(1,1) primary key,
	TAIKHOAN VARCHAR(10),
	MATKHAU VARCHAR(10),
	MANHANVIEN VARCHAR(4)
)


INSERT INTO TAIKHOAN VALUES ('root','123','NV01') 
ALTER TABLE HOADONBH ADD CONSTRAINT fk01_NV FOREIGN KEY(MANHANVIEN) REFERENCES NHANVIEN(MANHANVIEN)
ALTER TABLE TAIKHOAN ADD CONSTRAINT fk02_NV FOREIGN KEY(MANHANVIEN) REFERENCES NHANVIEN(MANHANVIEN)

CREATE TABLE THONGKEDONHANG
(
	MATHONGKEDONHANG INT identity(1,1) primary key,
	MADONDATHANG VARCHAR(6),
	TIENDATHANG INT
)
select * from THONGKEDONHANG

CREATE TABLE THONGKEHOADON
(
	MATHONGKEHOADON INT identity(1,1) primary key,
	MAHOADONBH VARCHAR(6),
	TIENBANHANG INT
)
select * from THONGKEHOADON
ALTER TABLE THONGKEDONHANG ADD CONSTRAINT fk01_DDH_TK FOREIGN KEY(MADONDATHANG) REFERENCES DONDATHANG(MADONDATHANG)
ALTER TABLE THONGKEHOADON ADD CONSTRAINT fk01_HD_TK FOREIGN KEY(MAHOADONBH) REFERENCES HOADONBH(MAHOADONBH)	