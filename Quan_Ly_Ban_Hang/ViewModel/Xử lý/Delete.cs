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
    }
}
