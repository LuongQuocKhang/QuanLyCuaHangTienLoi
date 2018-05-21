using Quan_Ly_Ban_Hang.Model;
using Quan_Ly_Ban_Hang.View;
using Quan_Ly_Ban_Hang.View.Quản_lý_hóa_đơn_bán_hàng;
using Quan_Ly_Ban_Hang.View.Thống_kê_doanh_thu;
using Quan_Ly_Ban_Hang.ViewModel.Thống_kê_doanh_thu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Quan_Ly_Ban_Hang.ViewModel
{
    public class DataContext : BaseViewModel
    {
        #region Properties
        private string name;
        public string Name { get => name; set { if (name != value) { name = value; OnPropertyChanged(); } } }

        public Window win { get; set; }
        #endregion
        public ICommand NhapHangCommand { get; set; }
        public ICommand BanHangCommand { get; set; }
        public ICommand QuanLiCommand { get; set; }
        public ICommand TimKiemCommand { get; set; }
        public ICommand MouseDownCommand { get; set; }
        public ICommand QuanLiTaiKhoanCommand { get; set; }
        public ICommand QuanLiNhanVienCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand ClosingCommand { get; set; }
        public ICommand ThongKeCommand { get; set; }
        public ICommand ThayDoiQuyDinhCommand { get; set; }
        public DataContext(Window window)
        {
            win = window;
            LoadInfo();
            new Task(() =>
            {
                Command();
            }).Start();
        }

        private void LoadInfo()
        {
            Name = User.Instance.TenNhanVien;
        }
        private bool isLogOut = false;
        public void Command()
        {
            int loaiNV = DataProvider.Instance.DB.NHANVIENs.Where(x => x.MANHANVIEN == User.Instance.MaNhanVien).Single().MALOAINV.Value;

            // command dùng chung
            BanHangCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                QuanLyHoaDonBH quanlyhoadon = new QuanLyHoaDonBH();
                quanlyhoadon.DataContext = new DataContextQuanLyHD();
                quanlyhoadon.ShowDialog();
            });
            TimKiemCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                Tim_Kiem TimKiem = new Tim_Kiem();
                TimKiem.DataContext = new DataContextTK();
                TimKiem.ShowDialog();
            });
            MouseDownCommand = new RelayCommand<Grid>((p) => true, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.DragMove();
                }
            });
            LogOutCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                isLogOut = true;
                (p as Window).Close();
            });
            ExitCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                Application.Current.Shutdown();
            });
            ClosingCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                if (isLogOut == true)
                {
                    Login login = new Login();
                    login.Show();
                }
            });

            // command dùng cho quản lý
            if (loaiNV == 1)
            {
                NhapHangCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                Quan_Ly_DDH QuanlyDDH = new Quan_Ly_DDH();
                QuanlyDDH.DataContext = new DataContextQuanLyDDH();
                QuanlyDDH.ShowDialog();
            });
                QuanLiCommand = new RelayCommand<object>((p) => true, (p) =>
                {
                    Quan_Li_Thong_Tin QuanLiThongTin = new Quan_Li_Thong_Tin();
                    QuanLiThongTin.DataContext = new DataContextQLTT();
                    QuanLiThongTin.ShowDialog();
                });
                QuanLiTaiKhoanCommand = new RelayCommand<object>((p) => true, (p) =>
                {
                    Quan_Li_Tai_Khoan QuanLiTaiKhoan = new Quan_Li_Tai_Khoan();
                    QuanLiTaiKhoan.DataContext = new DataContextQLTK();
                    QuanLiTaiKhoan.ShowDialog();
                });
                QuanLiNhanVienCommand = new RelayCommand<object>((p) => true, (p) =>
                {
                    Quan_Li_Nhan_Vien QuanLiNhanVien = new Quan_Li_Nhan_Vien();
                    QuanLiNhanVien.DataContext = new DataContextQLNV();
                    QuanLiNhanVien.ShowDialog();
                });   
                ThongKeCommand = new RelayCommand<object>((p) => true, (p) =>
                {
                    ThongKeDoanhThu thongke = new ThongKeDoanhThu();
                    thongke.DataContext = new ThongKeDataContext();
                    thongke.ShowDialog();
                });
                ThayDoiQuyDinhCommand = new RelayCommand<object>((p) => true, (p) =>
                {
                    Thay_Doi_Quy_DInh ThayDoiQuyDinh = new Thay_Doi_Quy_DInh();
                    ThayDoiQuyDinh.DataContext = new DataContextTDQD();
                    ThayDoiQuyDinh.ShowDialog();
                });
            }
        }
        public FrameworkElement GetWindowParent(object p)
        {
            FrameworkElement parent = (FrameworkElement)p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
    }
}
