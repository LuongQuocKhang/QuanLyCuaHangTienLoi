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
        public List<DONDATHANG> LoadDonDatHangTheoNhaCungCap(string manhacungcap)
        {
            return DataProvider.Instance.DB.DONDATHANGs.Where(x => x.MANHACUNGCAP == manhacungcap).ToList();
        }
        public List<DONDATHANG> LoadDonDatHangTheoHinhThucTT(int mahinhthucthanhtoan)
        {
            return DataProvider.Instance.DB.DONDATHANGs.Where(x => x.MAHINHTHUCTHANHTOAN == mahinhthucthanhtoan).ToList();
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
        public ObservableCollection<HOADONBH> LoadHoaDonBanHang()
        {
            ObservableCollection<HOADONBH> lists = new ObservableCollection<HOADONBH>();
            var temp = DataProvider.Instance.DB.HOADONBHs.ToList();
            foreach (var item in temp)
            {
                lists.Add(item);
            }
            return lists;
        }
        public ObservableCollection<CHITIETHOADON> LoadChiTietHoaDonBanHang()
        {
            ObservableCollection<CHITIETHOADON> lists = new ObservableCollection<CHITIETHOADON>();
            var temp = DataProvider.Instance.DB.CHITIETHOADONs.ToList();
            foreach (var item in temp)
            {
                lists.Add(item);
            }
            return lists;
        }

        public ObservableCollection<NHANVIEN> Load_Thong_Tin_Nhan_Vien()
        {

            ObservableCollection<NHANVIEN> lists = new ObservableCollection<NHANVIEN>();
            var temp = DataProvider.Instance.DB.NHANVIENs.ToList();
            foreach (var item in temp)
            {
                lists.Add(item);
            }
            return lists;
        }

        public ObservableCollection<TAIKHOAN> Load_Thong_Tin_Tai_Khoan()
        {

            ObservableCollection<TAIKHOAN> lists = new ObservableCollection<TAIKHOAN>();
            var temp = DataProvider.Instance.DB.TAIKHOANs.ToList();
            foreach (var item in temp)
            {
                lists.Add(item);
            }
            return lists;
        }
        public ObservableCollection<string> Load_Thong_Tin_LoaiNV()
        {

            ObservableCollection<string> lists = new ObservableCollection<string>();
            var temp = DataProvider.Instance.DB.LOAINHANVIENs.Select(x => x.TENLOAINV).ToList();
            foreach (var item in temp)
            {
                lists.Add(item);
            }
            return lists;
        }

        public ObservableCollection<THONGKEDONHANG> LoadThongKeDonHang()
        {
            ObservableCollection<THONGKEDONHANG> donhang = new ObservableCollection<THONGKEDONHANG>();
            var collection = DataProvider.Instance.DB.THONGKEDONHANGs;
            foreach (var item in collection)
            {
                donhang.Add(item);
            }
            return donhang;
        }
        public ObservableCollection<THONGKEHOADON> LoadThongKeHoaDon()
        {
            ObservableCollection<THONGKEHOADON> donhang = new ObservableCollection<THONGKEHOADON>();
            var collection = DataProvider.Instance.DB.THONGKEHOADONs;
            foreach (var item in collection)
            {
                donhang.Add(item);
            }
            return donhang;
        }
        public THAMSO LoadQuiDinh()
        {
            return DataProvider.Instance.DB.THAMSOes.Single();
        }
    }
}
