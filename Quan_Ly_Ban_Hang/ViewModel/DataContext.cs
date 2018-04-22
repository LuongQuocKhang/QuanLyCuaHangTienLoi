using Quan_Ly_Ban_Hang.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Quan_Ly_Ban_Hang.ViewModel
{
    public class DataContext : BaseViewModel
    {
        public ICommand NhapHangCommand { get; set; }
        public ICommand BanHangCommand { get; set; }
        public ICommand QuanLiCommand { get; set; }
        public ICommand TimKiemCommand { get; set; }
        public DataContext()
        {
            NhapHangCommand = new RelayCommand<object>((p) => true,(p) => 
            {
                Quan_Ly_DDH QuanlyDDH = new Quan_Ly_DDH();
                QuanlyDDH.DataContext = new DataContextQuanLyDDH();
                QuanlyDDH.Show();
            });
            BanHangCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                 Ban_Hang HoaDonBanHang = new Ban_Hang();
                HoaDonBanHang.DataContext = new DataContextHDBH();
                HoaDonBanHang.Show();
            });
            QuanLiCommand=new RelayCommand<object>((p) => true, (p) =>
            {
                Quan_Li_Thong_Tin QuanLiThongTin = new Quan_Li_Thong_Tin();
                QuanLiThongTin.DataContext = new DataContextQLTT();
                QuanLiThongTin.Show();
            });
            TimKiemCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                Tim_Kiem TimKiem = new Tim_Kiem();
                TimKiem.DataContext = new DataContextTK();
                TimKiem.Show();
            });
        }
    }
}
