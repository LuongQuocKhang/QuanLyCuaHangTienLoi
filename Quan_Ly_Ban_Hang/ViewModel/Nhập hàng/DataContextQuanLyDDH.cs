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
//using System.Diagnostics;
//using System.Windows;

namespace Quan_Ly_Ban_Hang.ViewModel
{
    public class DataContextQuanLyDDH : BaseViewModel
    {
        //Stopwatch st = new Stopwatch();

        #region list
        public ObservableCollection<DONDATHANG> ListDonDatHang { get; set; }
        public ObservableCollection<CHITIETDONDATHANG> ListChiTiet { get; set; }
        public List<NHACUNGCAP> Listnhacungcap { get; set; }
        public List<HINHTHUCTHANHTOAN> listHTTT { get; set; }
        public ObservableCollection<HANG> ListTenHang { get; set; }
        #endregion

        #region properties
        private string sodonhang;
        public string Sodonhang { get => sodonhang; set { if (sodonhang != value) { sodonhang = value; OnPropertyChanged(); } } }

        private string manhacungcap;
        public string Manhacungcap { get => manhacungcap; set { if (manhacungcap != value) { manhacungcap = value; OnPropertyChanged(); } } }

        private string tennhacungcap;
        public string Tennhacungcap { get => tennhacungcap; set { if (tennhacungcap != value) { tennhacungcap = value; OnPropertyChanged(); } } }

        private string diachi;
        public string DiaChi { get => diachi; set { if (diachi != value) { diachi = value; OnPropertyChanged(); } } }

        private string maCuaHang;
        public string MaCuaHang { get => maCuaHang; set { if (maCuaHang != value) { maCuaHang = value; OnPropertyChanged(); } } }

        private string maHang;
        public string MaHang { get => maHang; set { if (maHang != value) { maHang = value; OnPropertyChanged(); } } }

        private string tenHang;
        public string TenHang { get => tenHang; set { if (tenHang != value) { tenHang = value; OnPropertyChanged(); } } }

        private DateTime? ngayDatHang;
        public DateTime? NgayDatHang { get => ngayDatHang; set { if (ngayDatHang != value) { ngayDatHang = value; OnPropertyChanged(); } } }

        private DateTime? ngayGiaoHang;
        public DateTime? NgayGiaoHang { get => ngayGiaoHang; set { if (ngayGiaoHang != value) { ngayGiaoHang = value; OnPropertyChanged(); } } }

        private int hinhThucThanhToan;
        public int HinhThucThanhToan { get => hinhThucThanhToan; set { if (hinhThucThanhToan != value) { hinhThucThanhToan = value; OnPropertyChanged(); } } }

        private string tenHinhThucThanhToan;
        public string TenHinhThucThanhToan { get => tenHinhThucThanhToan; set { if (tenHinhThucThanhToan != value) { tenHinhThucThanhToan = value; OnPropertyChanged(); } } }

        private int soLuong;
        public int SoLuong { get => soLuong; set { if (soLuong != value) { soLuong = value; OnPropertyChanged(); } } }
        #endregion

        #region Command
        public ICommand OpenCommand { get; set; }
        public ICommand XoaDonDatHang { get; set; }
        public ICommand CapNhatDonHang { get; set; }
        public ICommand SelectedItemListViewChangedCommand { get; set; }
        public ICommand Chitiet_SelectedItemListViewChangedCommand { get; set; }
        public ICommand CB_NCC_SelectedItemhangedCommand { get; set; }
        public ICommand CB_HTTT_SelectedItemChangedCommand { get; set; }
        public ICommand SelectedProductCommand { get; set; }
        public ICommand ThemChiTietDDH { get; set; }
        public ICommand XoaChiTietDDH { get; set; }
        public ICommand SuaChiTietDDH { get; set; }
        public ICommand RefreshCommand { get; set; }
        #endregion

        public DataContextQuanLyDDH()
        {
            Loadinfo();
            new Task( () =>
            {
                LoadCommand();
            }).Start();
        }

