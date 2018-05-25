using Quan_Ly_Ban_Hang.Model;
using Quan_Ly_Ban_Hang.ViewModel.Xử_lý;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Quan_Ly_Ban_Hang.ViewModel.Nhập_hàng
{
    public class DataContextTimKiem : BaseViewModel
    {
        #region list
        public ObservableCollection<DONDATHANG> ListDonDatHang { get; set; }
        public List<NHACUNGCAP> Listnhacungcap { get; set; }
        public List<HINHTHUCTHANHTOAN> listHTTT { get; set; }
        #endregion

        #region properties
        private string manhacungcap;
        public string Manhacungcap { get => manhacungcap; set { if (manhacungcap != value) { manhacungcap = value; OnPropertyChanged(); } } }
        private DateTime? ngayDatHang;
        public DateTime? NgayDatHang { get => ngayDatHang; set { if (ngayDatHang != value) { ngayDatHang = value; OnPropertyChanged(); } } }

        private DateTime? ngayGiaoHang;
        public DateTime? NgayGiaoHang { get => ngayGiaoHang; set { if (ngayGiaoHang != value) { ngayGiaoHang = value; OnPropertyChanged(); } } }

        private int hinhThucThanhToan;
        public int HinhThucThanhToan { get => hinhThucThanhToan; set { if (hinhThucThanhToan != value) { hinhThucThanhToan = value; OnPropertyChanged(); } } }
        #endregion

        #region command
        public ICommand CB_NCC_SelectedItemhangedCommand { get; set; }
        public ICommand CB_HTTT_SelectedItemChangedCommand { get; set; }
        public ICommand NDH_SelectedDateChanged { get; set; }
        public ICommand NGH_SelectedDateChanged { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        #endregion

        public DataContextTimKiem()
        {
            Loadinfo();
            new Task(() =>
            {
                LoadCommand();
            }).Start();
        }

        private void LoadCommand()
        {
            CB_HTTT_SelectedItemChangedCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                HinhThucThanhToan = (p as ComboBox).SelectedIndex + 1;
                var collection = ListDonDatHang.Where(x => x.MAHINHTHUCTHANHTOAN == HinhThucThanhToan).ToList();
                ListDonDatHang.Clear();
                foreach (var item in collection)
                {
                    ListDonDatHang.Add(item);
                }
            });
            CB_NCC_SelectedItemhangedCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                Manhacungcap = ((p as ComboBox).SelectedItem as NHACUNGCAP).MANHACUNGCAP;
                var collection = ListDonDatHang.Where(x => x.MANHACUNGCAP == Manhacungcap).ToList();
                ListDonDatHang.Clear();
                foreach (var item in collection)
                {
                    ListDonDatHang.Add(item);
                }
            });
            NDH_SelectedDateChanged = new RelayCommand<object>((p) => true, (p) =>
            {
                if ( NgayDatHang > NgayGiaoHang)
                {
                    return;
                }
                var collection = ListDonDatHang.Where(x => x.NGAYDATHANG.Value >= NgayDatHang && x.NGAYDATHANG <= DateTime.Now).ToList();
                ListDonDatHang.Clear();
                foreach (var item in collection)
                {
                    ListDonDatHang.Add(item);
                }

            });
            NGH_SelectedDateChanged = new RelayCommand<object>((p) => true, (p) =>
            {
                if (NgayGiaoHang < NgayDatHang)
                {
                    return;
                }
                var collection = ListDonDatHang.Where(x => x.NGAYGIAOHANG.Value >= NgayGiaoHang && x.NGAYGIAOHANG.Value <= DateTime.Now).ToList();
                ListDonDatHang.Clear();
                foreach (var item in collection)
                {
                    ListDonDatHang.Add(item);
                }
            });
            RefreshCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                ListDonDatHang.Clear();
                var collection = Load.Instance.LoadDonDatHang();
                foreach (var item in collection)
                {
                    ListDonDatHang.Add(item);
                }
            });
            ExitCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                (p as Window).Close();
            });
        }

        private void Loadinfo()
        {
            NgayDatHang = DateTime.Now;
            NgayGiaoHang = DateTime.Now;

            ListDonDatHang = Load.Instance.LoadDonDatHang();
            Listnhacungcap = DataProvider.Instance.DB.NHACUNGCAPs.ToList();
            listHTTT = DataProvider.Instance.DB.HINHTHUCTHANHTOANs.ToList();
        }
    }
}
