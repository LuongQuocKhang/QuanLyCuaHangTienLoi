using Quan_Ly_Ban_Hang.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Quan_Ly_Ban_Hang.View
{
    /// <summary>
    /// Interaction logic for Tim_Kiem.xaml
    /// </summary>
    public partial class Tim_Kiem : Window
    {
        public Tim_Kiem()
        {
            InitializeComponent();
            //CollectionView view = new CollectionView();// (CollectionView)CollectionViewSource.GetDefaultView(listsanpham.ItemsSource);
            //view.Filter = UserFilter;
        }
        //private bool UserFilter(object item)
        //{
        //    if (String.IsNullOrEmpty(txbMaHang.Text))
        //        return true;
        //    else
        //        return ((item as HANG).MAHANG.IndexOf(txbMaHang.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        //}
        //private void txbMaHang_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    CollectionViewSource.GetDefaultView(listsanpham.ItemsSource).Refresh();
        //}
    }
}
