using Quan_Ly_Ban_Hang.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Hang.ViewModel
{
    class Update
    {
        private static Update _instance;
        public static Update Instance { get { if (_instance == null) _instance = new Update(); return _instance; } set => _instance = value; }

        public void Update_SoLuongTon_NhapHang(string id , int soluongnhap)
        {
            HANG sanpham = DataProvider.Instance.DB.HANGs.Where((p) => p.MAHANG == id).SingleOrDefault();
            sanpham.SOLUONGTON = sanpham.SOLUONGTON + soluongnhap;
            DataProvider.Instance.DB.SaveChanges();
        }
        public void Update_SoLuongTon_BanHang(string id, int soluongban)
        {
            HANG sanpham = DataProvider.Instance.DB.HANGs.Where((p) => p.MAHANG == id).SingleOrDefault();
            sanpham.SOLUONGTON = sanpham.SOLUONGTON - soluongban;
            DataProvider.Instance.DB.SaveChanges();
        }
        public void Update_ThongTinSanPham(HANG hang)
        {
            HANG sanpham = DataProvider.Instance.DB.HANGs.Where(h => h.MAHANG == hang.MAHANG).SingleOrDefault();
            sanpham.TENHANG = hang.TENHANG;
            sanpham.DONGIA = hang.DONGIA;
            DataProvider.Instance.DB.SaveChanges();
        }
        public void Update_DonDatHang(DONDATHANG dondathang)
        {
            DONDATHANG temp = DataProvider.Instance.DB.DONDATHANGs.Where(d => d.MADONDATHANG == dondathang.MADONDATHANG).SingleOrDefault();
            temp.NGAYGIAOHANG = dondathang.NGAYGIAOHANG;
            temp.MAHINHTHUCTHANHTOAN = dondathang.MAHINHTHUCTHANHTOAN;
            temp.MANHACUNGCAP = dondathang.MANHACUNGCAP;
            DataProvider.Instance.DB.SaveChanges();
        }
        public void Update_ChiTietDDH(int MachitietDDH , int SoLuong)
        {
            CHITIETDONDATHANG chitietdonhang = DataProvider.Instance.DB.CHITIETDONDATHANGs.Where(p => p.MACHITIETDONDATHANG == MachitietDDH).SingleOrDefault();
            chitietdonhang.SOLUONGNHAP = SoLuong;

            THONGKEDONHANG thongke = DataProvider.Instance.DB.THONGKEDONHANGs.Where(p => p.MADONDATHANG == chitietdonhang.MADONDATHANG).SingleOrDefault();
            if (thongke != null)
            {
                int? tongtien = 0;
                foreach (var item in DataProvider.Instance.DB.CHITIETDONDATHANGs.Where(p => p.MADONDATHANG == chitietdonhang.MADONDATHANG))
                {
                    tongtien += item.TONGTIENCHITIET;
                }
                thongke.TIENDATHANG = tongtien;
            }

            DataProvider.Instance.DB.SaveChanges();
        }
        public void Update_ChiTietHD(int MachitietHD, int SoLuong)
        {
            CHITIETHOADON chitietdonhang = DataProvider.Instance.DB.CHITIETHOADONs.Where(p => p.MACHITIETHOADON == MachitietHD).SingleOrDefault();
            chitietdonhang.SOLUONGBAN = SoLuong;

            THONGKEHOADON thongke = DataProvider.Instance.DB.THONGKEHOADONs.Where(p => p.MAHOADONBH == chitietdonhang.MAHOADONBH).SingleOrDefault();
            if (thongke != null)
            {
                int? tongtien = 0;
                foreach (var item in DataProvider.Instance.DB.CHITIETHOADONs.Where(p => p.MAHOADONBH == chitietdonhang.MAHOADONBH))
                {
                    tongtien += item.TONGTIEN;
                }
                thongke.TIENBANHANG = tongtien;
            }

            DataProvider.Instance.DB.SaveChanges();
        }
    }
}
