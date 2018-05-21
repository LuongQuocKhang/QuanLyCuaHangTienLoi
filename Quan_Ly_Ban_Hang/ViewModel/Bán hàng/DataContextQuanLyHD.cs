using Quan_Ly_Ban_Hang.Model;
using Quan_Ly_Ban_Hang.View;
using Quan_Ly_Ban_Hang.ViewModel.Xử_lý;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Quan_Ly_Ban_Hang.ViewModel
{
    public class DataContextQuanLyHD : BaseViewModel
    {
        #region properties
        private string sohoadon;
        public string SoHoaDon { get => sohoadon; set { if (sohoadon != value) { sohoadon = value; OnPropertyChanged(); } } }
        private DateTime? ngayLapHoaDon;
        public DateTime? NgayLapHoaDon { get => ngayLapHoaDon; set { if (ngayLapHoaDon != value) { ngayLapHoaDon = value; OnPropertyChanged(); } } }
        private int? soLuong;
        public int? SoLuong { get => soLuong; set { if (soLuong != value) { soLuong = value; OnPropertyChanged(); } } }
        private string tenHang;
        public string TenHang { get => tenHang; set { if (tenHang != value) { tenHang = value; OnPropertyChanged(); } } }
        private string maHang;
        public string MaHang { get => maHang; set { if (maHang != value) { maHang = value; OnPropertyChanged(); } } }
        #endregion

        #region List
        public ObservableCollection<HOADONBH> ListHoaDon { get; set; }
        public ObservableCollection<CHITIETHOADON> ListChiTiet { get; set; }
        public ObservableCollection<HANG> ListTenHang { get; set; }
        #endregion

        #region Command
        public ICommand SelectedItemListViewChangedCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand XoaHoaDon { get; set; }
        public ICommand ThemChiTietHD { get; set; }
        public ICommand XoaChiTietHD { get; set; }
        public ICommand SuaChiTietHD { get; set; }
        public ICommand SelectedProductCommand { get; set; }
        public ICommand Chitiet_SelectedItemListViewChangedCommand { get; set; }
        #endregion

        public DataContextQuanLyHD()
        {
            LoadInfo();
            new Task(() =>
            {
                LoadCommand();
            }).Start();
        }

        private void LoadCommand()
        {
            SelectedItemListViewChangedCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                ListChiTiet.Clear();
                int index = (p as ListView).SelectedIndex;
                if ( index >= 0 )
                {
                    string mahoadon = ListHoaDon[index].MAHOADONBH;
                    var collection = DataProvider.Instance.DB.CHITIETHOADONs.Where(chitiet => chitiet.MAHOADONBH == mahoadon).ToList();
                    foreach (var item in collection)
                    {
                        ListChiTiet.Add(item);
                    }
                    SoHoaDon = mahoadon;
                    NgayLapHoaDon = ListHoaDon[index].NGAYLAPHOADON;
                }        
            });
            OpenCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                Ban_Hang HoaDonBanHang = new Ban_Hang();
                HoaDonBanHang.DataContext = new DataContextHDBH();
                HoaDonBanHang.ShowDialog();
            });           
            Chitiet_SelectedItemListViewChangedCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                if ((p as ListView).SelectedIndex >= 0)
                {
                    SoLuong = ((p as ListView).SelectedItem as CHITIETHOADON).SOLUONGBAN;
                    MaHang = ((p as ListView).SelectedItem as CHITIETHOADON).MAHANG;
                    TenHang = ListTenHang.Where(h => h.MAHANG == MaHang).SingleOrDefault().TENHANG;
                }
            });
            SelectedProductCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                MaHang = ((p as ComboBox).SelectedItem as HANG).MAHANG;
                TenHang = ((p as ComboBox).SelectedItem as HANG).TENHANG;
            });     
            RefreshCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                ListHoaDon = Load.Instance.LoadHoaDonBanHang();
            });

            int loaiNV = DataProvider.Instance.DB.NHANVIENs.Where(x => x.MANHANVIEN == User.Instance.MaNhanVien).Single().MALOAINV.Value;
            if ( loaiNV == 1)
            {
                ThemChiTietHD = new RelayCommand<object>((p) => true, (p) =>
                {
                    if (ListChiTiet.Where(h => h.MAHANG == MaHang).ToList().Count > 0)
                    {
                        MessageBox.Show("Mặt hàng đã tồn tại trong đơn đặt hàng");
                        return;
                    }
                    else
                    {
                        try
                        {
                            CHITIETHOADON chitiet = new CHITIETHOADON();
                            chitiet.MAHOADONBH = SoHoaDon;
                            chitiet.MAHANG = MaHang;
                            chitiet.SOLUONGBAN = SoLuong;
                            chitiet.TONGTIEN = SoLuong * (int)ListTenHang.Where(sp => sp.MAHANG == chitiet.MAHANG).SingleOrDefault().DONGIA.Value;

                            Insert.Instance.ThemChiTietHD(chitiet);
                            MessageBox.Show("Thêm thành công");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message.ToString());
                        }
                    }
                });
                XoaChiTietHD = new RelayCommand<object>((p) => true, (p) =>
                {
                    int index = (p as ListView).SelectedIndex;
                    if (index >= 0)
                    {
                        MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa không ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            try
                            {
                                CHITIETHOADON chitiet = ((p as ListView).SelectedItem as CHITIETHOADON);
                                Delete.Instance.XoaChiTietHoaDon(chitiet);
                                (p as ListView).SelectedIndex = 0;
                                ListChiTiet.RemoveAt(index);
                                MessageBox.Show("Xóa thành công");
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.ToString());
                            }
                        }
                    }
                });
                SuaChiTietHD = new RelayCommand<object>((p) => true, (p) =>
                {
                    int index = (p as ListView).SelectedIndex;
                    if (index < 0) return;
                    try
                    {
                        ListChiTiet[index].SOLUONGBAN = SoLuong;
                        ListChiTiet[index].TONGTIEN = ListChiTiet[index].SOLUONGBAN * (int)ListTenHang.Where(sp => sp.MAHANG == ListChiTiet[index].MAHANG).SingleOrDefault().DONGIA.Value;
                        Update.Instance.Update_ChiTietHD(ListChiTiet[index].MACHITIETHOADON, SoLuong.Value);
                        MessageBox.Show("Cập nhật thành công");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                });
                XoaHoaDon = new RelayCommand<object>((p) => true, (p) =>
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa hóa đơn không ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            int index = (p as ListView).SelectedIndex;
                            Delete.Instance.XoaHoaDonBanHang(ListHoaDon[index]);
                            (p as ListView).SelectedIndex = 0;
                            ListHoaDon.RemoveAt(index);
                            MessageBox.Show("Xóa thành công");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message.ToString());
                        }
                    }
                });
            }
        }

        private void LoadInfo()
        {
            ListHoaDon = Load.Instance.LoadHoaDonBanHang();
            ListChiTiet = new ObservableCollection<CHITIETHOADON>();
            ListTenHang = Load.Instance.Load_Thong_Tin_Hang();
        }
    }
}
