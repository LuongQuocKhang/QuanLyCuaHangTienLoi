using Quan_Ly_Ban_Hang.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Hang.ViewModel.Xử_lý
{
    class Insert
    {
        private static Insert _instance;
        public static Insert Instance { get { if (_instance == null) _instance = new Insert(); return _instance; } set => _instance = value; }
        public void ThemDonDatHang(DONDATHANG dondathang)
        {
            DataProvider.Instance.DB.DONDATHANGs.Add(dondathang);
            DataProvider.Instance.DB.SaveChanges();
        }
        public void ThemChiTietDDH(CHITIETDONDATHANG chitietDDH)
        {
            DataProvider.Instance.DB.CHITIETDONDATHANGs.Add(chitietDDH);
            THONGKEDONHANG thongke = DataProvider.Instance.DB.THONGKEDONHANGs.Where(p => p.MADONDATHANG == chitietDDH.MADONDATHANG).SingleOrDefault();
            if ( thongke != null )
            {
                thongke.TIENDATHANG += chitietDDH.TONGTIENCHITIET;
                HANG hang = DataProvider.Instance.DB.HANGs.Where(p => p.MAHANG == chitietDDH.MAHANG).SingleOrDefault();
                hang.SOLUONGTON += chitietDDH.SOLUONGNHAP;
            }
            DataProvider.Instance.DB.SaveChanges();
        }
        public void ThemHoaDonBH(HOADONBH hoadon)
        {
            DataProvider.Instance.DB.HOADONBHs.Add(hoadon);
            DataProvider.Instance.DB.SaveChanges();
        }
        public void ThemChiTietHoaDonBH(CHITIETHOADON chitiethoadon)
        {
            DataProvider.Instance.DB.CHITIETHOADONs.Add(chitiethoadon);
            DataProvider.Instance.DB.SaveChanges();
        }

        public void ThemThongTinSanPham (HANG sanpham)
        {
            DataProvider.Instance.DB.HANGs.Add(sanpham);
            DataProvider.Instance.DB.SaveChanges();
        }

        public void ThemThongKeDonHang(THONGKEDONHANG thongke)
        {
            DataProvider.Instance.DB.THONGKEDONHANGs.Add(thongke);
            DataProvider.Instance.DB.SaveChanges();
        }
        public void ThemThongKeHoaDon(THONGKEHOADON thongke)
        {
            DataProvider.Instance.DB.THONGKEHOADONs.Add(thongke);
            DataProvider.Instance.DB.SaveChanges();
        }
        public void ThemChiTietHD(CHITIETHOADON chitiet)
        {
            DataProvider.Instance.DB.CHITIETHOADONs.Add(chitiet);
            THONGKEHOADON thongke = DataProvider.Instance.DB.THONGKEHOADONs.Where(p => p.MAHOADONBH == chitiet.MAHOADONBH).SingleOrDefault();
            if (thongke != null)
            {
                thongke.TIENBANHANG += chitiet.TONGTIEN;
                HANG hang = DataProvider.Instance.DB.HANGs.Where(p => p.MAHANG == chitiet.MAHANG).SingleOrDefault();
                hang.SOLUONGTON -= chitiet.SOLUONGBAN;
            }
            DataProvider.Instance.DB.SaveChanges();
        }

        public void ThemThongTinTaiKhoan(TAIKHOAN taikhoan)
        {
            DataProvider.Instance.DB.TAIKHOANs.Add(taikhoan);
            DataProvider.Instance.DB.SaveChanges();
        }
        public void ThemThongTinNhanVien(NHANVIEN nhanvien)
        {
            DataProvider.Instance.DB.NHANVIENs.Add(nhanvien);
            DataProvider.Instance.DB.SaveChanges();
        }
    }
}
