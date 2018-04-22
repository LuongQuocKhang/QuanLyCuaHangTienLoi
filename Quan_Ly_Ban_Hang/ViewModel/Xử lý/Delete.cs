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
            foreach (var item in collection)
            {
                DataProvider.Instance.DB.CHITIETDONDATHANGs.Remove(item);
            }
            DataProvider.Instance.DB.DONDATHANGs.Remove(dondathang);
            DataProvider.Instance.DB.SaveChanges();
        }
        public void XoaChiTietDDH(CHITIETDONDATHANG chitiet)
        {
            CHITIETDONDATHANG chitietdonhang = DataProvider.Instance.DB.CHITIETDONDATHANGs.Where(p => p.MADONDATHANG == chitiet.MADONDATHANG)
                                                .Where(p => p.MACHITIETDONDATHANG == chitiet.MACHITIETDONDATHANG).SingleOrDefault();
            DataProvider.Instance.DB.CHITIETDONDATHANGs.Remove(chitietdonhang);
            DataProvider.Instance.DB.SaveChanges();
        }
    }
}