        private void LoadCommand()
        {
            OpenCommand = new RelayCommand<object>((p) => true, (p) =>
            {
               Don_Dat_Hang dondathang = new Don_Dat_Hang();
               dondathang.DataContext = new DataContextDDH();
               dondathang.Closing += (sender, e) =>
               {
                    ListDonDatHang = Load.Instance.LoadDonDatHang();
               };
               dondathang.ShowDialog();
            });
            SelectedItemListViewChangedCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                //st.Start();
                ListChiTiet.Clear();
                int index = (p as ListView).SelectedIndex;
                if ( index >= 0 )
                {
                    string madondathang = ListDonDatHang[index].MADONDATHANG;
                    var collection = DataProvider.Instance.DB.CHITIETDONDATHANGs.ToList().Where(d => d.MADONDATHANG == madondathang).ToList();
                    foreach (var item in collection)
                    {
                        ListChiTiet.Add(item);
                    }
                    Sodonhang = ListDonDatHang[index].MADONDATHANG;
                    string manhacungcap = ListDonDatHang[index].MANHACUNGCAP;
                    Manhacungcap = DataProvider.Instance.DB.NHACUNGCAPs.Where(n => n.MANHACUNGCAP == manhacungcap).SingleOrDefault().MANHACUNGCAP;
                    Tennhacungcap = DataProvider.Instance.DB.NHACUNGCAPs.Where(n => n.MANHACUNGCAP == manhacungcap).SingleOrDefault().TENNHACUNGCAP;
                    NgayDatHang = ListDonDatHang[index].NGAYDATHANG;
                    NgayGiaoHang = ListDonDatHang[index].NGAYGIAOHANG;
                    string macuahang = ListDonDatHang[index].MACUAHANG;
                    DiaChi = DataProvider.Instance.DB.CUAHANGs.Where(n => n.MACUAHANG == macuahang).SingleOrDefault().DIACHI;
                    TenHinhThucThanhToan = listHTTT[ListDonDatHang[index].MAHINHTHUCTHANHTOAN.Value - 1].TENHINHTHUCTHANHTOAN;
                }      
                // đo thời gian thực thi
                //st.Stop();
                //long timeelapsed = st.ElapsedMilliseconds;
                //MessageBox.Show(timeelapsed.ToString);
            });
            CB_NCC_SelectedItemhangedCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                Manhacungcap = ((p as ComboBox).SelectedItem as NHACUNGCAP).MANHACUNGCAP;
            });
            XoaDonDatHang = new RelayCommand<object>((p) => true, (p) =>
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa đơn đặt hàng không ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        int index = (p as ListView).SelectedIndex;

                        Delete.Instance.XoaDonDatHang(ListDonDatHang[index]);
                        (p as ListView).SelectedIndex = 0;
                        ListDonDatHang.RemoveAt(index);
                        MessageBox.Show("Xóa thành công");
                        ListDonDatHang = Load.Instance.LoadDonDatHang();
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }
            });
            CapNhatDonHang = new RelayCommand<object> ((p) => true, (p) =>
            {
                int index = (p as ListView).SelectedIndex;
                if ( index >= 0 )
                {
                    try
                    {
                        ListDonDatHang[index].NGAYGIAOHANG = NgayGiaoHang;
                        ListDonDatHang[index].MANHACUNGCAP = Manhacungcap;
                        ListDonDatHang[index].MAHINHTHUCTHANHTOAN = HinhThucThanhToan;
                        Update.Instance.Update_DonDatHang(ListDonDatHang[index]);
                        MessageBox.Show("cập nhật thành công .");
                        ListDonDatHang = Load.Instance.LoadDonDatHang();
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }
            });
            CB_HTTT_SelectedItemChangedCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                HinhThucThanhToan = (p as ComboBox).SelectedIndex + 1;
            });
            Chitiet_SelectedItemListViewChangedCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                if ((p as ListView).SelectedIndex >= 0)
                {
                    SoLuong = ((p as ListView).SelectedItem as CHITIETDONDATHANG).SOLUONGNHAP;
                    MaHang = ((p as ListView).SelectedItem as CHITIETDONDATHANG).MAHANG;
                    TenHang = ListTenHang.Where(h => h.MAHANG == MaHang).SingleOrDefault().TENHANG;
                }
            });
            SelectedProductCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                MaHang = ((p as ComboBox).SelectedItem as HANG).MAHANG;
                TenHang = ((p as ComboBox).SelectedItem as HANG).TENHANG;
            });
            ThemChiTietDDH = new RelayCommand<object>((p) => true, (p) =>
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
                        CHITIETDONDATHANG chitiet = new CHITIETDONDATHANG();
                        chitiet.MADONDATHANG = sodonhang;
                        chitiet.MAHANG = MaHang;
                        chitiet.SOLUONGNHAP = SoLuong;
                        chitiet.TONGTIENCHITIET = SoLuong * (int)ListTenHang.Where(sp => sp.MAHANG == chitiet.MAHANG).SingleOrDefault().DONGIA.Value;

                        Insert.Instance.ThemChiTietDDH(chitiet);
                        ListChiTiet.Add(chitiet);
                        MessageBox.Show("Thêm thành công");
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }
            });
            XoaChiTietDDH = new RelayCommand<object>((p) => true, (p) =>
            {
                int index = (p as ListView).SelectedIndex;
                if (index >= 0)
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa không ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            CHITIETDONDATHANG chitiet = ((p as ListView).SelectedItem as CHITIETDONDATHANG);
                            Delete.Instance.XoaChiTietDDH(chitiet);
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
            SuaChiTietDDH = new RelayCommand<object>((p) => true, (p) =>
            {
                int index = (p as ListView).SelectedIndex;
                if (index < 0) return;
                try
                {  
                    ListChiTiet[index].SOLUONGNHAP = SoLuong;
                    ListChiTiet[index].TONGTIENCHITIET = ListChiTiet[index].SOLUONGNHAP * (int)ListTenHang.Where(sp => sp.MAHANG == ListChiTiet[index].MAHANG).SingleOrDefault().DONGIA.Value;
                    Update.Instance.Update_ChiTietDDH(ListChiTiet[index].MACHITIETDONDATHANG, SoLuong);
                    MessageBox.Show("Cập nhật thành công");
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            });
            RefreshCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                ListDonDatHang = Load.Instance.LoadDonDatHang();
            });
        }
        private void Loadinfo()
        {
            ListChiTiet = new ObservableCollection<CHITIETDONDATHANG>();
            ListDonDatHang = Load.Instance.LoadDonDatHang();
            Listnhacungcap = DataProvider.Instance.DB.NHACUNGCAPs.ToList();
            listHTTT = DataProvider.Instance.DB.HINHTHUCTHANHTOANs.ToList();
            ListTenHang = Load.Instance.Load_Thong_Tin_Hang();
        }
    }
}
