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
    }
}
