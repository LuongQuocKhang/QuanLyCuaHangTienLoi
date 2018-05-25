using Quan_Ly_Ban_Hang.Model;
using Quan_Ly_Ban_Hang.ViewModel.Xử_lý;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Quan_Ly_Ban_Hang.ViewModel.Bán_hàng
{
    public class DataContextTimKiemHD : BaseViewModel
    {
        #region properties 
        private DateTime ngayLapHoaDon;
        public DateTime NgayLapHoaDon { get => ngayLapHoaDon; set { if (ngayLapHoaDon != value) { ngayLapHoaDon = value; OnPropertyChanged(); } } }
        #endregion

        #region list
        public List<NHANVIEN> ListNhanVien { get; set; }
        public ObservableCollection<HOADONBH> ListHoaDon { get; set; }
        #endregion

        #region command
        public ICommand CB_TenNV_SelectedItemhangedCommand { get; set; }
        public ICommand NLHD_SelectedDateChanged { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        #endregion
        public DataContextTimKiemHD()
        {
            LoadInfo();
            new Task(() =>
            {
                Command();
            }).Start();
        }

        private void Command()
        {
            CB_TenNV_SelectedItemhangedCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                int index = (p as ComboBox).SelectedIndex;
                string manv = ListNhanVien[index].MANHANVIEN;
                var collection = ListHoaDon.Where(x => x.MANHANVIEN == manv).ToList();
                ListHoaDon.Clear();
                foreach (var item in collection)
                {
                    ListHoaDon.Add(item);
                }
            });
            NLHD_SelectedDateChanged = new RelayCommand<object>((p) => true, (p) =>
            {
                if (NgayLapHoaDon > DateTime.Now) return;
                var collection = ListHoaDon.Where(x => x.NGAYLAPHOADON.Value >= NgayLapHoaDon && x.NGAYLAPHOADON <= DateTime.Now).ToList();
                ListHoaDon.Clear();
                foreach (var item in collection)
                {
                    ListHoaDon.Add(item);
                }
            });
            RefreshCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                ListHoaDon.Clear();
                var collection = Load.Instance.LoadHoaDonBanHang();
                foreach (var item in collection)
                {
                    ListHoaDon.Add(item);
                }
            });
            ExitCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                (p as Window).Close();
            });
        }

        public void LoadInfo()
        {
            NgayLapHoaDon = DateTime.Now;
            ListNhanVien = DataProvider.Instance.DB.NHANVIENs.ToList();
            ListHoaDon = Load.Instance.LoadHoaDonBanHang();
        }
    }
}
