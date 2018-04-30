using Quan_Ly_Ban_Hang.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Hang.ViewModel.Xử_lý
{
    class Delete
    {
        private static Delete _instance;
        public static Delete Instance { get { if (_instance == null) _instance = new Delete(); return _instance; } set => _instance = value; }
        public void XoaThongTinSanPham(HANG sanpham)
        {
            DataProvider.Instance.DB.HANGs.Remove(sanpham);
            DataProvider.Instance.DB.SaveChanges();
        }
        public void XoaDonDatHang(DONDATHANG dondathang)
        {
            List<CHITIETDONDATHANG> collection = DataProvider.Instance.DB.CHITIETDONDATHANGs.Where((p) => p.MADONDATHANG == dondathang.MADONDATHANG).ToList();
            if ( collection.Count > 0)
            {
                foreach (var item in collection)
                {
                    DataProvider.Instance.DB.CHITIETDONDATHANGs.Remove(item);
                }
            }
            THONGKEDONHANG thongke = DataProvider.Instance.DB.THONGKEDONHANGs.Where(h => h.MADONDATHANG == dondathang.MADONDATHANG).SingleOrDefault();
            if ( thongke != null )
            {
                DataProvider.Instance.DB.THONGKEDONHANGs.Remove(thongke);
            }

            DataProvider.Instance.DB.DONDATHANGs.Remove(dondathang);
            DataProvider.Instance.DB.SaveChanges();
        }
        public void XoaChiTietDDH(CHITIETDONDATHANG chitiet)
        {
            CHITIETDONDATHANG chitietdonhang = DataProvider.Instance.DB.CHITIETDONDATHANGs.Where(p => p.MADONDATHANG == chitiet.MADONDATHANG)
                                                .Where(p => p.MACHITIETDONDATHANG == chitiet.MACHITIETDONDATHANG).SingleOrDefault();
            THONGKEDONHANG thongke =  DataProvider.Instance.DB.THONGKEDONHANGs.Where(p => p.MADONDATHANG == chitiet.MADONDATHANG).SingleOrDefault();
            if ( thongke != null)
            {
                thongke.TIENDATHANG -= chitietdonhang.TONGTIENCHITIET;
            }
            HANG hang = DataProvider.Instance.DB.HANGs.Where(p => p.MAHANG == chitiet.MAHANG).SingleOrDefault();
            hang.SOLUONGTON -= chitiet.SOLUONGNHAP;
            DataProvider.Instance.DB.CHITIETDONDATHANGs.Remove(chitietdonhang);
            DataProvider.Instance.DB.SaveChanges();
        }
        public void XoaHoaDonBanHang(HOADONBH hoadon)
        {
            List<CHITIETHOADON> collection = DataProvider.Instance.DB.CHITIETHOADONs.Where((p) => p.MAHOADONBH == hoadon.MAHOADONBH).ToList();
            if (collection.Count > 0)
            {
                foreach (var item in collection)
                {
                    DataProvider.Instance.DB.CHITIETHOADONs.Remove(item);
                }
            }
            THONGKEHOADON thongke = DataProvider.Instance.DB.THONGKEHOADONs.Where(h => h.MAHOADONBH == hoadon.MAHOADONBH).SingleOrDefault();
            if (thongke != null)
            {
                DataProvider.Instance.DB.THONGKEHOADONs.Remove(thongke);
            }

            DataProvider.Instance.DB.HOADONBHs.Remove(hoadon);
            DataProvider.Instance.DB.SaveChanges();
        }
        public void XoaChiTietHoaDon(CHITIETHOADON chitiet)
        {
            CHITIETHOADON chitiethoadon = DataProvider.Instance.DB.CHITIETHOADONs.Where(p => p.MAHOADONBH == chitiet.MAHOADONBH)
                                                .Where(p => p.MACHITIETHOADON == chitiet.MACHITIETHOADON).SingleOrDefault();

            THONGKEHOADON thongke = DataProvider.Instance.DB.THONGKEHOADONs.Where(p => p.MAHOADONBH == chitiet.MAHOADONBH).SingleOrDefault();
            if (thongke != null)
            {
                thongke.TIENBANHANG -= chitiethoadon.TONGTIEN;
            }

            HANG hang = DataProvider.Instance.DB.HANGs.Where(p => p.MAHANG == chitiet.MAHANG).SingleOrDefault();
            hang.SOLUONGTON += chitiet.SOLUONGBAN;
            DataProvider.Instance.DB.CHITIETHOADONs.Remove(chitiethoadon);
            DataProvider.Instance.DB.SaveChanges();
        }
    }
}
