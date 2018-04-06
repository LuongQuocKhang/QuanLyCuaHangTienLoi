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
        public DataContext()
        {
            NhapHangCommand = new RelayCommand<object>((p) => true,(p) => 
            {
                Don_Dat_Hang dondathang = new Don_Dat_Hang();
                dondathang.DataContext = new DataContextDDH();
                dondathang.Show();
            });
            BanHangCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                 Ban_Hang HoaDonBanHang = new Ban_Hang();
                HoaDonBanHang.DataContext = new DataContextHDBH();
                HoaDonBanHang.Show();
            });
        }
    }
}
