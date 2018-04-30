using Quan_Ly_Ban_Hang.View;
using Quan_Ly_Ban_Hang.View.Quản_lý_hóa_đơn_bán_hàng;
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
        public ICommand NhapHangCommand { get; set; }
        public ICommand BanHangCommand { get; set; }
        public ICommand QuanLiCommand { get; set; }
        public ICommand TimKiemCommand { get; set; }
        public ICommand MouseDownCommand { get; set; }
        public DataContext()
        {
            NhapHangCommand = new RelayCommand<object>((p) => true,(p) => 
            {
                Quan_Ly_DDH QuanlyDDH = new Quan_Ly_DDH();
                QuanlyDDH.DataContext = new DataContextQuanLyDDH();
                QuanlyDDH.ShowDialog();
            });
            BanHangCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                QuanLyHoaDonBH quanlyhoadon = new QuanLyHoaDonBH();
                quanlyhoadon.DataContext = new DataContextQuanLyHD();
                quanlyhoadon.ShowDialog();
            });
            QuanLiCommand=new RelayCommand<object>((p) => true, (p) =>
            {
                Quan_Li_Thong_Tin QuanLiThongTin = new Quan_Li_Thong_Tin();
                QuanLiThongTin.DataContext = new DataContextQLTT();
                QuanLiThongTin.ShowDialog();
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
