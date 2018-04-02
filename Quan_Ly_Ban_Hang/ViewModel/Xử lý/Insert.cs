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
            DataProvider.Instance.DB.SaveChanges();
        }
    }
}
