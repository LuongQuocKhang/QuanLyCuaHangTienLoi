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
MADONDATHANG int identity(1,1) primary key,
MANHACUNGCAP char(8),
MACUAHANG char(6),
NGAYDATHANG smalldatetime,
NGAYGIAOHANG smalldatetime,
MAHINHTHUCTHANHTOAN INT
)
select * from DONDATHANG

insert into DONDATHANG values('123','FM01','3/10/2018','4/2/2018','1')
insert into DONDATHANG values('148','FM01','3/20/2018','4/1/2018','2')
insert into DONDATHANG values('148','FM01','3/30/2018','4/10/2018','2')


-- HINHTHUCTHANHTOAN
CREATE TABLE HINHTHUCTHANHTOAN(
MAHINHTHUCTHANHTOAN INT identity(1,1) primary key,
TENHINHTHUCTHANHTOAN VARCHAR(10)
)
INSERT INTO HINHTHUCTHANHTOAN VALUES('TIEN MAT')
INSERT INTO HINHTHUCTHANHTOAN VALUES('CARD')


--CHITIETDONDATHANG
CREATE TABLE CHITIETDONDATHANG(
MACHITIETDONDATHANG int identity(1,1) primary key,
MADONDATHANG int,
MAHANG varchar(8),
SOLUONGNHAP int NOT NULL
)
select * from CHITIETDONDATHANG


--NHACUNGCAP
CREATE TABLE NHACUNGCAP(
MANHACUNGCAP char(8) not null,
TENNHACUNGCAP varchar(30)
constraint pk_NCC primary key(MANHACUNGCAP)
)
insert into NHACUNGCAP values ('123',N'CONGTY CP GN')
insert into NHACUNGCAP values ('148',N'CONGTY TNHH ')


--CUAHANG
CREATE TABLE CUAHANG(
MACUAHANG char(6) not null,
DIACHI varchar(50)
constraint pk_CH primary key(MACUAHANG)
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


--HOADONBANHANG
CREATE TABLE HOADONBH(
MAHOADONBH char(8) not null,
NGAYLAPHOADON smalldatetime,
MACUAHANG char(6)
constraint pk_HDBH primary key(MAHOADONBH)
)


--CHITIETHOADON
CREATE TABLE CHITIETHOADON(
MACHITIETHOADON char(8) not null,
MAHOADONBH char(8),
MAHANG varchar(8),
SOLUONGBAN int
constraint pk_CTHD primary key(MACHITIETHOADON)
)


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