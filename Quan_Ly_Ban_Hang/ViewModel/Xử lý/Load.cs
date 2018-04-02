using Quan_Ly_Ban_Hang.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Hang.ViewModel.Xử_lý
{
    class Load
    {
        private static Load _instance;
        public static Load Instance { get { if (_instance == null) _instance = new Load(); return _instance; } set => _instance = value; }

        public string Load_So_Hoa_Don()
        {
            var temp = DataProvider.Instance.DB.DONDATHANGs.ToList();
            return (temp[temp.Count - 1].MADONDATHANG + 1).ToString();
        }
        public CUAHANG Load_Cua_Hang()
        {
            return DataProvider.Instance.DB.CUAHANGs.SingleOrDefault();
        }
        public List<HANG> Load_Thong_Tin_Hang()
        {
            return DataProvider.Instance.DB.HANGs.ToList();
        }
        public List<HINHTHUCTHANHTOAN> Load_HTTT()
        {
            return DataProvider.Instance.DB.HINHTHUCTHANHTOANs.ToList();
        }
    }
}
