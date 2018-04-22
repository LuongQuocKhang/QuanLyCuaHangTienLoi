using Quan_Ly_Ban_Hang.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Hang.ViewModel.Xử_lý
{
    class Load
    {
        private static Load _instance;
        public static Load Instance { get { if (_instance == null) _instance = new Load(); return _instance; } set => _instance = value; }

        /// <summary>
        /// load số hóa đơn của đơn đặt hàng
        /// </summary>
        /// <returns></returns>
        public string Load_So_Hoa_Don_Nhap_Hang()
        {
            var temp = DataProvider.Instance.DB.DONDATHANGs.ToList();
            return (temp[temp.Count - 1].MADONDATHANG + 1).ToString();
        }
        public string Load_So_Hoa_Don_Ban_Hang()
        {
            var temp = DataProvider.Instance.DB.HOADONBHs.ToList();
            return (temp[temp.Count - 1].MAHOADONBH + 1).ToString();
        }

        public CUAHANG Load_Cua_Hang()
        {
            return DataProvider.Instance.DB.CUAHANGs.SingleOrDefault();
        }

        public ObservableCollection<HANG> Load_Thong_Tin_Hang()
        {

            ObservableCollection<HANG> lists=new ObservableCollection<HANG>();
            var temp = DataProvider.Instance.DB.HANGs.ToList();
            foreach (var item in temp)
            {
                lists.Add(item);
            }
            return lists;
        }

        public List<HINHTHUCTHANHTOAN> Load_HTTT()
        {
            return DataProvider.Instance.DB.HINHTHUCTHANHTOANs.ToList();
        }
        public ObservableCollection<DONDATHANG> LoadDonDatHang()
        {
            ObservableCollection<DONDATHANG> lists = new ObservableCollection<DONDATHANG>();
            var temp = DataProvider.Instance.DB.DONDATHANGs.ToList();
            foreach (var item in temp)
            {
                lists.Add(item);
            }
            return lists;
        }
        public ObservableCollection<CHITIETDONDATHANG> LoadChiTietDonHang()
        {
            ObservableCollection<CHITIETDONDATHANG> lists = new ObservableCollection<CHITIETDONDATHANG>();
            var temp = DataProvider.Instance.DB.CHITIETDONDATHANGs.ToList();
            foreach (var item in temp)
            {
                lists.Add(item);
            }
            return lists;
        }
    }
}
