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
        public ICommand LoadCommand { get; set; }
        public DataContext()
        {
            LoadCommand = new RelayCommand<object>((p) => true,(p) => 
            {
                Don_Dat_Hang _dondathang = new Don_Dat_Hang();
                _dondathang.Show();
            });
        }
    }
}
